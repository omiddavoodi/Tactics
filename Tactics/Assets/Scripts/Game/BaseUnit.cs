using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit {

    public int id;

    // Hardcoded Stats
    public int maxHealth; // Max HP
    public int maxStamina; // Max Stamina
    public int mov; // Movements per turn


    // Softcoded Stats
    public Dictionary<int, UnitStat> stats;

    // Misc
    public string name;

    // Features
    // Possible Actions
    public Dictionary<int, UnitAction> actions;
    
    // Graphics
    // Sounds
    // Traits




    // TODO Make one from a base unit
    //public static BattleUnit FromBaseUnit(BaseUnit base)
    //{
    //}


    public string Serialize()
    {
        // ****** TODO IMPLEMENT LATER *********
        return "Implement later";
    }

    public static BattleUnit Deserialize(string state)
    {
        // ****** TODO IMPLEMENT LATER *********
        return new BattleUnit();
    }
}
