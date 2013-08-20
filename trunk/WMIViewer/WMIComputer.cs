namespace WMIViewer
{
    public sealed class WMIComputer : IWmiComputer
    {
        public WMIComputer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
