using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImpossibleCorridorError : System.Exception
{
    public ImpossibleCorridorError() { }
    public ImpossibleCorridorError(string message) : base(message) { }
    public ImpossibleCorridorError(string message, System.Exception inner) : base(message, inner) { }
    protected ImpossibleCorridorError(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class IntVector2 {
    public int x;
    public int y;

    public IntVector2(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class MapRoom {
    public readonly IntVector2 start;
    public readonly IntVector2 end;
    public readonly int mapSize;

    public MapRoom(int mapSize, int maxRoomSize) {
        this.mapSize = mapSize;
        start = new IntVector2(Random.Range(0, mapSize), Random.Range(0, mapSize));
        end = new IntVector2(Random.Range(0, mapSize), Random.Range(0, mapSize));
        if(start.x > end.x) {
            int swap = start.x;
            start.x = end.x;
            end.x = swap;
        }
        if(start.y > end.y) {
            int swap = start.y;
            start.y = end.y;
            end.y = swap;
        }
        while(end.x - start.x > maxRoomSize) {
            end.x--;
            start.x++;
        }
        while(end.y - start.y > maxRoomSize) {
            end.y--;
            start.y++;
        }
    }
    
    public bool RightCheck() {
        return end.x < mapSize-1;
    }

    public IntVector2 RightCorridorAttachment() {
        return new IntVector2(end.x+1, Random.Range(start.y, end.y+1));
    }

    public bool LeftCheck() {
        return start.x > 0;
    }

    public IntVector2 LeftCorridorAttachment() {
        return new IntVector2(start.x-1, Random.Range(start.y, end.y+1));
    }
    
    public bool TopCheck() {
        return end.y < mapSize-1;
    }

    public IntVector2 TopCorridorAttachment() {
        return new IntVector2(Random.Range(start.x, end.x+1), end.y+1);
    }

    public bool BottomCheck() {
        return start.y > 0;
    }

    public IntVector2 BottomCorridorAttachment() {
        return new IntVector2(Random.Range(start.x, end.x+1), start.y-1);
    }
}

public enum CorridorModes {
    bottomToTop,
    topToBottom,
    rightToLeft,
    leftToRight
}

public class MapCorridor {
    public readonly IntVector2 start;
    public readonly IntVector2 end;
    public readonly bool horizontal_priority;

    public MapCorridor(MapRoom from, MapRoom to, int mapSize) {
        List<CorridorModes> corridorModes = new List<CorridorModes>();
        //Find allowed CorridorModes
        if(from.end.y <= to.start.y && from.BottomCheck() && to.TopCheck()) corridorModes.Add(CorridorModes.bottomToTop);
        if(from.start.y >= to.end.y && from.TopCheck() && to.BottomCheck()) corridorModes.Add(CorridorModes.topToBottom);
        if(from.end.x <= to.start.y && from.RightCheck() && to.LeftCheck()) corridorModes.Add(CorridorModes.rightToLeft);
        if(from.start.y >= to.end.y && from.LeftCheck() && to.RightCheck()) corridorModes.Add(CorridorModes.leftToRight);
        //Select and use a corridor mode
        if(corridorModes.Count < 1) throw new ImpossibleCorridorError();
        CorridorModes corridorMode = corridorModes[Random.Range(0, corridorModes.Count)];
        if(corridorMode == CorridorModes.bottomToTop) {
            start = from.BottomCorridorAttachment();
            end = to.TopCorridorAttachment();
        }
        if(corridorMode == CorridorModes.topToBottom) {
            start = from.TopCorridorAttachment();
            end = to.BottomCorridorAttachment();
        }
        if(corridorMode == CorridorModes.rightToLeft) {
            start = from.RightCorridorAttachment();
            end = to.LeftCorridorAttachment();
        }
        if(corridorMode == CorridorModes.leftToRight) {
            start = from.LeftCorridorAttachment();
            end = to.RightCorridorAttachment();
        }
        //50%
        horizontal_priority = Random.Range(0f, 1f) >= 0.5f;
    }
}

public class Map : MonoBehaviour
{
    [BeforeStartAttribute]
    public int mapSize = 30;

    [BeforeStartAttribute]
    public int roomsToGenerate = 5;

    [BeforeStartAttribute]
    public int maxRoomSize = 6;

    [BeforeStartAttribute]
    public int maxRoomIterations = 100;

    [BeforeStartAttribute]
    public Sprite wallSprite;

    [BeforeStartAttribute]
    public Sprite roomSprite;

    [BeforeStartAttribute]
    public Sprite corridorSprite;

    [BeforeStartAttribute]
    public GameObject tilePrefab;

    private GameObject[,] tiles;
    private List<MapRoom> rooms;
    private System.Random rnd;

    public Tile GetTile(int x, int y) {
        GameObject tileObject = tiles[x, y];
        Tile tile = tileObject.GetComponent<Tile>();
        return tile;
    }

    private void InitTile(int x, int y, bool walkable, Sprite tileSprite, bool roomPart) {
        Tile tile = GetTile(x, y);
        tile.walkable = walkable;
        tile.sprite = tileSprite;
        tile.roomPart = roomPart;
    }

    private void FillWithWalls() {
        for(int x = 0; x < mapSize; x++) {
            for(int y = 0; y < mapSize; y++) {
                GameObject tileObject = Instantiate(tilePrefab, transform);
                tileObject.transform.position = new Vector3(x, y, 0);
                tiles[x, y] = tileObject;
                tileObject.name = "Tile [" + x.ToString() + ", " + y.ToString() + "]";
                Tile tile = tileObject.GetComponent<Tile>();
                tile.walkable = false;
                tile.sprite = wallSprite;
            }
        }
    }

    private void PlaceRoom(MapRoom mr) {
        for(int x = mr.start.x; x <= mr.end.x; x++) {
            for(int y = mr.start.y; y <= mr.end.y; y++) {
                InitTile(x, y, true, roomSprite, true);
            }
        }
    }

    private bool ScanRoom(MapRoom mr) {
        //Returns true if the room can be safely placed
        for(int x = Mathf.Clamp(mr.start.x-1, 0, mapSize-1); x <= Mathf.Clamp(mr.end.x+1, 0, mapSize-1); x++) {
            for(int y = Mathf.Clamp(mr.start.y-1, 0, mapSize-1); y <= Mathf.Clamp(mr.end.y+1, 0, mapSize-1); y++) {
                if(GetTile(x, y).roomPart) {
                    return false;
                }
            }
        }
        return true;
    }

    private void PlaceCorridor(MapCorridor mc) {
        IntVector2 cursor = new IntVector2(mc.start.x, mc.start.y);
        InitTile(cursor.x, cursor.y, true, corridorSprite, false);
        if(mc.horizontal_priority) {
            while(cursor.x != mc.end.x) {
                if(cursor.x > mc.end.x) cursor.x--;
                else cursor.x++;
                InitTile(cursor.x, cursor.y, true, corridorSprite, false);
            }
            while(cursor.y != mc.end.y) {
                if(cursor.y > mc.end.y) cursor.y--;
                else cursor.y++;
                InitTile(cursor.x, cursor.y, true, corridorSprite, false);
            }
        }
        else
        {
            while(cursor.y != mc.end.y) {
                if(cursor.y > mc.end.y) cursor.y--;
                else cursor.y++;
                InitTile(cursor.x, cursor.y, true, corridorSprite, false);
            } 
            while(cursor.x != mc.end.x) {
                if(cursor.x > mc.end.x) cursor.x--;
                else cursor.x++;
                InitTile(cursor.x, cursor.y, true, corridorSprite, false);
            }
        }
    }
    
    private void GenerateMap() {
        FillWithWalls();
        int roomIterations = 0;
        while(rooms.Count < roomsToGenerate && roomIterations < maxRoomIterations) {
            roomIterations++;
            MapRoom room = new MapRoom(mapSize, maxRoomSize);
            if(ScanRoom(room)) {
                PlaceRoom(room);
                rooms.Add(room);
            }
            if(rooms.Count > 1) {
                MapRoom from = rooms[rooms.Count-2];
                MapRoom to = rooms[rooms.Count-1];
                try {
                    MapCorridor corridor = new MapCorridor(from, to, mapSize);
                    PlaceCorridor(corridor);
                }
                catch (ImpossibleCorridorError) {}
            }
        }
    }

    private void Start()
    {
        //Initialize everything
        tiles = new GameObject[mapSize, mapSize];
        rooms = new List<MapRoom>();
        rnd = new System.Random();
        //Generate the map
        GenerateMap();   
    }
}
