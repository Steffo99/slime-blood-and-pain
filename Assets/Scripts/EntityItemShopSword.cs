using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemShopSword : EntityItemShop {

    public float damage = 3f;

    public override string Name {
        get {
            return "Sword (" + damage.ToString() + " dmg)";
        }
    }

    protected override void OnPurchase() {
        Destroy(player.GetComponent<PlayerAttack>());
        player.AddComponent(PlayerAttackMelee);
        PlayerAttackMelee pam = player.GetComponent<PlayerAttackMelee>();
        pam.damage = this.damage;
    }
}