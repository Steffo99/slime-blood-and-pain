using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode {
    Move,
    Attack
}

public class Player : Entity
{
    public int exp;
    public int level;

    protected ControlMode controlMode;

    protected override void Start() {
        base.Start();
        controlMode = ControlMode.Move;
    }

    protected void Update()
    {
        CheckForControlModeChange();
        if(controlMode == ControlMode.Move) CheckForMovementInput();
        if(controlMode == ControlMode.Attack) CheckForAttackInput();    
    }

    protected void CheckForControlModeChange() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            controlMode = ControlMode.Move;
            messageBar.Write("Control mode: Move", Color.cyan);
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            controlMode = ControlMode.Attack;
            messageBar.Write("Control mode: Attack", Color.cyan);
        }
    }

    protected void CheckForAttackInput() {
        bool hasAttacked = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hasAttacked = GetComponent<PlayerAttack>().Attack(MapPosition + Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasAttacked = GetComponent<PlayerAttack>().Attack(MapPosition + Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            hasAttacked = GetComponent<PlayerAttack>().Attack(MapPosition + Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            hasAttacked = GetComponent<PlayerAttack>().Attack(MapPosition + Vector2Int.down);
        }
        if(hasAttacked) {
            //Turn happens!
            turnHandler.OnTurn();
        }
    }

    protected void CheckForMovementInput()
    {
        bool hasMoved = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.left)) {
                transform.Translate(Vector3.left);
                hasMoved = true;
                spriteRenderer.flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.right)) {
                transform.Translate(Vector3.right);
                hasMoved = true;
                spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.up)) {
                transform.Translate(Vector3.up);
                hasMoved = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (map.CanMoveTo(MapPosition + Vector2Int.down)) {
                transform.Translate(Vector3.down);
                hasMoved = true;
            }
        }
        if(hasMoved) {
            //Check for pickuppable items
            List<Entity> entities = turnHandler.GetEntitiesAtPosition(MapPosition);
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
