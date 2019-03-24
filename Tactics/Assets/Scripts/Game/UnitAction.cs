using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionAreaType
{
    LocalDirected,
    LocalUndirected,
    Global
}

public class UnitAction {

    public int id;

    public string name;

    public int minRange;
    public int maxRange;

    // Area
    public ActionAreaType areaType;
    public int areaCenterX;
    public int areaCenterY;
    public int areaWidth;
    public int areaHeight;
    public bool[][] areaMask;

    // Effects
    public UnitActionEffect[] effects;

    public string Serialize()
    {
        // ****** TODO IMPLEMENT LATER *********
        return "Implement later";
    }

    public static UnitAction Deserialize(string state)
    {
        // ****** TODO IMPLEMENT LATER *********
        return new UnitAction();
    }
}
