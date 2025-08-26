using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<EntityData> entities = new();
    bool attacking = false;
    int entityId = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entities = GameObject.FindObjectsByType<EntityData>(FindObjectsSortMode.None).ToList<EntityData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!States.gameRunning)
        {
            Time.timeScale = 0;
            GetComponent<HUDManager>().gameMsg.SetActive(true);
        }


        foreach (EntityData e in entities)
        {
            if (e.nbdEntities.Count != 0)
            {
                if (e.states.Contains(EntityState.Attacking))
                {
                    attacking = true;
                    entityId = e.GetHashCode();
                }
            }
        }

    }
}

public enum SigilType
{
    Moon,
    Ir,
    Sky
}