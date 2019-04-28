using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : PlayerAttack
{
    public float damage = 1f;

    public override bool Attack(Vector2Int target) {
        List<Entity> targetEntities = turnHandler.GetEntitiesAtPosition(target);
        if(targetEntities.Count == 0) {
            return false;
        }
        Entity targetEntity = targetEntities[0];
        targetEntity.hp -= damage;
        return true;
    }
}