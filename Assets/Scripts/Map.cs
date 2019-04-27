﻿using System.Collections;
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

public class MapRoom {
    public readonly Vector2Int start;
    public readonly Vector2Int end;
    public readonly int mapSize;

    public MapRoom(int mapSize, int maxRoomSize, int minRoomSize) {
        this.mapSize = mapSize;
        start = new Vector2Int(Random.Range(1, mapSize-1), Random.Range(1, mapSize-1));
        end = new Vector2Int(Random.Range(1, mapSize-1), Random.Range(1, mapSize-1));
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
        while(end.x - start.x < minRoomSize) {
            end.x++;
            start.x--;
        }
        while(end.y - start.y < minRoomSize) {
            end.y++;
            start.y--;
        }
        start.Clamp(new Vector2Int(1, 1), new Vector2Int(mapSize-1, mapSize-1));
        end.Clamp(new Vector2Int(1, 1), new Vector2Int(mapSize-1, mapSize-1));
    }

    public Vector2Int RandomPoint() {
        return new Vector2Int(Random.Range(start.x, end.x+1), Random.Range(start.y, end.y+1));
    }
}

public class MapCorridor {
    public readonly Vector2Int start;
    public readonly Vector2Int end;
    public readonly bool horizontal_priority;

    public MapCorridor(MapRoom from, MapRoom to, int mapSize) {
        start = from.RandomPoint();
        end = to.RandomPoint();
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
    public int minRoomSize = 2;

    [BeforeStartAttribute]
    public int maxRoomSize = 6;

    [BeforeStartAttribute]
    public int maxRoomIterations = 100;

    [BeforeStartAttribute]
    public Sprite wallSprite;

    [BeforeStartAttribute]
    public List<Sprite> floorSprites;

    [BeforeStartAttribute]
    public GameObject tilePrefab;

    [BeforeStartAttribute]
    public GameObject playerPrefab;

    [BeforeStartAttribute]
    public List<GameObject> enemyPrefabs;

    [BeforeStartAttribute]
    public int enemiesToSpawn = 10;

    private GameObject[,] tiles;
    private List<MapRoom> rooms;
    private TurnHandler turnHandler;

    public Tile GetTile(Vector2Int position) {
        GameObject tileObject = tiles[position.x, position.y];
        return tileObject.GetComponent<Tile>();
    }

    public bool CanMoveTo(Vector2Int position)
    {
        try {
            bool walkable = GetTile(position).walkable;
            bool free = !(bool)turnHandler.GetEntityAtPosition(position);
            return walkable && free;
        }
        catch(System.IndexOutOfRangeException) {
            return false;
        }
    }

    private void InitTile(Vector2Int position, bool walkable, Sprite tileSprite, bool roomPart) {
        Tile tile = GetTile(position);
        tile.walkable = walkable;
        tile.sprite = tileSprite;
        tile.roomPart |= roomPart;
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
                InitTile(new Vector2Int(x, y), true, GetFloorTileSprite(), true);
            }
        }
    }

    private bool ScanRoom(MapRoom mr) {
        //Returns true if the room can be safely placed
        for(int x = Mathf.Clamp(mr.start.x-1, 0, mapSize-1); x <= Mathf.Clamp(mr.end.x+1, 0, mapSize-1); x++) {
            for(int y = Mathf.Clamp(mr.start.y-1, 0, mapSize-1); y <= Mathf.Clamp(mr.end.y+1, 0, mapSize-1); y++) {
                if(GetTile(new Vector2Int(x, y)).roomPart) {
                    return false;
                }
            }
        }
        return true;
    }

    private void PlaceCorridor(MapCorridor mc) {
        Vector2Int cursor = new Vector2Int(mc.start.x, mc.start.y);
        InitTile(cursor, true, GetFloorTileSprite(), false);
        if(mc.horizontal_priority) {
            while(cursor.x != mc.end.x) {
                if(cursor.x > mc.end.x) cursor.x--;
                else cursor.x++;
                InitTile(cursor, true, GetFloorTileSprite(), false);
            }
            while(cursor.y != mc.end.y) {
                if(cursor.y > mc.end.y) cursor.y--;
                else cursor.y++;
                InitTile(cursor, true, GetFloorTileSprite(), false);
            }
        }
        else
        {
            while(cursor.y != mc.end.y) {
                if(cursor.y > mc.end.y) cursor.y--;
                else cursor.y++;
                InitTile(cursor, true, GetFloorTileSprite(), false);
            } 
            while(cursor.x != mc.end.x) {
                if(cursor.x > mc.end.x) cursor.x--;
                else cursor.x++;
                InitTile(cursor, true, GetFloorTileSprite(), false);
            }
        }
    }
    
    private void GenerateMap() {
        FillWithWalls();
        int roomIterations = 0;
        while(rooms.Count < roomsToGenerate && roomIterations < maxRoomIterations) {
            roomIterations++;
            MapRoom room = new MapRoom(mapSize, maxRoomSize, minRoomSize);
            if(ScanRoom(room)) {
                //Fill with the room
                PlaceRoom(room);
                rooms.Add(room);
            }
            //Place a corridor
            if(rooms.Count > 1) {
                MapRoom from = rooms[rooms.Count-2];
                MapRoom to = rooms[rooms.Count-1];
                try {
                    MapCorridor corridor = new MapCorridor(from, to, mapSize);
                    PlaceCorridor(corridor);
                }
                catch (ImpossibleCorridorError) {
                }
            }
        }
    }

    private void PlacePlayer() {
        MapRoom room = rooms[Random.Range(0, rooms.Count)];
        Vector2Int point = room.RandomPoint();
        GameObject playerObject = Instantiate(playerPrefab, turnHandler.transform);
        playerObject.name = "Player";
        playerObject.transform.position = new Vector3(point.x, point.y, 0);
    }

    private void PlaceEnemies() {
        for(int i = 0; i < enemiesToSpawn; i++) {
            MapRoom room = rooms[Random.Range(0, rooms.Count)];
            Vector2Int point = room.RandomPoint();
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            GameObject enemyObject = Instantiate(enemyPrefab, turnHandler.transform);
            enemyObject.name = "Enemy " + i.ToString();
            enemyObject.transform.position = new Vector3(point.x, point.y, 0);
        }
    }

    private Sprite GetFloorTileSprite() {
        return floorSprites[Random.Range(0, floorSprites.Count)];
    }

    private void Start()
    {
        //Initialize everything
        tiles = new GameObject[mapSize, mapSize];
        rooms = new List<MapRoom>();
        turnHandler = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<TurnHandler>();

        GenerateMap();   
        PlacePlayer();
        PlaceEnemies();
    }
}