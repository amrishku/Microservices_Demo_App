namespace Chingari.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
