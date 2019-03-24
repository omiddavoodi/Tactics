using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitController {

    private BattleUnit unit;

    private bool doneMovement = false;
    private bool doneAction = false;
    private bool endedTurn = false;

    public bool hasDoneMovement()
    {
        return doneMovement;
    }

    public bool hasDoneAction()
    {
        return doneAction;
    }

    public bool hasEndedTurn()
    {
        return endedTurn;
    }


    // What happens when it is this unit's turn
    public void NewTurn()
    {

    }

    // What happens when ordered to move to a new location
    public bool Move()
    {
        // TODO   IMPLEMENT
        return true;
    }

    // What happens when ordered to perform an action
    public bool DoAction()
    {
        // TODO   IMPLEMENT
        return true;
    }

    // What happens when ordered to end its turn
    public bool Wait()
    {
        // TODO   IMPLEMENT
        return true;
    }


    public int GetStat()
    {
        // TODO   IMPLEMENT MODIFIERS
        return 0;
    }

    public int GetMaxHealth()
    {
        // TODO   IMPLEMENT MODIFIERS
        return unit.baseUnit.maxHealth;
    }

    public int GetMaxStamina()
    {
        // TODO   IMPLEMENT MODIFIERS
        return unit.baseUnit.maxStamina;
    }

    public int GetMovements()
    {
        // TODO   IMPLEMENT MODIFIERS
        return unit.baseUnit.maxStamina;
    }

    public int GetHealth()
    {
        return unit.health;
    }

    public int GetStamina()
    {
        return unit.stamina;
    }
}
