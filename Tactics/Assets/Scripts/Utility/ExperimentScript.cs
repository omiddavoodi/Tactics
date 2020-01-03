using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperimentScript : MonoBehaviour
{
    public GameObject previous = null;
    

    public void AddMeshFromResources()
    {
        if (previous != null)
        {

            GameObject.DestroyImmediate(previous);

        }

        Mesh mesh = (Mesh)Resources.Load("Meshes/Test/Palm_Tree", typeof(Mesh));
        Texture texture = (Texture)Resources.Load("Textures/Test/tree_diffuse", typeof(Texture));
        Material mat = new Material(Shader.Find("Standard"));
        mat.name = "bar";
        mat.SetTexture("_MainTex", texture);
        mat.SetFloat("_Glossiness", 0.0f);
        //print(Shader.PropertyToID("_Glossiness"));
        GameObject newobject = new GameObject();
        MeshFilter mf = newobject.AddComponent<MeshFilter>();
        MeshRenderer mr = newobject.AddComponent<MeshRenderer>();
        mr.sharedMaterial = mat;

        mf.mesh = mesh;
        newobject.name = "New Object!!!";

        newobject.transform.position = transform.position;

        previous = newobject;
    }
}
