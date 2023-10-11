using BlockChainEventStreamProcessor.Handlers.Commands;
using BlockchainEventStreamProcessor.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlockChainEventStreamProcessor.Routers
{
    public class CommandRouter : ICommandRouter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandRouter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommandHandler<string> GetCommandHandler(string command)
        {
            switch (command.ToLower())
            {
                case "--read-inline":
                    return serviceProvider.GetRequiredService<ReadInlineCommandHandler>();
                case "--read-file":
                    return serviceProvider.GetRequiredService<ReadFileCommandHandler>();
                case "--reset":
                    return serviceProvider.GetRequiredService<ResetCommandHandler>();
                default:
                    return null;
            }
        }
    }
}

