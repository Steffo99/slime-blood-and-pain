using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hpMax;

    [AfterStartAttribute]
    public int hp;

    [BeforeStartAttribute]
    public Sprite sprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        hp = hpMax;
    }
}
