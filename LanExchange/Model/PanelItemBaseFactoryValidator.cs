using System;
using LanExchange.SDK;

namespace LanExchange.Model
{
    static class PanelItemBaseFactoryValidator
    {

        public static bool ValidateFactory(PanelItemBaseFactory factory)
        {
            if (factory == null)
                return false;
            bool result = false;
            try
            {
                var panelItem = factory.CreatePanelItem(null, "TEST");
                if (panelItem != null && panelItem.CountColumns > 0)
                {
                    for (int i = 0; i < panelItem.CountColumns; i++)
                    {
                        var value = panelItem[i];
                    }
                    try
                    {
                        var value = panelItem[-1];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        result = true;
                    }
                    result = false;
                    try
                    {
                        var value = panelItem[panelItem.CountColumns];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        result = true;
                    }
                }
            }
            catch
            { }
            return result;
        }
    }
}
