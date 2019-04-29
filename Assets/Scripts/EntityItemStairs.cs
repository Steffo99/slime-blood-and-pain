using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemStairs : EntityItem
{
    public override string Name {
        get {
            return "Stairs";
        }
    } 
    
    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("Generating next floor...", Color.magenta);
        map.NewLevel();
    }
}