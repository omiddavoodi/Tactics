using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameState {

    public BattleMap battleMap;
    public List<BattleUnit> units;


    public string Serialize()
    {
        // ****** TODO IMPLEMENT LATER *********
        return "Implement later";
    }

    public static BattleGameState Deserialize(string state)
    {
        // ****** TODO IMPLEMENT LATER *********
        return new BattleGameState();
    }
}
