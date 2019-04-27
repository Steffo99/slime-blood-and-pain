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
        CheckForMovementInput();
    }

    void CheckForMovementInput()
    {
        bool hasMoved = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMoveTo(Vector2Int.left)) {
                transform.Translate(Vector3.left);}
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanMoveTo(Vector2Int.right)) {
                transform.Translate(Vector3.right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanMoveTo(Vector2Int.up)) {
                transform.Translate(Vector3.up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CanMoveTo(Vector2Int.down)) {
                transform.Translate(Vector3.down);
            }
        }
        
    }
    
    bool CanMoveTo(Vector2Int direction)
    {
        return map.GetTile(direction).walkable;
    }
}
