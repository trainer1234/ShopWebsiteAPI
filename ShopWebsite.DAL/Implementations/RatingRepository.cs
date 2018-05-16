using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.CustomerModels;
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

        public RatingRepository(ShopWebsiteSqlContext context, IErrorLogRepository errorLogRepository)
        {
            _context = context;
            _errorLogRepository = errorLogRepository;
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
    }
}
