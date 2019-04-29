using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonsterWatcher : EntityMonster
{
    public float moveChance = 1f;
    public float visionRange = 5f;
    public float attackRange = 1f;
    public float damage = 2f;
    public float dash_chance_starting = 0.5f;
    private float dash_chance_current;
    public GameObject attackAnimation;
    protected EntityPlayer player;

    [BeforeStartAttribute]
    public Sprite upSprite;

    [BeforeStartAttribute]
    public Sprite downSprite;


    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
        dash_chance_current=dash_chance_starting;
    }

    public override void OnTurn(){
        if(Random.value < moveChance) return;
        if (CanSeePlayer()){
            Vector2Int distance = player.MapPosition - MapPosition;
            if(distance.magnitude <= attackRange) {
                float actualDamage = Random.value * damage;
                player.hp -= actualDamage;
                Instantiate(attackAnimation, player.transform);
                messageBar.Write("Took " + actualDamage.ToString("0.0") + " damage!", Color.red);
                if(Random.value < dash_chance_current){
                    int direction_dash = Random.Range(0, 4);
                    if (direction_dash == 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                        transform.Translate(Vector3.left);
                        spriteRenderer.flipX = false;
                    }
                    else if (direction_dash == 1 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                        transform.Translate(Vector3.right);
                        spriteRenderer.flipX = true;
                    }
                    else if (direction_dash == 2 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                        transform.Translate(Vector3.up);
                        spriteRenderer.sprite = upSprite;
                    }
                    else if (direction_dash == 3 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                        transform.Translate(Vector3.down);
                        spriteRenderer.sprite = downSprite;
                    }
                    dash_chance_current=dash_chance_starting;
                }
                else{
                    dash_chance_current-=0.1f;
                }
            }
            else if (distance.x < 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                transform.Translate(Vector3.left);
                spriteRenderer.flipX = false;
            }
            else if (distance.x > 0 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                transform.Translate(Vector3.right);
                spriteRenderer.flipX = true;
            }
            else if (distance.y > 0 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                transform.Translate(Vector3.up);
                spriteRenderer.sprite = upSprite;
            }
            else if (distance.y < 0 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                transform.Translate(Vector3.down);
                spriteRenderer.sprite = downSprite;
            }
        }
        else {
            int direction = Random.Range(0, 4);
            if (direction == 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                transform.Translate(Vector3.left);
                spriteRenderer.flipX = false;
            }
            else if (direction == 1 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                transform.Translate(Vector3.right);
                spriteRenderer.flipX = true;
            }
            else if (direction == 2 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                transform.Translate(Vector3.up);
                spriteRenderer.sprite = upSprite;
            }
            else if (direction == 3 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                transform.Translate(Vector3.down);
                spriteRenderer.sprite = downSprite;
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
