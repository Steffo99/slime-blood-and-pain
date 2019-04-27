using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Entity
{
    public virtual void OnTurn(){
        Debug.LogWarning("OnTurn() not overridden");
    }
}