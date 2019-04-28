using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonster : Entity
{
    public virtual string Name {
        get {
            Debug.LogWarning("No name given to a monster");
            return "";
        }
    } 

    public virtual void OnTurn(){
        Debug.LogWarning("OnTurn() not overridden");
    }

    public override void Die() {       
        messageBar.Write("Killed: " + Name, Color.red);
        Destroy(gameObject);
    }
}