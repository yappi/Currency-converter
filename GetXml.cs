using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace CurrencyConverter
{
    class GetXml
    {
        public XmlTextReader Reader;
        //public IEnumerable<Rate> Rates;

        public void TryGetXml(ref IEnumerable<Rate> Rates)
        {
            Thread.Sleep(10000);
            string UrlString = ConfigurationManager.ConnectionStrings["URL"].ConnectionString;
            Reader = new XmlTextReader(UrlString);
            XDocument xdoc = XDocument.Load(Reader);
            if (xdoc != null)
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
                ErrorURL errorURL = new ErrorURL();
                errorURL.Show();
            }
        }

        public void UpdateUI()
        {

        }
    }
}
