using System;
using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Plugin.Shortcut
{
    public sealed class ShortcutFiller : IPanelFiller
    {
        private const string PANEL_ITEM_SUFFIX = "PanelItem";

        private readonly ITranslationService translationService;
        private readonly IAddonManager addonManager;
        private readonly IMenuProducer menuProducer;

        public ShortcutFiller(
            ITranslationService translationService,
            IAddonManager addonManager,
            IMenuProducer menuProducer)
        {
            if (translationService != null) throw new ArgumentNullException(nameof(translationService));
            if (addonManager != null) throw new ArgumentNullException(nameof(addonManager));
            if (menuProducer != null) throw new ArgumentNullException(nameof(menuProducer));

            this.translationService = translationService;
            this.addonManager = addonManager;
            this.menuProducer = menuProducer;
        }

        public bool IsParentAccepted(PanelItemBase parent)
        {
            return parent is ShortcutRoot;
        }

        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var visitor = new FillerMenuVisitor(parent, result);
            menuProducer.MainMenu.Accept(visitor);

            //result.Add(new ShortcutPanelItem(parent, Resources.KeyF1, Resources.KeyF1__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyF10, Resources.KeyF10__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlW, Resources.KeyCtrlW__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlR, Resources.KeyCtrlR__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlWinX, Resources.KeyCtrlWinX__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlA, Resources.KeyCtrlA__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlC, Resources.KeyCtrlC__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlV, Resources.KeyCtrlV__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlIns, Resources.KeyCtrlIns__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlAltIns, Resources.KeyCtrlAltIns__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyDel, Resources.KeyDel__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyEsc, Resources.KeyEsc__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyEscLong, Resources.KeyEscLong__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyAnyChar, Resources.KeyAnyChar__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyBackspace, Resources.KeyBackspace__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlDown, Resources.KeyCtrlDown__));
            //result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlUp, Resources.KeyCtrlUp__));

            foreach (var pair in addonManager.PanelItems)
                foreach (var menuItem in pair.Value.ContextMenu)
                    if (!string.IsNullOrEmpty(menuItem.ShortcutKeys))
                    {
                        var translatedText = translationService.Translate(menuItem.Text);
                        var context = SuppressPostfix(pair.Key, PANEL_ITEM_SUFFIX);
                        string customImageName = string.Empty;
                        if (menuItem.ProgramValue != null)
                            customImageName = string.Format(PanelImageNames.ADDON_FMT, menuItem.ProgramValue.Id);
                        result.Add(new ShortcutPanelItem(parent, menuItem.ShortcutKeys, translatedText, context, customImageName));
                    }
        }

        private static string SuppressPostfix(string value, string postfix)
        {
            if (value.EndsWith(postfix, StringComparison.Ordinal))
                return value.Remove(value.Length - postfix.Length);
            return value;
        }
    }
}