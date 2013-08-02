namespace LanExchange.Service
{
    public static class PuntoSwitcherServiceFactory
    {
        private static IPuntoSwitcherService s_ServiceEngRus;

        public static IPuntoSwitcherService GetPuntoSwitcherService()
        {
            if (s_ServiceEngRus == null)    
                s_ServiceEngRus = new PuntoSwitcherServiceEngRus();
            return s_ServiceEngRus;
        }
    }
}
