using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// For testing
[CustomEditor(typeof(ExperimentScript))]
public class ExperimentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ExperimentScript myScript = (ExperimentScript)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.AddMeshFromResources();
        }
    }
}
