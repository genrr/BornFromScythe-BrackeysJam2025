using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

/**
    Class to handle Move animations, Sigil & Token calculations and state transitions of Moves


*/

public class MeleeSystem : MonoBehaviour
{
    private static Animator anim; 

    static Move move = null;
    static List<HitBox> hitboxes;
    HitBox h;

    int start;
    int end;
    bool attacking = false;

    void Start()
    {
/*         axisInputs = new List<int>();
        axisInputs.Add(6);
        axisInputs.Add(8);
        axisInputs.Add(13);
        axisInputs.Add(14);
        axisInputs.Add(15);
        axisInputs.Add(16);
        axisInputs.Add(17);
        axisInputs.Add(18);
        axisInputs.Add(19);
        axisInputs.Add(20); */
        anim = gameObject.GetComponentInChildren<Animator>();
        hitboxes = GetComponent<EntityData>().hitboxes;
        h = hitboxes[0];
    }
    
    void Update()
    {
        
        
        

        if(move != null && !anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Armature|Idle"))
        {
            int frame = Utility.GetFrame(anim, Utility.GetCurrentProgress(anim));
            //Debug.Log(h);
                if(frame >= start)
                {
                    if(frame <= end)
                    {
                        h.startCheckingCollision();
                        //Debug.Log("collision detection active for hitbox "+h.name+"!");
                    }
                    else
                    {
                        h.stopCheckingCollision();
                        //Debug.Log("collision detection deactivated for hitbox "+h.name+"!");
                        
                        attacking = false;       

                        //Debug.Log(frame);
                        if(frame == move.totalFrames){
                            GetComponent<EntityData>().SetPlayerMove(null);
                            
                        }        
                        //move = null;    
                    }
                }
                
            
        }

            
        
    }



    //perform animation of the Move

    //if move is off/util, activate hitBoxes using Moves frameData and check for collisions
    //and move is hit, compute & subtract ac
    //compute Target Token, compute result of players Token vs. target Token, compute tokenRolls
    //determine state changes

    //if move is def, compute & subtract ac, compute tokenRolls
    //determine state changes

    public bool ExecuteMove(Move m)
    {
        
        //set sigils in Moves Sigilslots as the Tokens Sigils
        //foreach (var item in m.usedSlots)
        //{
            
            //this.GetComponent<GameManager>().currentToken.tokenSigils.Add(this.GetComponent<GameManager>().equippedSigils[(int)item]);
        //}

        

        
        string[] frames;

        frames = m.frameData.Split("-");

        foreach(string frameInstruction in frames)
        {
            //Debug.Log(frameInstruction);
            string[] temp = frameInstruction.Split("@");

            start = int.Parse(temp[0].Split(",")[0]);
            end = int.Parse(temp[0].Split(",")[1]);

            //h = hitboxes[int.Parse(temp[1])];
        }
        move = m;
        //Debug.Log(h);
        attacking = true;
        anim.SetTrigger(m.animName);
        
        //anim.Play(m.animName);


        //subtract ac

        //GameManager playerData = player.GetComponent<GameManager>();
        

        return true;
    }


}

