using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItem : Entity
{
    public virtual string Name {
        get {
            Debug.LogWarning("No name given to an item");
            return "";
        }
    } 

    protected override void Start() {
        base.Start();
        overlappable = true;
    }

    public virtual void OnPickup(EntityPlayer player) {
        Debug.LogWarning("OnPickup not overridden");
        messageBar.Write("Picked up: " + Name, Color.yellow);
        Destroy(gameObject);
    }

    public override void Die() {
        messageBar.Write("Destroyed: " + Name, Color.red);
        Destroy(gameObject);
    }
}