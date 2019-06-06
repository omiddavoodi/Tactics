using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController {

    public class PathfindingNode
    {
        public int x;
        public int y;
        public float cost;
        public float heuristic;
        public PathfindingNode origin;
    }

    public class PathfindingNodeComparer : IComparer<PathfindingNode>
    {
        public int Compare(PathfindingNode a, PathfindingNode b)
        {
            if (a.heuristic == b.heuristic)
                return 0;
            if (a.heuristic < b.heuristic)
                return -1;

            return 1;
        }
    }

    public readonly BattleMap map;
    public readonly BattleSimulation simulation;

    public List<BattleUnitController> unitControllers;


    // Gets tiles adjacent to another on this map.
    // For now includes only the 4 tiles around it but could theoretically include portal jumps
    public List<PathfindingNode> GetAdjacentTiles(PathfindingNode origin, bool[,] passabilityMask, bool[,] seenBefore)
    {
        List<PathfindingNode> ret = new List<PathfindingNode>();

        if (origin.x - 1 >= 0)
        {
            int newX = origin.x - 1;
            int newY = origin.y;
            if (!seenBefore[newY, newX] && passabilityMask[newY, newX])
            {
                PathfindingNode neighbor = new PathfindingNode
                {
                    x = newX,
                    y = newY,
                    origin = origin,
                    cost = origin.cost + 1, // Pathfinding cost is 1 due to the tiled nature of the space
                    heuristic = 0
                };

                ret.Add(neighbor);
            }
        }
        if (origin.y - 1 >= 0)
        {
            int newX = origin.x;
            int newY = origin.y - 1;
            if (!seenBefore[newY, newX] && passabilityMask[newY, newX])
            {
                PathfindingNode neighbor = new PathfindingNode
                {
                    x = newX,
                    y = newY,
                    origin = origin,
                    cost = origin.cost + 1, // Pathfinding cost is 1 due to the tiled nature of the space
                    heuristic = 0
                };

                ret.Add(neighbor);
            }
        }
        if (origin.x + 1 < map.width)
        {
            int newX = origin.x + 1;
            int newY = origin.y;
            if (!seenBefore[newY, newX] && passabilityMask[newY, newX])
            {
                PathfindingNode neighbor = new PathfindingNode
                {
                    x = newX,
                    y = newY,
                    origin = origin,
                    cost = origin.cost + 1, // Pathfinding cost is 1 due to the tiled nature of the space
                    heuristic = 0
                };

                ret.Add(neighbor);
            }
        }
        if (origin.y + 1 < map.height)
        {
            int newX = origin.x;
            int newY = origin.y + 1;
            if (!seenBefore[newY, newX] && passabilityMask[newY, newX])
            {
                PathfindingNode neighbor = new PathfindingNode
                {
                    x = newX,
                    y = newY,
                    origin = origin,
                    cost = origin.cost + 1, // Pathfinding cost is 1 due to the tiled nature of the space
                    heuristic = 0
                };

                ret.Add(neighbor);
            }
        }

        return ret;
    }
    
    private float CalculateHeuristic(PathfindingNode node, int endX, int endY)
    {
        return node.cost + Mathf.Abs(endX - node.x) + Mathf.Abs(endY - node.y);
    }

    // Main pathfinder. Uses A*. Could get faster but due to the small size of the maps, it is not a high priority.
	public List<BattleMapTile> GetPath(int startX, int startY, int endX, int endY, bool[,] passabilityMask)
    {
        List<BattleMapTile> path = new List<BattleMapTile>();

        // Mask to know which tiles we have already checked
        bool[,] seenBefore = new bool[map.height, map.width];

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                seenBefore[y, x] = false;
            }
        }

        // A* for getting a specific path

        // Define the base node
        PathfindingNode start = new PathfindingNode
        {
            x = startX,
            y = startY,
            origin = null,
            cost = 0,
            heuristic = 0
        };

        start.heuristic = CalculateHeuristic(start, endX, endY);

        // Set the start position as seen
        seenBefore[startY, startX] = true;


        // TODO: This is a grossly inefficient way of handling this. Should use a proper min heap based priority queue instead
        List<PathfindingNode> pool = new List<PathfindingNode>();
        PathfindingNodeComparer comparer = new PathfindingNodeComparer();
        pool.Add(start);

        PathfindingNode current = null;


        bool foundPath = false;
        // Main loop
        while (pool.Count > 0)
        {
            pool.Sort(comparer);

            current = pool[0];
            pool.RemoveAt(0);

            List<PathfindingNode> adj = GetAdjacentTiles(current, passabilityMask, seenBefore);

            foreach (PathfindingNode node in adj)
            {
                // If we found the path, termiate early
                if (node.x == endX && node.y == endY)
                {
                    foundPath = true;
                    current = node;
                    break;
                }
                else
                {
                    node.heuristic = CalculateHeuristic(node, endX, endY);
                    pool.Add(node);
                    seenBefore[node.y, node.x] = true;
                }
            }


            if (foundPath)
                break;
        }

        if (foundPath)
        {
            while (current.origin != null)
            {
                path.Add(map.tiles[current.y][current.x]);
                current = current.origin;
            }
            path.Reverse();
        }
        return path;
    }
}
