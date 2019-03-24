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

    public bool requiresTargets;

    // Area
    public ActionAreaType areaType;

    // Effects
    public UnitActionEffect[] effects;

    // Costs
    public int healthCost;
    public int staminaCost;

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
