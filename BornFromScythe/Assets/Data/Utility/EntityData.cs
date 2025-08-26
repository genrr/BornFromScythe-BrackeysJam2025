using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    Move moveInProgress = null;
    public List<Move> moves;
    public List<HitBox> hitboxes = new List<HitBox>();
    public int SC = 1;
    public int currentSigil = 0;
    public List<int> sigils;
    public List<float> sigStabilities;
    public float HP = 50;
    public float maxHP = 50;
    public float energy = 13;
    public float maxEnergy = 13;
    public List<EntityData> nbdEntities = new();
    public List<EntityState> states = new();
    public delegate void OnHUDChanged();
    public OnHUDChanged onHUDChangedCallback;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SC <= 0)
        {
            States.gameRunning = false;
        }
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));
        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;
        Debug.DrawRay(transform.position, transform.forward, Color.blue);


        if (Physics.Raycast(ray, out hit, 1, layerMask))
        {
            var entity = hit.collider.GetComponent<EntityData>();

            if (entity != null && !nbdEntities.Contains(entity))
            {
                nbdEntities.Add(entity);
                Debug.Log("entity found!");
            }
        }
    }

    public Move GetPlayerMove()
    {
        return moveInProgress;
    }

    public void SetPlayerMove(Move move)
    {
        moveInProgress = move;
    }

    public void ModifyStats(int selector, int value)
    {

        if (selector == 0)
        {
            HP += value;
        }
        else if (selector == 1)
        {
            energy += value;
        }
        if (onHUDChangedCallback != null)
        {
            onHUDChangedCallback.Invoke();
        }

    }
}

public enum EntityState
{
    Attacking,
    Walking,
    Running,
    Idle,
    Dashing,
    Deflecting,
    Blocking,
    Knockdown,
    Paralyzed
}
