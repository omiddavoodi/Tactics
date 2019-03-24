using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

// This class is the model for units in battle. See BaseUnit for units data in general
public class BattleUnit {

    public int battleid;

    public BaseUnit baseUnit;

    // Stats
    public int health; // HP
    public int stamina; // Stamina

    // Status effects

    // Positioning
    public int x;
    public int y;
    public Direction direction; // 0: up, 1: right, 2: down, 3:left

    public BattleMapTile tile;
    public BattleMap map;

    
        
    public static BattleUnit FromBaseUnit(BaseUnit baseUnit)
    {
        BattleUnit ret = new BattleUnit();

        ret.baseUnit = baseUnit;

        // BattleUnit specific stuff
        ret.health = baseUnit.maxHealth;
        ret.stamina = baseUnit.maxStamina;

        return ret;
    }


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
