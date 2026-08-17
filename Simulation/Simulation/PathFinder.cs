using Simulation.Entities;

namespace Simulation;

public class PathFinder
{
    public List<Point> FindPath(Map map, Point start, Point goal)
    {
        var path = new List<Point>();
        var openSet = new PriorityQueue<Point, int>();
        var closedSet = new HashSet<Point>();
        var cameFrom = new Dictionary<Point, Point>();
        var scores = new Dictionary<Point, int>();

        int stepsFromStart = 0;
        int estimatedStepsToGoal = Math.Abs(goal.X - start.X) + Math.Abs(goal.Y - start.Y);
        int totalSteps = stepsFromStart + estimatedStepsToGoal;

        scores.Add(start, stepsFromStart);
        openSet.Enqueue(start, totalSteps);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (closedSet.Contains(current))
            {
                continue;
            }

            closedSet.Add(current);

            if (current == goal)
            {
                var temp = current;

                while (temp != start)
                {
                    path.Add(temp);
                    temp = cameFrom[temp];
                }

                path.Reverse();
                return path;
            }

            foreach (var neighbor in map.GetNeighbors(current))
            {
                if (map.IsOccupied(neighbor) && neighbor != goal)
                {
                    continue;
                }

                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                int stepsFromStartNeighbor = scores[current] + 1;

                if (!scores.ContainsKey(neighbor) || stepsFromStartNeighbor < scores[neighbor])
                {
                    int estimatedStepsToGoalNeighbor = Math.Abs(goal.X - neighbor.X) + Math.Abs(goal.Y - neighbor.Y);
                    int totalStepsToNeighbor = stepsFromStartNeighbor + estimatedStepsToGoalNeighbor;

                    scores[neighbor] = stepsFromStartNeighbor;
                    cameFrom[neighbor] = current;

                    openSet.Enqueue(neighbor, totalStepsToNeighbor);
                }
            }
        }

        return path;
    }
}
