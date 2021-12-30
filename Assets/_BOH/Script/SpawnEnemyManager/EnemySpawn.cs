using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public float wait = 3;      //delay for first enemy
    public GameObject enemy;    //enemy spawned
    public int numberEnemy = 5;     //the number of enemy need spawned
    public float rate = 1;  //time delay spawn next enemy
}
