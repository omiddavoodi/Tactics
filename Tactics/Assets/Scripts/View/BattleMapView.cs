using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapView : MonoBehaviour {

    public Terrain terrain;

	// Use this for initialization
	void Start () {
        //float[,] k = terrain.terrainData.GetHeights(0, 0, 10, 10);
        //print(k.Length);

        /*float[,] k = new float[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                k[i, j] = (i + j)*0.001f;
            }
        }

        terrain.terrainData.SetHeights(0, 0, k);
        */
        //print(terrain.terrainData.);
        float[,,] alphas = new float[terrain.terrainData.alphamapHeight, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapLayers];
        for (int i = 0; i < terrain.terrainData.alphamapHeight; i++)
        {
            for (int j = 0; j < terrain.terrainData.alphamapWidth; j++)
            {
                if (i % 10 == 0 || j % 10 == 0)
                {
                    alphas[i, j, 0] = 0.5f;
                    alphas[i, j, 1] = 0.5f;
                }
                else
                {
                    alphas[i, j, 0] = 1.0f;
                    alphas[i, j, 1] = 0.0f;
                }
            }
        }

        terrain.terrainData.SetAlphamaps(0, 0, alphas);
        
	}
	

    public void SetupTerrain(BattleMap map)
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
