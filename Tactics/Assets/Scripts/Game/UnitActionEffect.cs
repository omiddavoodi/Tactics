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

    // Amount Calculation
    public int fixedAmount = 0;
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
    public float flankingBonus = 0.0f;
    public float rearFlankingBonus = 0.0f;



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
