using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainEventStreamProcessor
{
    class Program
    {
        private readonly ICommandRouter commandRouter;

        public Program(ICommandRouter commandRouter)
        {
            this.commandRouter = commandRouter;
        }

        static void Main(string[] args)
        {
            var serviceProvider = ServiceConfiguration.ConfigureServices();
            var router = serviceProvider.GetRequiredService<ICommandRouter>();
            var program = new Program(router);

            try
            {
                program.Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Invalid command. Usage: program <command> <arguments>");
                return;
            }

            var command = args[0];
            var argument = args.Length > 1 ? args[1] : command;
            var commandHandler = commandRouter.GetCommandHandler(command);
            if (commandHandler != null)
            {
                commandHandler.Handle(argument);
            }
            else
            {
                Console.WriteLine($"Unknown command: {command}");
            }
        }
    }
}
