using LanExchange.Application.Implementation;

namespace GenerateEnglish
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new GenerateEnglishProcess(new FolderManager()).Execute();
        }
    }
}