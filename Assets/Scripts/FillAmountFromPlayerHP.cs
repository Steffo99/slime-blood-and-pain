using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountFromPlayerHP : MonoBehaviour
{
    private EntityPlayer player;
    private Image image;
    public bool max;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
        image = GetComponent<Image>();
    }

    private void Update() {
        if(max) {
            image.fillAmount = player.hpMax / player.hpTrueMax;
        }
        else {
            image.fillAmount = player.hp / player.hpTrueMax;
        }
    }
}
