/*
 * Use for weapon
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_EFFECT { NORMAL, POISON, FREEZE }

[System.Serializable]
public class WeaponEffect
{
    public WEAPON_EFFECT effectType = WEAPON_EFFECT.NORMAL;
    [Header("NORMAL")]
    public float normalDamageMin = 30;
    public float normalDamageMax = 50;

    [Header("POISON")]
    public float poisonTime = 5;
    public float poisonDamagePerSec = 80;
    [Space]
    [Header("FREEZE")]
    public float freezeTime = 5;
    //[Space]
    //[Header("ELECTRIC")]
    //public float electricTime = 5;
    //public float electricDamagePerSec = 80;


}
