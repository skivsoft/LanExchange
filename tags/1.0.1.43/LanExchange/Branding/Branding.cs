
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

        public static string GetWebSiteURL()
        {
            return "code.google.com/p/lanexchange/";
        }

        public static string GetEmailAddress()
        {
            return "skivsoft@gmail.com";
        }
    }
}
