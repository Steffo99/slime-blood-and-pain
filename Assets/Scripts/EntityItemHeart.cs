using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemHeart : EntityItem
{
    public float regen;

    public override string Name {
        get {
            return "Heart";
        }
    } 

    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("Healed " + regen.ToString("0.0") + " HP!", Color.green);
        player.hp += regen;
        if (player.hp > player.hpMax) player.hp = player.hpMax;
        Destroy(gameObject);
    }
}
