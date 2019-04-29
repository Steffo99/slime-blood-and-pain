using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySkeletonArcher : EntityMonster
{
    public float moveChance = 0.5f;
    public float visionRange = 6f;
    public float attackRange = 5f;
    public float damage = 1.5f;
    public GameObject attackAnimation;
    protected EntityPlayer player;

    [BeforeStartAttribute]
    public Sprite upSprite;

    [BeforeStartAttribute]
    public Sprite downSprite;

    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    public override void OnTurn(){
        if(Random.value < moveChance) return;
        if (CanSeePlayer()){
            Vector2Int distance = player.MapPosition - MapPosition;
            if(distance.magnitude <= attackRange) {
                if(Random.value < (distance.magnitude/10)+0.25f){ //se colpisce, sta fermo
                    float actualDamage = Random.value * damage;
                    player.hp -= actualDamage;
                    Instantiate(attackAnimation, player.transform);
                    messageBar.Write("Took " + actualDamage.ToString("0.0") + " damage from an arrow!", Color.red);
                }
                else{ //se non colpisce, avanza verso il giocatore
                    int direction2 = Random.Range(0, 4);
                    if (direction2 == 0 && map.CanMoveTo(MapPosition + Vector2Int.left)){
                        transform.Translate(Vector3.left);
                        spriteRenderer.flipX = false;
                    }
                    else if (direction2 == 1 && map.CanMoveTo(MapPosition + Vector2Int.right)){
                        transform.Translate(Vector3.right);
                        spriteRenderer.flipX = true;
                    }
                    else if (direction2 == 2 && map.CanMoveTo(MapPosition + Vector2Int.up)){
                        transform.Translate(Vector3.up);
                        spriteRenderer.sprite = upSprite;
                    }
                    else if (direction2 == 3 && map.CanMoveTo(MapPosition + Vector2Int.down)){
                        transform.Translate(Vector3.down);
                        spriteRenderer.sprite = downSprite;
                    }
                }
            }
            else{
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
