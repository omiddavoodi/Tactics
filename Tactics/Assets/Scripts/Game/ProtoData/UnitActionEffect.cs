using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public enum ActionEffectType
{
    Damage,
    Heal,
    Pushback,
}

public enum ActionEffectTarget
{
    ActionTargets,
    Self,

}

public class UnitActionEffect {

    public int id;
    public ActionEffectType type;
    public ActionEffectTarget targetType;

    // Area
    public int areaCenterX;
    public int areaCenterY;
    public int areaWidth;
    public int areaHeight;
    public bool[][] areaMask;

    // Amount Calculation
    // Base fixed amount
    public int fixedAmount = 0;

    // This ratio is multiplied by the relevant stat and added to the damage
    public float targetMaxHealthRatio = 0.0f;
    public float targetCurrentHealthRatio = 0.0f;
    public float targetMaxStaminaRatio = 0.0f;
    public float targetCurrentStaminaRatio = 0.0f;
    public float targetMovementRatio = 0.0f;
    public List<KeyValuePair<int, float>> targetStatsRatio = new List<KeyValuePair<int, float>>();
    public float selfMaxHealthRatio = 0.0f;
    public float selfCurrentHealthRatio = 0.0f;
    public float selfMaxStaminaRatio = 0.0f;
    public float selfCurrentStaminaRatio = 0.0f;
    public float selfMovementRatio = 0.0f;
    public List<KeyValuePair<int, float>> selfStatsRatio = new List<KeyValuePair<int, float>>();

    // These bonuses are applied in the end by multiplying them on the results after everything above is applied first
    public float flankingBonus = 1.0f;
    // Is compleetely separate from flanking. A unit is either flanking or rear-flanking another unit, not both at the same time
    public float rearFlankingBonus = 1.0f;

    // TODO Conditions

    public string Serialize()
    {
        // ****** TODO IMPLEMENT LATER *********
        return "Implement later";
    }

    public static UnitActionEffect Deserialize(string state)
    {
        // ****** TODO IMPLEMENT LATER *********
        return new UnitActionEffect();
    }
}
