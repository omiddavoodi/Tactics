using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapTile
{
    // Base info
    public int x;
    public int y;
    public int height;

    // Movement info
    public bool impassable = false;
    public int moveCost;

    // Unit info
    public BattleUnit unit;

    // ***** TODO   TEXTURES AND TERRAIN AND STUFF
}

public class BattleMap {

    public int width;
    public int height;

    public BattleMapTile[][] tiles;

    public BattleMapCosmetic[] cosmetics;

    // ***** TODO Cosmetic stuff should go here 


    public string Serialize()
    {
        // ****** TODO IMPLEMENT LATER *********
        return "Implement later";
    }

    public static BattleMap Deserialize(string state)
    {
        // ****** TODO IMPLEMENT LATER *********
        return new BattleMap();
    }
}
