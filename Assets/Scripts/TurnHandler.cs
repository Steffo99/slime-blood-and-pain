using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public void OnTurn() {
        AI[] ais = gameObject.GetComponentsInChildren<AI>();
        foreach(AI ai in ais) {
            ai.OnTurn();
        }
    }

    public List<Entity> GetEntityAtPosition(Vector2Int position) {
        Entity[] entities = GetComponentsInChildren<Entity>();
        List<Entity> found = new List<Entity>();
        foreach(Entity entity in entities) {
            if(entity.MapPosition == position) {
                found.Add(entity);
            }
        }
        return found;
    }
}