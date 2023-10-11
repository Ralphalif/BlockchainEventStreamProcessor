namespace BlockChainEventStreamProcessor.Interfaces
{
    public interface ICommandRouter
    {
        ICommandHandler<string> GetCommandHandler(string command);
    }
}

