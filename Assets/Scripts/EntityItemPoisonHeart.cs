using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemPoisonHeart : EntityItem
{
    public int damage;

    public override string Name {
        get {
            return "Poisonous Heart";
        }
    } 
    
    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("You used: " + Name, Color.yellow);
        player.hp -= damage;
        Destroy(gameObject);
    }
}