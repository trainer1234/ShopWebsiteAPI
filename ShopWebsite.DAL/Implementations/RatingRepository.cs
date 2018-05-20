using Microsoft.EntityFrameworkCore;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class RatingRepository : IRatingRepository
    {
        private ShopWebsiteSqlContext _context;
        private IErrorLogRepository _errorLogRepository;
        private IProductRepository _productRepository;

        public RatingRepository(ShopWebsiteSqlContext context, IErrorLogRepository errorLogRepository,
                                    IProductRepository productRepository)
        {
            _context = context;
            _errorLogRepository = errorLogRepository;
            _productRepository = productRepository;
        }

        public CustomerRating GetPastRating(string userId, string productId)
        {
            try
            {
                var pastRating = _context.CustomerRatings.SingleOrDefault(rating =>
                                    rating.UserId == userId && rating.ProductId == productId);
                if (pastRating != null)
                {
                    return pastRating;
                }
                else
                {
                    return new CustomerRating
                    {
                        ProductId = productId,
                        UserId = userId,
                        Rating = -1
                    };
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
        }

        public void Update(CustomerRating customerRating)
        {
            try
            {
                var pastRating = _context.CustomerRatings.SingleOrDefault(rating =>
                                    rating.UserId == customerRating.UserId && rating.ProductId == customerRating.ProductId);
                if (pastRating != null)
                {
                    pastRating.Rating = customerRating.Rating;

                    _context.Update(pastRating);
                }
                else
                {
                    _context.Add(new CustomerRating
                    {
                        ProductId = customerRating.ProductId,
                        UserId = customerRating.UserId,
                        Rating = customerRating.Rating
                    });
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
        }

        public List<Product> GetNRecommendedProduct(User user, int n)
        {
            var userItemPredicts = _context.UserItemPredicts
                                        .Include(p => p.Product)
                                            .ThenInclude(prod => prod.ProductImages)
                                        .Include(p => p.Product)
                                            .ThenInclude(product => product.Manufacture)
                                                .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                        .Include(p => p.Product)
                                            .ThenInclude(product => product.ProductProperties)
                                                .ThenInclude(property => property.Property)
                                    .Where(p => p.UserId == user.Id).ToList();

            for (int i = 0; i < userItemPredicts.Count; i++)
            {
                foreach (var hobby in user.UserHobbies)
                {
                    if (hobby.ManufactureId == userItemPredicts[i].Product.ManufactureId)
                    {
                        userItemPredicts[i].PredictedRating += 0.5;
                        break;
                    }
                }

                var income = IncomeValue.GetIncomeLimitValue(user.Income);
                var productPrice = userItemPredicts[i].Product.Price;
                if (income >= productPrice)
                {
                    userItemPredicts[i].PredictedRating += 0.5;
                }
            }

            return userItemPredicts.OrderByDescending(p => p.PredictedRating).Take(n).Select(p => p.Product).ToList();
        }
    }
}
