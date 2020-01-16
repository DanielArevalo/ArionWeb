namespace Xpinn.Util.PaymentACH
{
    public static class AppConstants
    {
        // :::::::::::::::::::::::::::::::::::::
        // PARAMETROS PAGOS ACH
        // :::::::::::::::::::::::::::::::::::::

        /*
        // AMBIENTE PRUEBAS
        public const string PSE_URL = "https://200.1.124.65/PSEHostingWebServices/PSEHostingWS.asmx";
        public const int ID_TICKETOFFICE = 2312;
        public const string PASSWORD = "123";
        public const string SitePrefix = "https://200.1.124.62/PSEHostingUI";
        public const string ENTITY_URL = "http://financialsoftware.com.co/AtencionAlClienteDEMO/Pages/Asociado/EstadoCuenta/DetallePago.aspx";
        public const bool USE_WS_SECURITY = false;
        public const string SERVICECODE = "1001";
        public const string DYNAMIC_FIELDS = "id_cliente;identificacion_cliente;nombre_cliente";
        */

        // AMBIENTE PRODUCCION
        public const string PSE_URL = "https://200.1.124.118/PSEHostingWebServices/PSEHostingWS.asmx";
        public const int ID_TICKETOFFICE = 6587;
        public const string PASSWORD = "123";
        public const string SitePrefix = "https://www.psepagos.co/PSEHostingUI";
        public const string ENTITY_URL = "http://financialsoftware.com.co/AtencionAlCliente/Pages/Asociado/EstadoCuenta/DetallePago.aspx";
        public const bool USE_WS_SECURITY = false;
        public const string SERVICECODE = "1001";
        public const string DYNAMIC_FIELDS = "id_cliente;identificacion_cliente;nombre_cliente";


        // PARAMETERS 
        public const string WindowServicesName = "Consult Payment WIN SERVICES";
        public const string NameApplication = "ATENCION AL CLIENTE";
        public const string UrlLogApplication = "C:/Publica/LogACHPayment/DataLogPayment.txt";
    }
}
