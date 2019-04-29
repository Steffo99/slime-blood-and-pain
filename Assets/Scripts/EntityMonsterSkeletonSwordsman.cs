using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonsterSkeletonSwordsman : EntityMonster
{
    public float moveChance = 1f;
    public float visionRange = 5f;
    public float attackRange = 1f;
    public float damage = 2f;
    public GameObject attackAnimation;
    protected EntityPlayer player;

    [BeforeStartAttribute]
    public Sprite upSprite;

    [BeforeStartAttribute]
    public Sprite downSprite;

    private bool has_attacked = false;

    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    public override void OnTurn(){
        if(Random.value < moveChance) return;
        if (CanSeePlayer() && !has_attacked){
            Vector2Int distance = player.MapPosition - MapPosition;
            if(distance.magnitude <= attackRange) {
                float actualDamage = Random.value * damage;
                player.hp -= actualDamage;
                Instantiate(attackAnimation, player.transform);
                messageBar.Write("Took " + actualDamage.ToString("0.0") + " damage!", Color.red);
                has_attacked = true;
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
            has_attacked = false;
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
