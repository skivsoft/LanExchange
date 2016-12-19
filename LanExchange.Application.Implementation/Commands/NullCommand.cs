using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation.Commands
{
    internal sealed class NullCommand : ICommand
    {
        private static readonly ICommand NullInstance = new NullCommand();

        private NullCommand()
        {
        }

        public static ICommand Instance
        {
            get { return NullInstance; }
        }

        public bool Enabled
        {
            get { return false; }
        }

        public void Execute()
        {
        }
    }
}