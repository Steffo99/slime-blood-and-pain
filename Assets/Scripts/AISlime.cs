using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISlime : AI
{
    public float visionRange = 4;
    protected Player player;

    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void OnTurn(){
        if (CanSeePlayer()){
            //TODO
        }
        else {
            int direction = Random.Range(0, 4);
            if (direction == 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                transform.Translate(Vector3.left);
            }
            else if (direction == 1 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                transform.Translate(Vector3.right);
            }
            else if (direction == 2 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                transform.Translate(Vector3.up);
            }
            else if (direction == 3 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                transform.Translate(Vector3.down);
            }
        }
    }

    public bool CanSeePlayer(){
        return Vector3.Distance(player.transform.position, transform.position) < visionRange;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}