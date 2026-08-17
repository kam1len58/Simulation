using Simulation.Actions;
using Simulation.Menu;

namespace Simulation;

public class Simulation(
    Map map,
    PathFinder pathFinder,
    Renderer renderer,
    int grassCount,
    int herbivoreCount,
    int predatorCount,
    int rockCount,
    int treeCount
)
{
    private bool _isSimulationStopped = false;
    private bool _errorShow = false;
    private readonly Map _map = map;
    private int _stepsCount;
    private readonly Renderer _renderer = renderer;
    private readonly List<GameAction> _initActions = [new InitMapAction(map, grassCount, herbivoreCount, predatorCount, rockCount, treeCount)];
    private readonly List<GameAction> _turnActions = [new AddGrassAction(map), new AddHerbivoreAction(map), new MoveCreaturesAction(map, pathFinder)];

    public void NextTurn()
    {
        foreach (var action in _turnActions)
        {
            action.Execute();
        }

        _stepsCount++;

        _renderer.Render(_map, _stepsCount, _isSimulationStopped);
    }

    public void StartSimulation()
    {
        Console.Clear();

        foreach (var action in _initActions)
        {
            action.Execute();
        }

        _renderer.Render(_map, _stepsCount, _isSimulationStopped);

        while (true)
        {
            if (_isSimulationStopped == true)
            {
                if (ProcessInput())
                {
                    return;
                }
            }
            else
            {
                NextTurn();
                Thread.Sleep(GameSettings.TurnDelay);

                if (ProcessInput())
                {
                    return;
                }
            }
        }
    }

    private ConsoleKeyInfo? TryReadKey()
    {
        if (!Console.KeyAvailable)
        {
            return null;
        }

        ConsoleKeyInfo key = Console.ReadKey(true);

        while (Console.KeyAvailable)
        {
            key = Console.ReadKey(true);
        }

        return key;
    }

    private void ShowInvalidKey()
    {
        if (!_errorShow)
        {
            ConsoleWorker.PrintColorText("Неверная операция! Используйте только предложенные.", ConsoleColor.Red);
            _errorShow = true;
        }
    }


    private bool ProcessInput()
    {
        var key = TryReadKey();

        if (!key.HasValue)
        {
            return false;
        }

        if (KeyHandler.IsAllowed(key.Value.Key, _isSimulationStopped))
        {
            if (key.Value.Key == ConsoleKey.Enter)
            {
                _isSimulationStopped = false;

                _renderer.Render(_map, _stepsCount, _isSimulationStopped);
            }
            else if (key.Value.Key == ConsoleKey.P)
            {
                _isSimulationStopped = true;

                _renderer.Render(_map, _stepsCount, _isSimulationStopped);
            }
            else if (key.Value.Key == ConsoleKey.Escape)
            {
                _errorShow = false;

                return true;
            }
        }
        else
        {
            ShowInvalidKey();
        }

        return false;
    }
}
