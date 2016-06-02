using LanExchange.Application.Implementation;

namespace GenerateEnglish
{
    class Program
    {
        static void Main(string[] args)
        {
            new GenerateEnglishProcess(new FolderManager()).Execute();
        }
    }
}