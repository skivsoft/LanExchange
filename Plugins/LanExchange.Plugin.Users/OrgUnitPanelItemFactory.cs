﻿using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    class OrgUnitPanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new OrgUnitPanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return new OrgUnitPanelItem(null, "TEST");
        }
    }
}