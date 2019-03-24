using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitController {

    public BattleController battleController;

    public readonly BattleUnit unit;

    private bool doneMovement = false;
    private bool doneAction = false;
    private bool endedTurn = false;



    public BattleUnitController(BattleUnit unit)
    {
        this.unit = unit;
    }


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
        doneMovement = false;
        doneAction = false;
        endedTurn = false;
    }



    // What happens when ordered to move to a new location
    public bool Move(int x, int y)
    {
        if (!endedTurn)
        {
            // Check if has not moved yet
            if (!doneMovement)
            {
                List<BattleMapTile> path = battleController.GetPath(unit.x, unit.y, x, y);
                // if there is a path and we are not already there
                if (path.Count > 1)
                {
                    BattleMapTile tile1 = path[path.Count - 2];
                    BattleMapTile tile2 = path[path.Count - 1];

                    if (tile1.x > tile2.x)
                    {
                        unit.direction = Direction.Left; 
                    }
                    else if (tile1.x < tile2.x)
                    {
                        unit.direction = Direction.Right;
                    }
                    else if (tile1.y > tile2.y)
                    {
                        unit.direction = Direction.Down;
                    }
                    else if (tile1.y < tile2.y)
                    {
                        unit.direction = Direction.Up;
                    }
                    else
                    {
                        Debug.LogError("Pathfinding bug: Last two tiles are the same.");
                        return false;
                    }

                    unit.x = x;
                    unit.y = y;
                    unit.tile.unit = null;
                    battleController.map.tiles[y][x].unit = unit;
                    doneMovement = true;

                    // TODO: IMPLEMENT GRAPHICS OF THIS

                    return true;
                }
                else
                {
                    Debug.LogError("No path to reach destination");
                }
            }
            else
            {
                Debug.LogError("Already moved");
            }
        }
        else
        {
            Debug.LogError("Already ended turn");
        }
        return false;
    }

    // What happens when ordered to perform an action
    public bool DoAction(int actionid, int x, int y, Direction direction)
    {
        // TODO   IMPLEMENT
        if (!endedTurn)
        {
            if (!doneAction)
            {
                if (unit.baseUnit.actions.ContainsKey(actionid))
                {
                    UnitAction action = unit.baseUnit.actions[actionid];
                    if (action.areaType == ActionAreaType.Global)
                    {
                        // TODO IMPLEMENT
                    }
                    else if (action.areaType == ActionAreaType.LocalDirected)
                    {
                        unit.direction = direction;
                        foreach (UnitActionEffect effect in action.effects)
                        {
                            List<BattleUnit> units = GetUnitsInLocalAreaForActionEffect(x, y, direction, action.areaType, effect, battleController.map);
                        }
                            
                    }
                    else if (action.areaType == ActionAreaType.LocalUndirected)
                    {

                    }
                    
                }

                return true;
            }
            else
            {
                Debug.LogError("Already done an action");
            }
        }
        else
        {
            Debug.LogError("Already ended turn");
        }
        return false;
    }

    // What happens when ordered to end its turn
    public bool Wait(Direction direction)
    {
        if (!endedTurn)
        {
            unit.direction = direction;
            endedTurn = true;


            // TODO: IMPLEMENT GRAPHICS OF THIS

            return true;
        }
        else
        {
            Debug.LogError("Already ended turn");
        }
        return false;
    }


    public int GetStat(int id)
    {
        // TODO   IMPLEMENT MODIFIERS
        if (unit.baseUnit.stats.ContainsKey(id))
        {
            return unit.baseUnit.stats[id].amount;
        }
        else
        {
            Debug.LogError("No stat for base unit " + unit.baseUnit.id.ToString() + " with id=" + id.ToString());
        }
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







    // Static logic

    public static List<BattleUnit> GetUnitsInLocalAreaForActionEffect(int x, int y, Direction direction, ActionAreaType areaType, UnitActionEffect effect, BattleMap map)
    {
        List<BaseUnit> ret = new List<BaseUnit>();

        // Area mask is always thought of as being rotated towards right
        if (direction == Direction.Right)
        {
            for (int i = Mathf.Max(0, ))
        }


        return ret;
    }
}
