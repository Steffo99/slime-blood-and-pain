using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [BeforeStartAttribute]
    public int mapSize = 30;

    [BeforeStartAttribute]
    public int roomsToGenerate = 5;

    [BeforeStartAttribute]
    public int maxRoomSize = 8;

    public GameObject[,] tiles;
    public Sprite wallSprite;
    public Sprite floorSprite;
    public GameObject tilePrefab;

    public Tile GetTile(int x, int y) {
        GameObject tileObject = tiles[x, y];
        Tile tile = tileObject.GetComponent<Tile>();
        return tile;
    }

    private void InitTile(int x, int y, bool is_walkable, Sprite tile_sprite) {
        Tile tile = GetTile(x, y);
        tile.walkable = is_walkable;
        tile.sprite = tile_sprite;
    }

    private void FillWithWalls() {
        for(int x = 0; x < mapSize; x++) {
            for(int y = 0; y < mapSize; y++) {
                GameObject tileObject = Instantiate(tilePrefab, transform);
                tileObject.transform.position = new Vector3(x, y, 0);
                tiles[x, y] = tileObject;
                Tile tile = tileObject.GetComponent<Tile>();
                tile.walkable = false;
                tile.sprite = wallSprite;
            }
        }
    }

    private void PlaceRoom(int start_x, int start_y, int end_x, int end_y) {
        for(int x = start_x; x <= end_x; x++) {
            for(int y = start_y; y <= end_y; y++) {
                InitTile(x, y, true, floorSprite);
            }
        }
    }

    private void PlaceCorridor(int start_x, int start_y, int end_x, int end_y, bool horizontal_priority) {
        if(horizontal_priority) {
            for(int x = start_x; x <= end_x; x++) {
                InitTile(x, start_y, true, floorSprite);
            }
            for(int y = start_y; y <= end_y; y++) {
                InitTile(end_x, y, true, floorSprite);
            }
        }
        else {
            for(int y = start_y; y <= end_y; y++) {
                InitTile(start_x, y, true, floorSprite);
            }
            for(int x = start_x; x <= end_x; x++) {
                InitTile(x, end_y, true, floorSprite);
            }
        }
    }
    
    private void GenerateMap() {
        FillWithWalls();
        for(int i = 0; i < roomsToGenerate; i++) {
            int start_x = Random.Range(0, mapSize);
            int start_y = Random.Range(0, mapSize);
            int end_x = Random.Range(0, mapSize) + start_x;
            int end_y = Random.Range(0, mapSize) + start_y;
            PlaceRoom(start_x, start_y, end_x, end_y);
        }
    }

    private void Start()
    {
        //Create the tile array
        tiles = new GameObject[mapSize, mapSize];
        //Generate the map
        GenerateMap();   
    }
}
