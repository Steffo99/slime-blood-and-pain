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
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMoveTo(Vector2Int.left)) transform.Translate(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanMoveTo(Vector2Int.right)) transform.Translate(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanMoveTo(Vector2Int.up)) transform.Translate(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CanMoveTo(Vector2Int.down)) transform.Translate(Vector3.down);
        }
        // Qui c'è da aggiungere la condizione per il controllo degli hp
    }
    
    bool CanMoveTo(Vector2Int direction)
    {
        Tile tile;
        return map.GetTile(direction).walkable;
    }
}
