using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    protected override void Start() {
        base.Start();
        overlappable = true;
    }

    public virtual void OnPickup(Player player) {
        Debug.LogWarning("OnPickup not overridden");
        turnHandler.WriteToMessageBar("Picked up [NULL].");
        Destroy(gameObject);
    }
}