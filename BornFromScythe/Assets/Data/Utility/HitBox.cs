using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool capsule = false;
    public new string name;
    public LayerMask mask;
    public Collider[] colliders;
    List<Collider> hitColliders = new List<Collider>();
    public float size = 1f;

    Color inactiveColor;
    Color collisionOpenColor;
    Color collidingColor;

    private ColliderState state;

    public AudioClip hitSound;
    public AudioClip hitSoundEntity;

    public void startCheckingCollision()
    {
        state = ColliderState.Open;
    }

    public void stopCheckingCollision()
    {
        state = ColliderState.Closed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log("state "+state);
        if(state == ColliderState.Closed)
        {
            hitColliders.Clear();
            return;
        }
        if(!capsule)
        {
            colliders = Physics.OverlapSphere(transform.position, size, mask);
        }
        else
        {
            colliders = Physics.OverlapBox(transform.position, new Vector3(size/2,5*size/2,size/2), gameObject.transform.rotation, mask);
        }
        
        //Debug.Log(colliders.Length);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider c = colliders[i];
                if(c.CompareTag("Player") && this.CompareTag("Player") || c.CompareTag("DarkOath") && this.CompareTag("DarkOath") || c.CompareTag("StrainEntity") && this.CompareTag("StrainEntity")) //found strain entity hurtbox, while this is strain entity hitbox
                {
                    continue;
                }
                
                //GameManager player1 = GetComponentInParent<GameManager>();
                //GameManager player2 = c.GetComponentInParent<GameManager>();

                Debug.Log("Found a collision: "+c.name);
                if(!hitColliders.Contains(c))
                {
                    

                    if(c.GetComponentInParent<EntityData>() != null)
                    {   
                        hitColliders.Add(c);

                        

            /*             player1.gameObject.GetComponent<AudioSource>().PlayOneShot(hitSoundEntity);

                        if(UnityEngine.Random.Range(0,1) < 0.5)
                        {
                            player2.gameObject.GetComponent<AudioSource>().PlayOneShot(player2.hitsound2);
                        }
                        else
                        {
                            player2.gameObject.GetComponent<AudioSource>().PlayOneShot(player2.hitsound1);
                        }
                        

                        Debug.Log(player1.name+" is hitting "+player2+"!");

                    
                        player2.gameObject.GetComponentInChildren<Animator>().SetTrigger("Hit");

                         */

                    }
                    else
                    {
                        gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound);
                    }
                    
                }

                
                state = ColliderState.Colliding;

            }
            
        }
        else
        {
            state = ColliderState.Open;
        }
    }

    private void OnDrawGizmos()
    {
        checkGizmoColor();
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        if(!capsule)
        {
            Gizmos.DrawSphere(Vector3.zero, size);
        }
        else
        {
            Gizmos.DrawCube(Vector3.zero,new Vector3(size,5*size,size));
        }
        
    }

    private void checkGizmoColor()
    {
        switch(state)
        {
            case ColliderState.Closed:
                Gizmos.color = Color.grey;
                break;
            case ColliderState.Open:
                Gizmos.color = Color.red;
                break;
            case ColliderState.Colliding:
                Gizmos.color = Color.magenta;
                break;
        }
    }
}


public enum ColliderState
{
    Closed,
    Open,
    Colliding
}