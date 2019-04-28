using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int exp;
    public int level;

    void Update()
    {
        CheckForMovementInput();
    }

    void CheckForMovementInput()
    {
        bool hasMoved = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.left)) {
                transform.Translate(Vector3.left);
                hasMoved = true;
                spriteRenderer.flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.right)) {
                transform.Translate(Vector3.right);
                hasMoved = true;
                spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.up)) {
                transform.Translate(Vector3.up);
                hasMoved = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.down)) {
                transform.Translate(Vector3.down);
                hasMoved = true;
            }
        }
        if(hasMoved) {
            //Check for pickuppable items
            List<Entity> entities = turnHandler.GetEntityAtPosition(MapPosition);
            foreach(Entity entity in entities) {
                if(entity is Item) {
                    Item item = entity as Item;
                    item.OnPickup(this);
                }
            }
            //Turn happens!
            turnHandler.OnTurn();
        }
    }
}
