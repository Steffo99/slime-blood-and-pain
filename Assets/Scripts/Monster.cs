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

    private Player player;
    
    // Start is called before the first frame update
    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hasSpottedPlayer = false;
    }
    
    void Turn(){
        IsPlayerNear();
        if (!hasSpottedPlayer){
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
        else{
            //Put here pathfinding code
        }
    }

    void IsPlayerNear(){
        if ((player.transform.position.x-transform.position.x)<=3 || (transform.position.x-player.transform.position.x)>=-3){
            hasSpottedPlayer=true;
        }
        if ((player.transform.position.y-transform.position.y)<=3 || (transform.position.y-player.transform.position.y)>=-3){
            hasSpottedPlayer=true;
        }
    }
}
