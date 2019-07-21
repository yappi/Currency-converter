using System.Net.NetworkInformation;

namespace CurrencyConverter
{
    public class CheckIntConection
    {
        public static void CheckInternetConection()
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send("8.8.8.8");
            if (pingReply.Status == IPStatus.TimedOut)
            {
                Error error = new Error();
                error.Show();
            }
        }
    }
}
