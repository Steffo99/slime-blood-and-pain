using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mostro : MonoBehaviour
{
    public string nome;
    public Sprite sprite;
    public int hpmax, hp, exp;
    public bool is_undead;
    public enum speed {slow, standard, fast};
    private bool player_spotted;
    public Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = gameObjectMappa.GetComponent<Map>();
        player_spotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_spotted){
            roam();
        }
        //Da mettere il pathfinding per quando trova il personaggio
    }
    void roam(){
        while (true)
        {
            int direction = Random.Range(1, 5);
            if (direction==1 && is_valid_movement("left")){
                transform.Translate(Vector3.left);
                break;
            }
            else if (direction==2 && is_valid_movement("right")){
                transform.Translate(Vector3.right);
                break;
            }
            else if (direction==3 && is_valid_movement("up")){
                transform.Translate(Vector3.up);
                break;
            }
            else if (is_valid_movement("down")){
                transform.Translate(Vector3.up);
                break;
            }
        }
    }
    bool is_valid_movement(string direction)
    {
        Tile tile;
        int posX = (int) transform.position.x;
        int posY = (int) transform.position.y;
        if (direction == "left") tile = map.GetTile(posX - 1, posY);
        else if (direction == "right") tile = map.GetTile(posX + 1, posY);
        else if (direction == "up") tile = map.GetTile(posX, posY + 1);
        else tile = map.GetTile(posX, posY - 1);
        return tile.walkable;
    }
}
