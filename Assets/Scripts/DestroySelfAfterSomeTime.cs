using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterSomeTime : MonoBehaviour {
    [BeforeStartAttribute]
    public float time = 1f;

    protected void Start() {
        Invoke(DestroySelf, time);
    }

    protected void DestroySelf() {
        Destroy(gameObject);
    }
}