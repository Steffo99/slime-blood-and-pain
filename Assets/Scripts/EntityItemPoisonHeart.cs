using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemPoisonHeart : EntityItem
{
    public int damage;
    protected EntityPlayer player;
    public override string Name {
        get {
            return "Poisonous Heart";
        }
    } 
    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    // Update is called once per frame
    public override void OnPickup(EntityPlayer player) {
        messageBar.Write("You used: " + Name, Color.yellow);
        player.hp-=damage;
        if (player.hp <= 0) player.Die();
        Destroy(gameObject);
    }
}