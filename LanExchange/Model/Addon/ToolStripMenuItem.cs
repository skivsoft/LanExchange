namespace LanExchange.Model.Addon
{
    public class ToolStripMenuItem
    {
        public string Text { get; set; }
        public string ShortcutKeys  { get; set; }
        public ObjectId ProgramRef { get; set; }
        public string ProgramArgs { get; set; }
    }
}
