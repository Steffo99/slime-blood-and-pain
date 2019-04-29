using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemShop : EntityItem {
    public float hpChange = -1f;
    public float maxHpChange = -1f;

    protected EntityPlayer player;

    protected override void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    protected virtual void OnPurchase() {
        Debug.LogWarning("OnPurchase not overridden");
    }

    public override void OnPickup(EntityPlayer player) {
        player.hp += hpChange;
        player.hpMax += maxHpChange;
        OnPurchase();
        messageBar.Write("Bought: " + Name, Color.yellow);
        Destroy(gameObject);
    }
}