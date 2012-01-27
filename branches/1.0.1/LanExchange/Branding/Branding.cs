
namespace LanExchange
{
    class Branding
    {
        public static string BuildFor()
        {
            return "Официальная сборка.";
        }

        public static string UpdateBaseURL()
        {
            return "http://skivsoft.net/lanexchange/update/";
        }

        public static string FileListURL()
        {
            return UpdateBaseURL() + "filelist.php";
        }
    }
}
