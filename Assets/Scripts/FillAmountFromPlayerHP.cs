using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountFromPlayerHP : MonoBehaviour
{
    private Player player;
    private Image image;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        image = GetComponent<Image>();
    }

    private void Update() {
        image.fillAmount = player.hp / player.hpMax;
    }
}
