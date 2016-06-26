using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Commands
{
    internal abstract class CommandBase : ICommand
    {
        public virtual bool Enabled
        {
            get { return true; }
        }

        public void Execute()
        {
            if (Enabled)
                InternalExecute();
        }

        protected abstract void InternalExecute();
    }
}