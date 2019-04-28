using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {

    public int quantity = 1;

    public virtual string Name {
        //Two items with the same name will stack!
        get {
            Debug.LogError("No name given to an item");
            return "";
        }
    } 

    public virtual void Use() {
        Debug.LogWarning("Use not overridden");
        quantity -= 1;
    }

    public InventoryItem(int quantity) {
        this.quantity = quantity;
    }
}