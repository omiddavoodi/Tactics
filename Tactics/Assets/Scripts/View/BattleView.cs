using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleView : MonoBehaviour {

    // Singleton instnace
    static private BattleView _instance = null;
    static public BattleView Instance { get { if (_instance == null) { _instance = new BattleView(); return _instance; } return _instance; } }

    public BattleMapView battleMapView;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupBattle()
    {
        // Setup Map

        // Setup Units

    }

    public void SetupUnits()
    {

    }

    public void PlaceUnit(BattleUnit unit, int x, int y)
    {

    }

    public Vector3 TranslateMapCoordsToWorldPosition(int x, int y)
    {
        Vector3 ret = new Vector3();


        return ret;
    }

    public Quaternion TranslateMapDirectionToWorldRotation(Direction direction)
    {
        Quaternion ret = new Quaternion();


        return ret;
    }
}
