using Simulation.Entities;

namespace Simulation;

public class PathFinder
{
    public List<Coordinates> FindPath(Coordinates start, Coordinates goal, Map map)
    {
        var path = new List<Coordinates>();
        var openSet = new PriorityQueue<Coordinates, int>();
        var closedSet = new HashSet<Coordinates>();
        var cameFrom = new Dictionary<Coordinates, Coordinates>();
        var scores = new Dictionary<Coordinates, int>();
        int stepsFromStart = 0;
        int estimatedStepsToGoal = Math.Abs(goal.X - start.X) + Math.Abs(goal.Y - start.Y);
        int totalSteps = stepsFromStart + estimatedStepsToGoal;
        scores.Add(start, stepsFromStart);
        openSet.Enqueue(start, totalSteps);
        while(openSet.Count > 0)
        {
            var current = openSet.Dequeue();
            if(closedSet.Contains(current))
            {
                continue;
            }
            closedSet.Add(current);

            if (current == goal)
            {
                var temp = current;

                while(temp!=start)
                {
                    path.Add(temp);
                    temp = cameFrom[temp];
                }

                path.Reverse();
                return path;
            }

            foreach(var neighbor in map.GetNeighbors(current))
            {
                if (map.IsOccupied(neighbor) && neighbor !=goal)
                    continue;

                if (closedSet.Contains(neighbor))
                    continue;

                int stepsFromStartNeighbor = scores[current] + 1;
                if(!scores.ContainsKey(neighbor) || stepsFromStartNeighbor < scores[neighbor])
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
