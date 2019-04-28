using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityItemHeart : EntityItem
{
    public int regen;
    protected EntityPlayer player;
    public override string Name {
        get {
            return "Heart";
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
        player.hp+=regen;
        if (player.hp > player.hpMax) player.hp = player.hpMax;
        Destroy(gameObject);
    }
}
