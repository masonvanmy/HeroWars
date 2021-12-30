using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class UpgradeStep
{
    public int price;
    public int healthStep, meleeDamageStep, rangeDamageStep, criticalStep;
}

public class UpgradedCharacterParameter : MonoBehaviour
{
    public string ID = "unique ID";
    public int unlockAtLevel = 1;

    [Header("EFFECT")]
    public WeaponEffect weaponEffect;
    [Space]
    public UpgradeStep[] UpgradeSteps;

    [Header("MELEE ATTACK")]
    public int maxTargetPerHit = 1; //how many target damaged per hit

    [Header("Default")]
    public int defaultHealth = 100;
    public int meleeDamage = 100;
    public int rangeDamage = 100;
    [Range(1, 100)]
    public int criticalDamagePercent = 10;

    public int CurrentUpgrade
    {
        get
        {
            int current = PlayerPrefs.GetInt(ID + "upgradeHealth" + "Current", 0);
            if (current >= UpgradeSteps.Length)
                current = -1;   //-1 mean overload
            return current;
        }
        set
        {
            PlayerPrefs.SetInt(ID + "upgradeHealth" + "Current", value);
        }
    }

    public void UpgradeCharacter(bool health, bool melee, bool range, bool crit)
    {
        if (CurrentUpgrade == -1)
            return;
        
        if (health)
        {
            UpgradeHealth += UpgradeSteps[CurrentUpgrade].healthStep;
        }

        if (melee)
        {
            UpgradeMeleeDamage += UpgradeSteps[CurrentUpgrade].meleeDamageStep;
        }

        if (range)
        {
            UpgradeRangeDamage += UpgradeSteps[CurrentUpgrade].rangeDamageStep;
        }

        if (crit)
        {
            UpgradeCriticalDamage += UpgradeSteps[CurrentUpgrade].criticalStep;
        }
        
        CurrentUpgrade++;
    }

    public int UpgradeHealth
    {
        get { return PlayerPrefs.GetInt(ID + "upgradeHealth", defaultHealth); }
        set { PlayerPrefs.SetInt(ID + "upgradeHealth", value); }
    }

    public int UpgradeMeleeDamage
    {
        get { return PlayerPrefs.GetInt(ID + "UpgradedMeleeDamage", meleeDamage); }
        set { PlayerPrefs.SetInt(ID + "UpgradedMeleeDamage", value); }
    }
    
    public int UpgradeRangeDamage
    {
        get { return PlayerPrefs.GetInt(ID + "UpgradeRangeDamage", rangeDamage); }
        set { PlayerPrefs.SetInt(ID + "UpgradeRangeDamage", value); }
    }
   
    public int UpgradeCriticalDamage
    {
        get { return PlayerPrefs.GetInt(ID + "UpgradeCriticalDamage", criticalDamagePercent); }
        set { PlayerPrefs.SetInt(ID + "UpgradeCriticalDamage", value); }
    }
}
