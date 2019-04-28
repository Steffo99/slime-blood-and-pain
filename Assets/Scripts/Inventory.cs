using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemNotInInventoryException : System.Exception
{
    public ItemNotInInventoryException() { }
    public ItemNotInInventoryException(string message) : base(message) { }
    public ItemNotInInventoryException(string message, System.Exception inner) : base(message, inner) { }
    protected ItemNotInInventoryException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class Inventory {
    public List<InventoryItem> items;

    public InventoryItem GetItemByName(string name) {
        foreach(InventoryItem item in items) {
            if(name == item.Name) 
            {
                return item;
            }
        }
        return null;
    }

    public void AddItem(InventoryItem item) {
        //Check if it's mergeable 
        InventoryItem other = GetItemByName(item.Name);
        if(other == null) {
            items.Add(item);
        }
        else {
            other.quantity += item.quantity;
        }
    }

    public void UseItem(InventoryItem item) {
        if(!items.Contains(item)) throw new ItemNotInInventoryException();
        item.Use();
        if(item.quantity <= 0) {
            items.Remove(item);
        }
    }
}