using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public void OnTurn() {
        Entity[] entities = gameObject.GetComponentsInChildren<Entity>();
        foreach(Entity entity in entities) {
            //Check for deaths
            if(entity.hp <= 0) {
                entity.Die();
            }
            //Move AIs
            if(entity is AI) {
                AI ai = entity as AI;
                ai.OnTurn();
            }
        }
    }

    public List<Entity> GetEntitiesAtPosition(Vector2Int position) {
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