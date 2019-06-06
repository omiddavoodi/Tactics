using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitView : MonoBehaviour {

    public BattleUnitController unit;
    public GameObject mesh;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void MoveInstantly(int x, int y)
    {
        this.transform.position = BattleView.Instance.TranslateMapCoordsToWorldPosition(x, y);
    }

    public void ChangeDirectionInstantly(Direction direction)
    {
        this.transform.rotation = BattleView.Instance.TranslateMapDirectionToWorldRotation(direction);
    }

    public IEnumerator MoveTo(int x, int y)
    {
        // Pathfinding to get the path

        // Move tile by tile to reach destination
        // Change rotation for each tile

        yield return null;

        // Failsafe
        this.MoveInstantly(x, y);
    }

    public IEnumerator FaceDirection(Direction direction)
    {
        // Change rotation to match direction

        yield return null;
        this.ChangeDirectionInstantly(direction);
    }
}
