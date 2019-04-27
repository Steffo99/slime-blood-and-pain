using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int exp;
    public int level; 
    public int hpMax;

    [AfterStartAttribute]
    public int hp; 
    
    private Map map;
    private GameObject gameController;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        hp = hpMax;
    }

    void Update()
    {
        CheckForMovementInput();
    }

    void CheckForMovementInput()
    {
        bool hasMoved = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (map.CanMoveTo(Vector2Int.left)) {
                transform.Translate(Vector3.left);
                hasMoved = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (map.CanMoveTo(Vector2Int.right)) {
                transform.Translate(Vector3.right);
                hasMoved = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (map.CanMoveTo(Vector2Int.up)) {
                transform.Translate(Vector3.up);
                hasMoved = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (map.CanMoveTo(Vector2Int.down)) {
                transform.Translate(Vector3.down);
                hasMoved = true;
            }
        }
        if(hasMoved) {
            gameController.BroadcastMessage("Turn");
        }
    }
}
