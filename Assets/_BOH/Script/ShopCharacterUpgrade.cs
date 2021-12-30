using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterUpgrade : MonoBehaviour
{
    public UpgradedCharacterParameter characterID;
    public bool upgradeHealth, upgradeMelee, upgradeRange, upgradeCrit;

    public Text currentHealth, upgradeHealthStep,
        currentMeleeDamage, upgradeMeleeDamageStep,
        currentRangeDamage, upgradeRangeDamageStep,
        currentCritical, upgradeCriticleStep;

    public Text price, unlockLevel, hitTargetTxt;

    public GameObject dot;
    public GameObject dotHoder;
    List<Image> upgradeDots;

    public Sprite dotImageOn, dotImageOff;
    public GameObject lockedPanel;
    [ReadOnly] public bool isUnlock = false;

    public GameObject poisonFX, freezeFX;
    // Start is called before the first frame update
    void Start()
    {
        hitTargetTxt.text = "HIT TARGET = " + characterID.maxTargetPerHit;
        poisonFX.SetActive(characterID.weaponEffect.effectType == WEAPON_EFFECT.POISON);
        freezeFX.SetActive(characterID.weaponEffect.effectType == WEAPON_EFFECT.FREEZE);

        isUnlock = GlobalValue.LevelPass >= characterID.unlockAtLevel - 1;
        lockedPanel.SetActive(!isUnlock);

        if (!isUnlock)
            unlockLevel.text = characterID.unlockAtLevel + "";


        upgradeDots = new List<Image>();
        upgradeDots.Add(dot.GetComponent<Image>());
        for (int i = 1; i < characterID.UpgradeSteps.Length; i++)
        {
            upgradeDots.Add(Instantiate(dot, dotHoder.transform).GetComponent<Image>());
        }

        UpdateParameter();
    }

    void UpdateParameter()
    {
        currentHealth.text = "HEALTH: " + characterID.UpgradeHealth;
        currentMeleeDamage.text = "DAMAGE: " + characterID.UpgradeMeleeDamage;
        currentRangeDamage.text = "DAMAGE: " + characterID.UpgradeRangeDamage;
        currentCritical.text = "CRIT: " + characterID.UpgradeCriticalDamage;

        if (characterID.CurrentUpgrade != -1)
        {
            price.text = characterID.UpgradeSteps[characterID.CurrentUpgrade].price + "";
            upgradeHealthStep.text = "+" + characterID.UpgradeSteps[characterID.CurrentUpgrade].healthStep;
            upgradeMeleeDamageStep.text = "+" + characterID.UpgradeSteps[characterID.CurrentUpgrade].meleeDamageStep;
            upgradeRangeDamageStep.text = "+" + characterID.UpgradeSteps[characterID.CurrentUpgrade].rangeDamageStep;
            upgradeCriticleStep.text = "+" + characterID.UpgradeSteps[characterID.CurrentUpgrade].criticalStep;

            SetDots(characterID.CurrentUpgrade);
        }
        else
        {
            price.text = "MAX";
            upgradeHealthStep.enabled = false;
            upgradeMeleeDamageStep.enabled = false;
            upgradeRangeDamageStep.enabled = false;
            upgradeCriticleStep.enabled = false;
            
            SetDots(upgradeDots.Count);
        }

    }

    void SetDots(int number)
    {
        for (int i = 0; i < upgradeDots.Count; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;
        }
    }

    public void Upgrade()
    {
        if (!isUnlock)
            return;

        if (characterID.CurrentUpgrade == -1)
            return;

        if(GlobalValue.SavedCoins >= characterID.UpgradeSteps[characterID.CurrentUpgrade].price)
        {
            GlobalValue.SavedCoins -= characterID.UpgradeSteps[characterID.CurrentUpgrade].price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            characterID.UpgradeCharacter(upgradeHealth, upgradeMelee, upgradeRange, upgradeCrit);
            UpdateParameter();
        }
    }
}
