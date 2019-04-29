using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [BeforeStartAttribute]
    public float hpTrueMax;

    public bool overlappable = false;

    [AfterStartAttribute]
    public float hpMax;

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
        hpMax = hpTrueMax;
        hp = hpMax;
    }

    public virtual void OnNewLevel() {
        Destroy(gameObject);
    }

    public virtual void Die() {
        Debug.LogWarning("Die not overridden");
        Destroy(gameObject);
    }
}
