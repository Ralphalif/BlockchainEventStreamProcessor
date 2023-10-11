namespace BlockchainEventStreamProcessor.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}

