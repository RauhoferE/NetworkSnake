



namespace NetworkLibrary
{
    using System.Net;

    public static class IPHelper
    {
        public static bool IsIPAdress(string s)
        {
            IPAddress adress;

            if (IPAddress.TryParse(s, out adress))
            {
                return true;
            }

            return false;
        }

        public static IPEndPoint GetIPAdress(string s)
        {
            return new IPEndPoint(IPAddress.Parse(s), 80); 
        }
    }
}