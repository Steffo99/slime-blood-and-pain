using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMonster : Entity
{
    public string monsterName;

    public virtual void OnTurn(){
        Debug.LogWarning("OnTurn() not overridden");
    }

    public override void Die() {       
        messageBar.Write("Killed: " + monsterName, Color.red);
        Destroy(gameObject);
    }
}