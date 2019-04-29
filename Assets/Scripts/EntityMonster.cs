using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonster : Entity
{
    public string monsterName;

    public virtual void OnTurn() {
        //Do nothing.
    }

    public override void Die() {       
        messageBar.Write("Killed: " + monsterName, Color.red);
        Destroy(gameObject);
    }
}