using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonsterSlime : EntityMonster
{
    public override string Name {
        get {
            return "Slime";
        }
    } 

    public float moveChance = 0.5f;
    public float visionRange = 4f;
    public float damage = 1f;
    public GameObject attackAnimation;
    protected EntityPlayer player;

    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    public override void OnTurn(){
        if(Random.Range(0f, 1f) < moveChance) return;
        if (CanSeePlayer()){
            Vector2Int distance = player.MapPosition - MapPosition;
            if (distance.x < 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                transform.Translate(Vector3.left);
            }
            else if (distance.x > 0 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                transform.Translate(Vector3.right);
            }
            else if (distance.y > 0 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                transform.Translate(Vector3.up);
            }
            else if (distance.y < 0 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                transform.Translate(Vector3.down);
            }
        }
        else {
            if(Vector3.Distance(player.transform.position, transform.position) > 2){
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
            else{
                float damage_done = Random.value * damage;
                player.hp -= damage;
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