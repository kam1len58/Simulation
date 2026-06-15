using System.Collections.Immutable;

namespace Simulation;

public static class KeyHandler
{
    private readonly static ImmutableHashSet<ConsoleKey> _allowedKeysInPause = [ConsoleKey.Enter, ConsoleKey.Escape];
    private readonly static ImmutableHashSet<ConsoleKey> _allowedKeysInSimulation = [ConsoleKey.P, ConsoleKey.Escape];

    public static bool IsAllowed(ConsoleKey key, bool isSimulationInPause)
    {
        if(isSimulationInPause)
            return _allowedKeysInPause.Contains(key);
        else
            return _allowedKeysInSimulation.Contains(key);
    }
}
