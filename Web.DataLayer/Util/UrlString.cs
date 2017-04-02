namespace Web.DataLayer.Util
{
    public class UrlString
    {
        private static string _url;

        public static string BaseAddress()
        {
            if (string.IsNullOrEmpty(_url))
                _url = "https://www.itexmo.com";

            return _url;
        }
    }
}
