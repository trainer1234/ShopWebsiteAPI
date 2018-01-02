using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface ICustomerRepository
    {
        Task<string> Add(Customer newCustomer);
        Task<bool> Edit(Customer newCustomer);
        Task<bool> Remove(string customerId);
        Task<Customer> Get(string customerId);
        Task<List<Customer>> GetAll();
    }
}
