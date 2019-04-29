using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemPoisonHeart : EntityItem
{
    public float damage;

    public override string Name {
        get {
            return "Poisonous Heart";
        }
    } 
    
    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("Poisoned! Took " + damage.ToString("0.0") + " damage!", Color.red);
        player.hp -= damage;
        Destroy(gameObject);
    }
}