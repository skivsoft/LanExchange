using LanExchange.Misc.Impl;

namespace GenerateEnglish
{
    class Program
    {
        static void Main(string[] args)
        {
            new GenerateEnglishProcess(new FolderManagerImpl()).Execute();
        }
    }
}