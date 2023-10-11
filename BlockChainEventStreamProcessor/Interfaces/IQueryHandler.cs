namespace BlockChainEventStreamProcessor.Interfaces
{
    public interface IQueryHandler<TQuery>
    {
        void Handle(TQuery query);
    }
}

