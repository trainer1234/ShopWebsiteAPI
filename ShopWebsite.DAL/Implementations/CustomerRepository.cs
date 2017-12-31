using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private ShopWebsiteSqlContext _context;

        public CustomerRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Customer newCustomer)
        {
            var customerExist = await _context.Customers.Where(cust => cust.Email == newCustomer.Email).Take(1).ToListAsync();
            if(customerExist != null && customerExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newCustomer);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Edit(Customer newCustomer)
        {
            var customerExist = await _context.Customers.Where(cust => cust.Email == newCustomer.Email).Take(1).ToListAsync();
            if (customerExist != null && customerExist.Count > 0)
            {
                var searchCustomer = customerExist[0];

                searchCustomer.Address = newCustomer.Address;
                searchCustomer.FullName = newCustomer.FullName;
                searchCustomer.Phone = newCustomer.Phone;
                
                _context.Update(searchCustomer);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Customer> Get(string customerId)
        {
            var customers = await _context.Customers.Where(cust => cust.Id == customerId).ToListAsync();

            if(customers != null && customers.Count > 0)
            {
                var customer = customers[0];

                return customer;
            }
            return null;
        }

        public async Task<List<Customer>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<bool> Remove(string customerId)
        {
            var customerExist = await _context.Customers.Where(cust => cust.Id == customerId).Take(1).ToListAsync();
            if (customerExist != null && customerExist.Count > 0)
            {
                var searchCustomer = customerExist[0];

                _context.Remove(searchCustomer);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
