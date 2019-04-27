using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public const int SIZE_X = 30;
    public const int SIZE_Y = 30;
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
        GameObject tileObject = Instantiate(tilePrefab);
        tileObject.transform.position = new Vector3(x, y, 0);
        tiles[x, y] = tileObject;
        Tile tile = GetTile(x, y);
        tile.is_walkable = is_walkable;
        tile.tile_sprite = tile_sprite;
    }

    private void FillWithWalls() {
        for(int x = 0; x < SIZE_X; x++) {
            for(int y = 0; y < SIZE_Y; y++) {
                InitTile(x, y, false, wallSprite);
            }
        }
    }

    private void GenerateSquare(int start_x, int start_y, int end_x, int end_y) {
        for(int x = start_x; x <= end_x; x++) {
            for(int y = start_y; y <= end_y; y++) {
                InitTile(x, y, true, floorSprite);
            }
        }
    }

    private void GenerateCorridor(int start_x, int start_y, int end_x, int end_y, bool horizontal_priority) {
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
        //Generate a random Square
        int start_x = Random.Range(0, SIZE_X);
        int start_y = Random.Range(0, SIZE_Y);
        int end_x = Random.Range(start_x, SIZE_X);
        int end_y = Random.Range(start_y, SIZE_Y);
        GenerateSquare(start_x, start_y, end_x, end_y);
    }

    private void Start()
    {
        //Create the tile array
        tiles = new GameObject[SIZE_X, SIZE_Y];
        //Generate the map
        GenerateMap();   
    }
}
