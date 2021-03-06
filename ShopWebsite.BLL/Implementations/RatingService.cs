﻿using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Implementations
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Result<List<Product>> GetTopNRecommendedProduct(User user, int n)
        {
            var result = new Result<List<Product>>();

            var recommendedProducts = _ratingRepository.GetNRecommendedProduct(user, n);

            result.Content = recommendedProducts;
            result.Succeed = true;

            return result;
        }

        public Result<CustomerRating> GetPastRating(string userId, string productId)
        {
            var result = new Result<CustomerRating>();

            var pastRating = _ratingRepository.GetPastRating(userId, productId);

            result.Content = pastRating;
            result.Succeed = true;

            return result;
        }

        public Result<bool> Update(CustomerRating newCustomerRating)
        {
            var result = new Result<bool>();

            _ratingRepository.Update(newCustomerRating);

            result.Succeed = true;
            result.Content = true;

            return result;
        }
    }
}
