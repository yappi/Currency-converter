using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CurrencyConverter
{
    class GetCurrency
    {
        public string GetCur(IEnumerable<Rate> rates, string curentCurrency, double uah)
        {
            double cc = double.Parse(rates.First(n => n.name == curentCurrency).rate, CultureInfo.CurrentCulture);
            return ( uah / cc ).ToString();
        }
    }
}
