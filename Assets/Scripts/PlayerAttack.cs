using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected EntityPlayer player;
    protected GameObject gameController;
    protected TurnHandler turnHandler;
    protected Map map;

    protected void Start() {
        player = GetComponent<EntityPlayer>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        turnHandler = gameController.GetComponentInChildren<TurnHandler>();
        map = gameController.GetComponentInChildren<Map>();
    }

    public virtual bool Attack(Vector2Int target) {
        //Returns if the attack was successful.
        Debug.LogWarning("Attack not overridden");
        return false;
    }
}