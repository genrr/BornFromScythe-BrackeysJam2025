using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Move", menuName = "Gameplay/Move")]
public class Move : ScriptableObject
{
    public string animName;
    public MoveType type;
    public MoveClass moveClass;
    public Image image;
    public string moveName;
    public string inputString;
    public float moveMult;
    public int baseCeCost;
    public int scaling;

    public string frameData;
    public int totalFrames;
    public List<string> usedHitboxes;



/*     public Move(Image image, int damage, Hitbox hitbox, string moveName)
    {
        this.image = image;
        this.damage = damage;
        this.hitbox = hitbox;
        this.moveName = moveName;
    } */

    public void attack()
    {
        
    }

/*     public void collidedWith(Collider collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        
    } */




}

public enum MoveType
{
    Offensive,
    Defensive,
    Utility
}

public enum MoveClass
{
    Normal,
    Unarmed,
    Planar,
    Niyomic
}

public enum SigilSlot
{
    Mind,
    LEye,
    REye,
    Storage,
    Cortex,
    Forehead,
    Chest,
    Stomach,
    Lhand,
    LHandExt,
    Rhand,
    RHandExt,
    Lleg,
    Rleg,
    HandAux,
    DualWield,
    BackExtension

}