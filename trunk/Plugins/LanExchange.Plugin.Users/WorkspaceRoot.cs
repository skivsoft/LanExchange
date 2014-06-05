using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public class WorkspaceRoot : PanelItemRootBase
    {
        protected override string GetName()
        {
            return Resources.Workspace;
        }

        public override string ImageName
        {
            get { return string.Empty; }
        }

        public override object Clone()
        {
            return new WorkspaceRoot();
        }
    }
}
