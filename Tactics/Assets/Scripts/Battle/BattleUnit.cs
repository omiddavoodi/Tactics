using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is the model for units in battle. See BaseUnit for units data in general
public class BattleUnit {

    public int id;

    // Stats
    public int maxHealth; // Max HP
    public int health; // HP
    public int maxStamina; // Max Stamina
    public int stamina; // Stamina
    public int str; // Strength
    public int def; // Defense
    public int spd; // Speed
    public int wll; // Willpower
    public int res; // Resistance
    public int mov; // Movements per turn
    public int exp; // Experience

    // Misc
    public string name;

    // Status
    // Possible Actions
    // Moveset
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
