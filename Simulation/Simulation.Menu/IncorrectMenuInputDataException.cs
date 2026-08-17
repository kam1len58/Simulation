
namespace Simulation.Menu;

[Serializable]
internal class IncorrectMenuInputDataException : Exception
{
    public IncorrectMenuInputDataException()
    {
    }

    public IncorrectMenuInputDataException(string? message)
        : base(message)
    {
    }

    public IncorrectMenuInputDataException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}