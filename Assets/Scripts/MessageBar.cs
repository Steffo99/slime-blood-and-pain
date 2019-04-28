using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBar : MonoBehaviour
{
    public float disappearanceSpeed = 0.3f;
    private float opacity;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        opacity = 0f;
    }

    public void Write(string message, Color color) {
        text.color = color;
        text.text = message;
        opacity = 1f;
    }

    void Update()
    {
        if(opacity > 0f) {
            opacity -= disappearanceSpeed * Time.deltaTime;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
}
