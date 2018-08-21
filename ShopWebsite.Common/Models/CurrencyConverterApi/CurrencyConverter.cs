using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.CurrencyConverterApi
{
    public class CurrencyConverter
    {
        public VND_USD VND_USD { get; set; }
    }

    public class VND_USD
    {
        public double val { get; set; }
    }
}
