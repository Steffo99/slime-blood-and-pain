using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFromPlayerHP : MonoBehaviour
{
    private EntityPlayer player;
    private Text text;
    public bool max;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
        text = GetComponent<Text>();
    }

    private void Update() {
        if(max) {
            text.text = "/ " + player.hpMax.ToString("0.0");
        }
        else {
            text.text = player.hp.ToString("0.0");
        }
    }
}
