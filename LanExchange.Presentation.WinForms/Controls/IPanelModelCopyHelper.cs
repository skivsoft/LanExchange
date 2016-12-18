using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Controls
{
    public interface IPanelModelCopyHelper
    {
        int IndexesCount { get; }

        string GetSelectedText();

        PanelItemBaseHolder GetItems();

        string GetColumnText(int colIndex);
    }
}