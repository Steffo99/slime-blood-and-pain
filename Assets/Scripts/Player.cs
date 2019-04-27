using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int exp, level, hp, maxhp;
    public int startingHp;
    public Map map;
    //TODO: Aggiungi gli oggetti in inventario

    // Start is called before the first frame update
    void Start()
    {
        hp = startingHp;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }

    void playerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (is_valid_movement("left")) transform.Translate(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (is_valid_movement("right")) transform.Translate(Vector3.right);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (is_valid_movement("up")) transform.Translate(Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (is_valid_movement("down")) transform.Translate(Vector3.down);
        }
        // Qui c'è da aggiungere la condizione per il controllo degli hp
    }
    
    bool is_valid_movement(string direction)
    {
        Tile tile;
        int posX = (int) transform.position.x;
        int posY = (int) transform.position.y;
        if (direction == "left") tile = map.GetTile(posX - 1, posY);
        else if (direction == "right") tile = map.GetTile(posX + 1, posY);
        else if (direction == "up") tile = map.GetTile(posX, posY + 1);
        else tile = map.GetTile(posX, posY - 1);
        return tile.walkable;
    }
}
