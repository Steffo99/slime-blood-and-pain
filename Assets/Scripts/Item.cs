using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    private new void Start() {
        overlappable = true;
    }

    public virtual void OnPickup(Player player) {
        Debug.LogWarning("OnPickup not overridden");
    }
}