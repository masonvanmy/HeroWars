using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMode))]
public class GameModeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("RESET ALL"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("RESET ALL!");
        }
    }
}
