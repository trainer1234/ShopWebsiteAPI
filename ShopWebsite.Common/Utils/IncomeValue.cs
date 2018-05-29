using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Utils
{
    public static class IncomeValue
    {
        public static double GetIncomeLimitValue(IncomeLimit incomeLimit)
        {
            if (incomeLimit == IncomeLimit.LessThan10M)
            {
                return 10000000;
            }
            else if (incomeLimit == IncomeLimit.LessThan15M)
            {
                return 15000000;
            }
            else if (incomeLimit == IncomeLimit.LessThan5M)
            {
                return 5000000;
            }
            else
            {
                return Int64.MaxValue;
            }
        }
    }
}
