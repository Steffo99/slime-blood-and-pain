using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float hpMax;
    public bool overlappable = false;

    [AfterStartAttribute]
    public float hp;

    public Vector2Int MapPosition {
        get {
            return new Vector2Int((int)transform.position.x, (int)transform.position.y);
        }
    }

    protected GameObject gameController;
    protected SpriteRenderer spriteRenderer;
    protected TurnHandler turnHandler;
    protected Map map;
    protected MessageBar messageBar;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        turnHandler = gameController.GetComponentInChildren<TurnHandler>();
        map = gameController.GetComponentInChildren<Map>();
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        messageBar = canvas.GetComponentInChildren<MessageBar>();
        hp = hpMax;
    }

    public virtual void Die() {
        Debug.LogWarning("Die not overridden");
        Destroy(gameObject);
    }
}
