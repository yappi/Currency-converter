using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlTextReader Reader { get; set; }
        public IEnumerable<Rate> Rates { get; set; }
        public MainWindow()
        {                      
            InitializeComponent();
            string UrlString = ConfigurationManager.ConnectionStrings["URL"].ConnectionString;
            CheckInternetConection();
            GetXml(UrlString);
        }

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

        private void GetXml(string UrlString)
        {
            Reader = new XmlTextReader(UrlString);
            XDocument xdoc = XDocument.Load(Reader);
            if(xdoc != null)
            {
                Rates = from xe in xdoc.Element("exchange").Elements("currency")
                        where xe.Element("cc").Value == "USD" || xe.Element("cc").Value == "EUR"
                        select new Rate
                        {
                            name = xe.Element("cc").Value,
                            rate = xe.Element("rate").Value
                        };
            }
            else
            {
                ErrorURL errorURL= new ErrorURL();
                errorURL.Show();
            }
            
        }

        private void InputValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            double usd = double.Parse(Rates.First(n => n.name == "USD").rate);
            double eur = double.Parse(Rates.First(n => n.name == "EUR").rate);
            double uah;
            double.TryParse(InputValue.Text, out uah);
            UsdOutputValue.Text = ((usd * uah) / 1000).ToString();
            EurOutputValue.Text = ((eur * uah) / 1000).ToString();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public class Rate
        {
            public string name;
            public string rate;
        }
    }
}
