using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float hpMax;
    public bool overlappable = false;

    [AfterStartAttribute]
    public float hp;

    [BeforeStartAttribute]
    public Sprite sprite;

    public Vector2Int MapPosition {
        get {
            return new Vector2Int((int)transform.position.x, (int)transform.position.y);
        }
    }

    protected GameObject gameController;
    protected SpriteRenderer spriteRenderer;
    protected TurnHandler turnHandler;
    protected Map map;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        turnHandler = gameController.GetComponentInChildren<TurnHandler>();
        map = gameController.GetComponentInChildren<Map>();
        hp = hpMax;
    }
}
