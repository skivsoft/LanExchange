namespace LanExchange.SDK
{
    /// <summary>
    /// LanExchange panel image names.
    /// </summary>
    public static class PanelImageNames
    {
        public const string NORMAL_POSTFIX   = ".normal";
        public const string DISABLED_POSTFIX = ".disabled";
        public const string HIDDEN_POSTFIX   = ".hidden";
        /// <summary>
        /// The workgroup
        /// </summary>
        public const string Workgroup        = "DomainPanelItem" + NORMAL_POSTFIX;
        /// <summary>
        /// The computer normal
        /// </summary>
        public const string ComputerNormal   = "ComputerPanelItem" + NORMAL_POSTFIX;
        /// <summary>
        /// The computer disabled
        /// </summary>
        public const string ComputerDisabled = "ComputerPanelItem" + DISABLED_POSTFIX;
        /// <summary>
        /// The share normal
        /// </summary>
        public const string ShareNormal      = "SharePanelItem" + NORMAL_POSTFIX;
        /// <summary>
        /// The share hidden
        /// </summary>
        public const string ShareHidden      = "SharePanelItem" + HIDDEN_POSTFIX;
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
        public const string UserNormal       = "UserPanelItem" + NORMAL_POSTFIX;
        /// <summary>
        /// The user disabled
        /// </summary>
        public const string UserDisabled     = "UserPanelItem" + DISABLED_POSTFIX;
    }
}
