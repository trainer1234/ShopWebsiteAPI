using PayPal.Api;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Implementations
{
    public class PaypalService : IPaypalService
    {
        public Payment CreatePayment(ProductOrder productOrder, string intent)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PaypalOption.ClientId, PaypalOption.ClientSecret).GetAccessToken());

            var payment = new Payment
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(productOrder),
                redirect_urls = new RedirectUrls
                {
                    return_url = PaypalOption.ReturnUrl,
                    cancel_url = PaypalOption.CancelUrl
                }
            };

            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }

        private List<Transaction> GetTransactionsList(ProductOrder productOrder)
        {
            var transactionList = new List<Transaction>();
            var itemList = new List<Item>();
            foreach (var product in productOrder.ProductMapOrderDetails)
            {
                long productRealPrice = product.Product.Price;
                if (product.Product.PromotionRate > 0)
                {
                    productRealPrice = product.Product.Price - (long)(product.Product.Price * product.Product.PromotionRate);
                }
                itemList.Add(new Item
                {
                    name = product.Product.Name,
                    currency = "USD",
                    price = productRealPrice.ToString(),
                    quantity = product.ProductAmount.ToString(),
                    sku = product.Product.Id
                });
            }
            transactionList.Add(new Transaction()
            {
                description = PaypalOption.TransactionDescription,
                invoice_number = productOrder.OrderId,
                amount = new Amount()
                {
                    currency = "USD",
                    total = productOrder.TotalCost.ToString(),
                    details = new Details()
                    {
                        tax = "0",
                        shipping = "0",
                        subtotal = productOrder.TotalCost.ToString()
                    }
                },
                item_list = new ItemList()
                {
                    items = itemList
                },
                payee = new Payee
                {
                    // TODO.. Enter the payee email address here
                    email = PaypalOption.PayeeEmail,

                    // TODO.. Enter the merchant id here
                    merchant_id = PaypalOption.PayeeMerchantId
                }
            });

            return transactionList;
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PaypalOption.ClientId, PaypalOption.ClientSecret).GetAccessToken());

            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
    }
}
