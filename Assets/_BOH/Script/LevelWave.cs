using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWave : MonoBehaviour
{
    public int level = 1;
    public int givenMana = 1000;
    [Range(1,5)]
    public int enemyFortrestLevel = 1;
    public EnemyWave[] Waves;
}
