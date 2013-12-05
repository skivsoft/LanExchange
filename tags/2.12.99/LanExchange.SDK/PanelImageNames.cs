namespace LanExchange.SDK
{
    /// <summary>
    /// LanExchange panel image names.
    /// </summary>
    public static class PanelImageNames
    {
        public const string DOMAIN   = "DomainPanelItem";
        public const string COMPUTER = "ComputerPanelItem";
        public const string SHARE    = "SharePanelItem";
        public const string USER     = "UserPanelItem";
        public const string SHORTCUT = "ShortcutPanelItem";
        public const string NORMAL_POSTFIX      = ".normal";
        public const string DISABLED_POSTFIX    = ".disabled";
        public const string HIDDEN_POSTFIX      = ".hidden";
        public const string UNREACHABLE_POSTFIX = ".unreachable";
        public const string CUSTOM_POSTFIX      = ".custom";
        public const string ADDON_FMT = "program.{0}";
        /// <summary>
        /// The workgroup
        /// </summary>
        public const string Workgroup        = DOMAIN + NORMAL_POSTFIX;
        /// <summary>
        /// The computer normal
        /// </summary>
        public const string ComputerNormal   = COMPUTER + NORMAL_POSTFIX;
        /// <summary>
        /// The computer disabled
        /// </summary>
        public const string ComputerDisabled = COMPUTER + DISABLED_POSTFIX;

        public const string ComputerUnreachable = COMPUTER + UNREACHABLE_POSTFIX;

        public const string ComputerCustom = COMPUTER + CUSTOM_POSTFIX;
        /// <summary>
        /// The share normal
        /// </summary>
        public const string ShareNormal      = SHARE + NORMAL_POSTFIX;
        /// <summary>
        /// The share hidden
        /// </summary>
        public const string ShareHidden      = SHARE + HIDDEN_POSTFIX;
        /// <summary>
        /// The share printer
        /// </summary>
        public const string SharePrinter     = "share_printer";
        /// <summary>
        /// The double dot
        /// </summary>
        public const string DoubleDot        = "double_dot";
        /// <summary>
        /// The user normal
        /// </summary>
        public const string UserNormal       = USER + NORMAL_POSTFIX;
        /// <summary>
        /// The user disabled
        /// </summary>
        public const string UserDisabled     = USER + DISABLED_POSTFIX;

        public const string ShortcutNormal   = SHORTCUT + NORMAL_POSTFIX;
    }
}
