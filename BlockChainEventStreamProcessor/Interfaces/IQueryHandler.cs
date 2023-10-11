namespace BlockchainEventStreamProcessor.Interfaces
{
    public interface IQueryHandler<TQuery>
    {
        void Handle(TQuery query);
    }
}

