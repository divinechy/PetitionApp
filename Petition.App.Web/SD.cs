namespace Petition.App.Web
{
    public static class SD
    {
        public static string PetitionAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
