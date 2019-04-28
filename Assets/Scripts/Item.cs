using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    public static string itemName = "White Triangle";

    protected override void Start() {
        base.Start();
        overlappable = true;
    }

    public virtual void OnPickup(Player player) {
        Debug.LogWarning("OnPickup not overridden");
        messageBar.Write("Picked up: " + itemName, Color.yellow);
        Destroy(gameObject);
    }

    public override void Die() {
        messageBar.Write("Destroyed: " + itemName, Color.red);
        Destroy(gameObject);
    }
}