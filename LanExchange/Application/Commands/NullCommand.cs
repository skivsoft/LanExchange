using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal sealed class NullCommand : ICommand
    {
        private static readonly ICommand instance = new NullCommand();

        private NullCommand()
        {
        }

        public static ICommand Instance
        {
            get { return instance; }
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