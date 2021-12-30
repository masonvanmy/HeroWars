using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    //public GameObject[] listCharacter;
   
    void Start()
    {
        Instance = this;
    }

   
    public GameObject SpawnCharacter(GameObject character)
    {
       return Instantiate(character, transform.position, Quaternion.identity) as GameObject;
    }
}
