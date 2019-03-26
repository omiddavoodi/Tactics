using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitController {

    public BattleController battleController;

    public readonly BattleUnit unit;

    private bool doneMovement = false;
    private bool doneAction = false;
    private bool endedTurn = false;

    private int originalX;
    private int originalY;
    private Direction originalDirection;


    public BattleUnitController(BattleUnit unit)
    {
        this.unit = unit;
        originalDirection = unit.direction;
        originalX = unit.x;
        originalY = unit.y;
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
        if (unit.isAlive)
        {
            doneMovement = false;
            doneAction = false;
            endedTurn = false;

            originalDirection = unit.direction;
            originalX = unit.x;
            originalY = unit.y;
        }
    }


    public bool UndoMove()
    {
        
        if (!endedTurn && !doneAction && doneMovement && unit.isAlive)
        {
            unit.direction = originalDirection;
            unit.x = originalX;
            unit.y = originalY;

            // TODO: UPDATE VISUALS

            return true;
        }
        else if (endedTurn)
        {
            Debug.LogError("Unit has already ended its turn");
        }
        else if (doneAction)
        {
            Debug.LogError("Unit has already performed an action");
        }
        else if (!doneMovement)
        {
            Debug.LogError("Unit has yet to move");
        }
        else if (!unit.isAlive)
        {
            Debug.LogError("Unit is dead");
        }
        return false;
    }

    // What happens when ordered to move to a new location
    public bool Move(int x, int y)
    {
        if (unit.isAlive)
        {
            if (!endedTurn)
            {
                // Check if has not moved yet
                if (!doneMovement)
                {
                    // Check if destination is empty
                    if (battleController.map.tiles[y][x].unit == null)
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

                            // TODO: UPDATE VISUALS

                            return true;
                        }
                        else
                        {
                            Debug.LogError("No path to reach destination");
                        }
                    }
                    else
                    {
                        Debug.LogError("Destination has a unit already");
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
        }
        else
        {
            Debug.LogError("Unit is dead");
        }
        return false;
    }

    // What happens when ordered to perform an action
    public bool DoAction(int actionid, int x, int y, Direction direction)
    {
        if (unit.isAlive)
        {
            if (!endedTurn)
            {
                if (!doneAction)
                {
                    if (unit.baseUnit.actions.ContainsKey(actionid))
                    {
                        UnitAction action = unit.baseUnit.actions[actionid];
                        bool targettedSomeone = false;
                        if (action.areaType == ActionAreaType.Global)
                        {
                            foreach (UnitActionEffect effect in action.effects)
                            {
                                List<BattleUnit> units = GetUnitsInLocalAreaForActionEffect(unit.x, unit.y, unit.direction, action.areaType, effect, battleController.map);
                                foreach (var target in units)
                                {
                                    ApplyActionEffect(effect, target.controller);
                                    targettedSomeone = true;
                                    // TODO: UPDATE VISUALS

                                }
                            }
                        }
                        else if (action.areaType == ActionAreaType.LocalDirected)
                        {
                            unit.direction = direction;
                            foreach (UnitActionEffect effect in action.effects)
                            {
                                List<BattleUnit> units = GetUnitsInLocalAreaForActionEffect(unit.x, unit.y, direction, action.areaType, effect, battleController.map);
                                foreach (var target in units)
                                {
                                    ApplyActionEffect(effect, target.controller);
                                    targettedSomeone = true;
                                    // TODO: UPDATE VISUALS
                                }
                            }

                        }
                        else if (action.areaType == ActionAreaType.LocalUndirected)
                        {
                            int dist = Mathf.Abs(unit.x - x + unit.y - y);
                            if (dist <= action.maxRange && dist >= action.minRange)
                            {
                                foreach (UnitActionEffect effect in action.effects)
                                {
                                    List<BattleUnit> units = GetUnitsInLocalAreaForActionEffect(x, y, direction, action.areaType, effect, battleController.map);
                                    foreach (var target in units)
                                    {
                                        ApplyActionEffect(effect, target.controller);
                                        targettedSomeone = true;
                                        // TODO: UPDATE VISUALS
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogError("Local undirected action attempted out of range");
                                return false;
                            }
                        }

                        if (!targettedSomeone)
                        {
                            Debug.LogError("Action attempted without valid targets");
                            return false;
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
        }
        else
        {
            Debug.LogError("Unit is dead");
        }
        return false;
    }

    // What happens when ordered to end its turn
    public bool Wait(Direction direction)
    {
        if (unit.isAlive)
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
        }
        else
        {
            Debug.LogError("Unit is dead");
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

    // Damages this unit. If it dies, return true. otherwise, returns false
    public bool TakeDamage(int amount, BattleUnitController attacker)
    {
        unit.health = Mathf.Max(0, unit.health - amount);

        // TODO: IMPLEMENT VISUALS
        if (unit.health == 0)
        {
            // TODO: IMPLEMENT ALL KINDS OF DEATH OPTIONS
            // TODO: IMPLEMENT VISUALS
            // TODO: IMPLEMENT UNIT EVENTS LIKE OnDeath and such
            battleController.map.tiles[unit.y][unit.x] = null;

            return true;
        }

        return false;
    }

    public int GetHealed(int amount, BattleUnitController healer)
    {
        if (unit.isAlive)
        {
            int h = unit.baseUnit.maxHealth - unit.health;
            if (h < amount)
            {
                unit.health = unit.baseUnit.maxHealth;
                return h;
            }
            else
            {
                unit.health += amount;
                return amount;
            }
        }
        else
        {
            Debug.LogError("Unit is dead");
        }
        return 0;
    }

    // Apply an effect of an action performed by this unit
    public void ApplyActionEffect(UnitActionEffect effect, BattleUnitController target)
    {
        float temp = 0;
        temp += effect.fixedAmount;

        temp += effect.selfCurrentHealthRatio * GetHealth();
        temp += effect.selfCurrentStaminaRatio * GetStamina();
        temp += effect.selfMaxHealthRatio * GetMaxHealth();
        temp += effect.selfMaxStaminaRatio * GetMaxStamina();
        temp += effect.selfMovementRatio * GetMovements();
        foreach (var stat in effect.selfStatsRatio)
        {
            temp += stat.Value * GetStat(stat.Key);
        }

        temp += effect.targetCurrentHealthRatio * target.GetHealth();
        temp += effect.targetCurrentStaminaRatio * target.GetStamina();
        temp += effect.targetMaxHealthRatio * target.GetMaxHealth();
        temp += effect.targetMaxStaminaRatio * target.GetMaxStamina();
        temp += effect.targetMovementRatio * target.GetMovements();
        foreach (var stat in effect.targetStatsRatio)
        {
            temp += stat.Value * target.GetStat(stat.Key);
        }

        int amount = (int)temp;
        if (amount < 0)
        {
            amount = 0;
        }

        if (effect.type == ActionEffectType.Damage)
        {
            target.TakeDamage(amount, this);
        }
        else if (effect.type == ActionEffectType.Heal)
        {
            target.GetHealed(amount, this);
        }
        else if (effect.type == ActionEffectType.Pushback)
        {

        }
    }




    // Static logic


    public static List<BattleMapTile> GetTilesInLocalAreaForActionEffect(int x, int y, Direction direction, ActionAreaType areaType, UnitActionEffect effect, BattleMap map)
    {
        List<BattleMapTile> ret = new List<BattleMapTile>();

        // Area mask is always thought of as being rotated towards right
        if (direction == Direction.Right)
        {
            int minx = Mathf.Max(0, x - effect.areaCenterX);
            int maxx = Mathf.Min(map.width - 1, minx + effect.areaWidth - 1);
            int miny = Mathf.Max(0, y - effect.areaCenterY);
            int maxy = Mathf.Min(map.height - 1, miny + effect.areaHeight - 1);

            // We have to use these for calculating the correct index for the masks when the unit is too close to the edge od the map 
            int minxh = -Mathf.Min(0, x - effect.areaCenterX);
            int minyh = -Mathf.Min(0, y - effect.areaCenterY);

            for (int i = minx; i <= maxx; ++i)
            {
                for (int j = miny; j <= maxy; ++j)
                {
                    if (effect.areaMask[j - miny + minyh][i - minx + minxh] == true)
                    {
                        ret.Add(map.tiles[j][i]);
                    }
                }
            }
        }
        // This is basically a complete 180 degree turn from the previous one
        else if (direction == Direction.Left)
        {
            int minx = Mathf.Max(0, x - (effect.areaWidth - effect.areaCenterX - 1));
            int maxx = Mathf.Min(map.width - 1, x + effect.areaCenterX);
            int miny = Mathf.Max(0, y - (effect.areaHeight - effect.areaCenterY - 1));
            int maxy = Mathf.Min(map.height - 1, y + effect.areaCenterY);

            // We have to use these for calculating the correct index for the masks when the unit is too close to the edge od the map 
            int minxh = -Mathf.Min(0, x - (effect.areaWidth - effect.areaCenterX - 1));
            int minyh = -Mathf.Min(0, y - (effect.areaHeight - effect.areaCenterY - 1));

            for (int i = minx; i <= maxx; ++i)
            {
                for (int j = miny; j <= maxy; ++j)
                {
                    if (effect.areaMask[effect.areaHeight - (j - miny + minyh) - 1][effect.areaWidth - (i - minx + minxh) - 1] == true)
                    {
                        ret.Add(map.tiles[j][i]);
                    }
                }
            }
        }
        // This is a 90 degree rotation of the first one
        else if (direction == Direction.Down)
        {
            int minx = Mathf.Max(0, x - (effect.areaHeight - effect.areaCenterY - 1));
            int maxx = Mathf.Min(map.width - 1, x + effect.areaCenterY);
            int miny = Mathf.Max(0, y - effect.areaCenterX);
            int maxy = Mathf.Min(map.height - 1, miny + effect.areaWidth - 1);

            // We have to use these for calculating the correct index for the masks when the unit is too close to the edge od the map 
            int minxh = -Mathf.Min(0, x - (effect.areaHeight - effect.areaCenterY - 1));
            int minyh = -Mathf.Min(0, y - effect.areaCenterX);

            for (int i = minx; i <= maxx; ++i)
            {
                for (int j = miny; j <= maxy; ++j)
                {
                    if (effect.areaMask[effect.areaHeight - (i - minx + minxh) - 1][j - miny + minyh] == true)
                    {
                        ret.Add(map.tiles[j][i]);
                    }
                }
            }
        }
        // This is a 270 degree rotation of the first one
        else if (direction == Direction.Up)
        {
            int minx = Mathf.Max(0, x - effect.areaCenterY);
            int maxx = Mathf.Min(map.width - 1, minx + effect.areaHeight - 1);
            int miny = Mathf.Max(0, y - (effect.areaWidth - effect.areaCenterX - 1));
            int maxy = Mathf.Min(map.height - 1, y + effect.areaCenterX);

            // We have to use these for calculating the correct index for the masks when the unit is too close to the edge od the map 
            int minxh = -Mathf.Min(0, x - effect.areaCenterY);
            int minyh = -Mathf.Min(0, y - (effect.areaWidth - effect.areaCenterX - 1));

            for (int i = minx; i <= maxx; ++i)
            {
                for (int j = miny; j <= maxy; ++j)
                {
                    if (effect.areaMask[i - minx + minxh][effect.areaWidth - (j - miny + minyh) - 1] == true)
                    {
                        ret.Add(map.tiles[j][i]);
                    }
                }
            }
        }

        return ret;
    }

    public static List<BattleUnit> GetUnitsInLocalAreaForActionEffect(int x, int y, Direction direction, ActionAreaType areaType, UnitActionEffect effect, BattleMap map)
    {
        List<BattleUnit> ret = new List<BattleUnit>();

        List<BattleMapTile> tiles = GetTilesInLocalAreaForActionEffect(x, y, direction, areaType, effect, map);

        foreach (var tile in tiles)
        {
            if (tile.unit != null)
            {
                ret.Add(tile.unit);
            }
        }

        return ret;
    }

    // Checks if unit1 is flanking unit2 that is facing a certain direction
    public static bool IsFlanking(int x1, int y1, int x2, int y2, Direction direction)
    {
        int deltaX = x2 - x1;
        int deltaY = y2 - y1;
        int xyDiff = Mathf.Abs(deltaX) - Mathf.Abs(deltaY);

        if (direction == Direction.Down)
        {
            if (xyDiff > 0 || (xyDiff == 0 && y1 < y2))
            {
                return true;
            }
        }
        else if (direction == Direction.Up)
        {
            if (xyDiff > 0 || (xyDiff == 0 && y2 < y1))
            {
                return true;
            }
        }
        else if (direction == Direction.Right)
        {
            if (xyDiff > 0 || (xyDiff == 0 && x1 < x2))
            {
                return true;
            }
        }
        else if (direction == Direction.Left)
        {
            if (xyDiff > 0 || (xyDiff == 0 && x2 < x1))
            {
                return true;
            }
        }

        return false;
    }

    // Checks if unit1 is attacking unit2 that is facing a certain direction from the back
    public static bool IsRearFlanking(int x1, int y1, int x2, int y2, Direction direction)
    {
        int deltaX = x2 - x1;
        int deltaY = y2 - y1;
        int xyDiff = Mathf.Abs(deltaX) - Mathf.Abs(deltaY);

        if (direction == Direction.Down)
        {
            if (xyDiff < 0 && y1 < y2)
            {
                return true;
            }
        }
        else if (direction == Direction.Up)
        {
            if (xyDiff < 0 && y2 < y1)
            {
                return true;
            }
        }
        else if (direction == Direction.Right)
        {
            if (xyDiff < 0 && x1 < x2)
            {
                return true;
            }
        }
        else if (direction == Direction.Left)
        {
            if (xyDiff < 0 && x2 < x1)
            {
                return true;
            }
        }

        return false;
    }

    
}
