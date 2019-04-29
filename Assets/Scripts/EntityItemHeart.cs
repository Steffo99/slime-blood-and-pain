using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemHeart : EntityItem
{
    public int regen;

    public override string Name {
        get {
            return "Heart";
        }
    } 

    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("Picked up: " + Name, Color.lime);
        player.hp += regen;
        if (player.hp > player.hpMax) player.hp = player.hpMax;
        Destroy(gameObject);
    }
}
