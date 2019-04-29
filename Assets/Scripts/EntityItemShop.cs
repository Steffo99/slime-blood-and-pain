using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemShop : EntityItem {
    public float hpChange = -1f;
    public float maxHpChange = -1f;

    protected virtual void OnPurchase(EntityPlayer player) {
        Debug.LogWarning("OnPurchase not overridden");
    }

    public override void OnPickup(EntityPlayer player) {
        player.hp += hpChange;
        player.hpMax += maxHpChange;
        OnPurchase(player);
        messageBar.Write("Bought: " + Name, Color.yellow);
        Destroy(gameObject);
    }
}