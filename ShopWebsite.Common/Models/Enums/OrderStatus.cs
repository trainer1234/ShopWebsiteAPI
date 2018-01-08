using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.Enums
{
    public enum OrderStatus : int
    {
        NotConfirmed = 0,
        Pending = 1,
        Delivering = 2,
        Delivered = 3,
        Cancel = 4
    }
}
