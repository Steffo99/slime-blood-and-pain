using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hpMax;
    public int hp;
    public bool isUndead;
    public int movesPerTurn;
    public bool hasSpottedPlayer;

    [BeforeStartAttribute]
    public Sprite sprite;

    private Map map;
    
    // Start is called before the first frame update
    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        hasSpottedPlayer = false;
    }
    
    void Turn(){
        int direction = Random.Range(0, 4);
        if (direction == 0 && map.CanMoveTo(Vector2Int.left)){
            transform.Translate(Vector3.left);
        }
        else if (direction == 1 && map.CanMoveTo(Vector2Int.right)){
            transform.Translate(Vector3.right);
        }
        else if (direction == 2 && map.CanMoveTo(Vector2Int.up)){
            transform.Translate(Vector3.up);
        }
        else if (direction == 3 && map.CanMoveTo(Vector2Int.left)){
            transform.Translate(Vector3.left);
        }
    }
}
