namespace BlockchainEventStreamProcessor.Interfaces
{
    public interface ICommandRouter
    {
        ICommandHandler<string> GetCommandHandler(string command);
    }
}

