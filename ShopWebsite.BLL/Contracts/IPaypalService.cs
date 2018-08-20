using PayPal.Api;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Contracts
{
    public interface IPaypalService
    {
        Payment CreatePayment(ProductOrder productOrder, string intent);

        Payment ExecutePayment(string paymentId, string payerId);
    }
}
