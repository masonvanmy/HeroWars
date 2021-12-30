using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [ReadOnly] public static LevelManager Instance;
    public int mana = 1000;


    private void Awake()
    {
        Instance = this;

        if (GameLevelSetup.Instance)
        {
            mana = GameLevelSetup.Instance.GetGivenMana();
        }
    }
}
