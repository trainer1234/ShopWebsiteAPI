using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.TrainingModels;
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

namespace ShopWebsite.BLL.Implementations
{
    public class RecommenderService : IRecommenderService
    {
        private ShopWebsiteSqlContext _context;
        private IErrorLogRepository _errorLogRepository;

        public RecommenderService(ShopWebsiteSqlContext context, IErrorLogRepository errorLogRepository)
        {
            _context = context;
            _errorLogRepository = errorLogRepository;
        }

        public void MatrixFactor()
        {
            try
            {
                var users = _context.Users.OrderBy(user => user.Index).ToList();
                var items = _context.Products.OrderBy(item => item.Index).ToList();
                var customerRatings = _context.CustomerRatings.ToList();

                List<List<double>> userItemRatingMatrix = new List<List<double>>();
                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];
                    if (user.Role == UserRole.Customer)
                    {
                        List<double> ratingsOfEachUser = new List<double>();
                        for (int j = 0; j < items.Count; j++)
                        {
                            var item = items[j];
                            var ratingOfUser = customerRatings.SingleOrDefault(customerRating =>
                                customerRating.UserId == user.Id && customerRating.ProductId == item.Id);
                            if (ratingOfUser != null)
                            {
                                ratingsOfEachUser.Add(ratingOfUser.Rating);
                            }
                            else
                            {
                                ratingsOfEachUser.Add(-1);
                            }
                        }
                        userItemRatingMatrix.Add(ratingsOfEachUser);
                    }
                }

                MatrixFactorization matrixFactorization = new MatrixFactorization();
                var twoMatrices = matrixFactorization.MatrixFactorization_SGD(MatrixFactorizationConstant.LatentFactorNumber,
                                    MatrixFactorizationConstant.LearningRate, MatrixFactorizationConstant.Regularization,
                                    MatrixFactorizationConstant.Iteration, MatrixFactorizationConstant.Threshold,
                                    userItemRatingMatrix);
                var W = twoMatrices.W;
                var H = twoMatrices.H;

                _context.UserLatentFactorMatrices.RemoveRange(_context.UserLatentFactorMatrices.ToList());
                _context.ItemLatentFactorMatrices.RemoveRange(_context.ItemLatentFactorMatrices.ToList());
                _context.SaveChanges();

                for (int i = 0; i < W.Count; i++)
                {
                    for (int j = 0; j < W[0].Count; j++)
                    {
                        _context.UserLatentFactorMatrices.Add(new UserLatentFactorMatrix
                        {
                            Row = i,
                            Column = j,
                            CellValue = W[i][j]
                        });
                    }
                }

                for (int i = 0; i < H.Count; i++)
                {
                    for (int j = 0; j < H[0].Count; j++)
                    {
                        _context.ItemLatentFactorMatrices.Add(new ItemLatentFactorMatrix
                        {
                            Row = i,
                            Column = j,
                            CellValue = H[i][j]
                        });
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
        }

        public void TrainRecommenderSystem()
        {
            try
            {
                var users = _context.Users.OrderBy(user => user.Index).ToList();
                var items = _context.Products.OrderBy(item => item.Index).ToList();
                var customerRatings = _context.CustomerRatings.ToList();
                var userLatentFactorMatrixData = _context.UserLatentFactorMatrices.ToList();
                var itemLatentFactorMatrixData = _context.ItemLatentFactorMatrices.ToList();

                List<List<double>> userLatentMatrix = new List<List<double>>();
                List<List<double>> itemLatentMatrix = new List<List<double>>();

                for (int i = 0; i < users.Count; i++)
                {
                    List<double> WK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        WK.Add(0);
                    }
                    userLatentMatrix.Add(WK);
                }
                for (int i = 0; i < items.Count; i++)
                {
                    List<double> HK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        HK.Add(0);
                    }
                    itemLatentMatrix.Add(HK);
                }

                for (int i = 0; i < userLatentFactorMatrixData.Count; i++)
                {
                    userLatentMatrix[userLatentFactorMatrixData[i].Row][userLatentFactorMatrixData[i].Column] = userLatentFactorMatrixData[i].CellValue;
                }
                for (int i = 0; i < itemLatentFactorMatrixData.Count; i++)
                {
                    itemLatentMatrix[itemLatentFactorMatrixData[i].Row][itemLatentFactorMatrixData[i].Column] = itemLatentFactorMatrixData[i].CellValue;
                }

                _context.RemoveRange(_context.UserItemPredicts.ToList());
                _context.SaveChanges();

                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];
                    if (user.Role == UserRole.Customer)
                    {
                        for (int j = 0; j < items.Count; j++)
                        {
                            var item = items[j];
                            var ratingOfUser = customerRatings.SingleOrDefault(customerRating =>
                                customerRating.UserId == user.Id && customerRating.ProductId == item.Id);
                            if (ratingOfUser == null)
                            {
                                var userItemPredict = new UserItemPredict
                                {
                                    ProductId = item.Id,
                                    UserId = user.Id,
                                    PredictedRating = 0
                                };
                                for (int k = 0; k < MatrixFactorizationConstant.LatentFactorNumber; k++)
                                {
                                    userItemPredict.PredictedRating += userLatentMatrix[i][k] * itemLatentMatrix[j][k];
                                }

                                _context.Add(userItemPredict);
                            }
                        }
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
        }

        public void UpdateUserLatentFactorMatrixWhenAddingNewUser(string userId)
        {
            try
            {
                var users = _context.Users.OrderBy(user => user.Index).ToList();
                var searchUserIndex = users.FindIndex(user => user.Id == userId);
                
                for (int i = 0; i < MatrixFactorizationConstant.LatentFactorNumber; i++)
                {
                    _context.UserLatentFactorMatrices.Add(new UserLatentFactorMatrix
                    {
                        Row = searchUserIndex,
                        Column = i,
                        CellValue = 1
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

        public void UpdateItemLatentFactorMatrixWhenAddingNewItem(string itemId)
        {
            try
            {
                var items = _context.Products.OrderBy(item => item.Index).ToList();
                var searchItemIndex = items.FindIndex(item => item.Id == itemId);
                for (int i = 0; i < MatrixFactorizationConstant.LatentFactorNumber; i++)
                {
                    _context.ItemLatentFactorMatrices.Add(new ItemLatentFactorMatrix
                    {
                        Row = searchItemIndex,
                        Column = i,
                        CellValue = 1
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

        public void RecommendNewUser(string userId)
        {
            try
            {
                var users = _context.Users.OrderBy(user => user.Index).ToList();
                var items = _context.Products.OrderBy(item => item.Index).ToList();
                var customerRatings = _context.CustomerRatings.ToList();
                var userLatentFactorMatrixData = _context.UserLatentFactorMatrices.ToList();
                var itemLatentFactorMatrixData = _context.ItemLatentFactorMatrices.ToList();

                List<List<double>> userLatentMatrix = new List<List<double>>();
                List<List<double>> itemLatentMatrix = new List<List<double>>();

                for (int i = 0; i < users.Count; i++)
                {
                    List<double> WK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        WK.Add(0);
                    }
                    userLatentMatrix.Add(WK);
                }
                for (int i = 0; i < items.Count; i++)
                {
                    List<double> HK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        HK.Add(0);
                    }
                    itemLatentMatrix.Add(HK);
                }

                for (int i = 0; i < userLatentFactorMatrixData.Count; i++)
                {
                    userLatentMatrix[userLatentFactorMatrixData[i].Row][userLatentFactorMatrixData[i].Column] = userLatentFactorMatrixData[i].CellValue;
                }
                for (int i = 0; i < itemLatentFactorMatrixData.Count; i++)
                {
                    itemLatentMatrix[itemLatentFactorMatrixData[i].Row][itemLatentFactorMatrixData[i].Column] = itemLatentFactorMatrixData[i].CellValue;
                }

                var searchUserIndex = users.FindIndex(user => user.Id == userId);

                if (users[searchUserIndex].Role == UserRole.Customer)
                {
                    for (int j = 0; j < items.Count; j++)
                    {
                        var item = items[j];
                        var ratingOfUser = customerRatings.SingleOrDefault(customerRating =>
                            customerRating.UserId == userId && customerRating.ProductId == item.Id);
                        if (ratingOfUser == null)
                        {
                            var userItemPredict = new UserItemPredict
                            {
                                ProductId = item.Id,
                                UserId = userId,
                                PredictedRating = 0
                            };
                            for (int k = 0; k < MatrixFactorizationConstant.LatentFactorNumber; k++)
                            {
                                userItemPredict.PredictedRating += userLatentMatrix[searchUserIndex][k] * itemLatentMatrix[j][k];
                            }

                            _context.Add(userItemPredict);
                        }
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
        }

        public void RecomendNewItem(string itemId)
        {
            try
            {
                var users = _context.Users.OrderBy(user => user.Index).ToList();
                var items = _context.Products.OrderBy(item => item.Index).ToList();
                var customerRatings = _context.CustomerRatings.ToList();
                var userLatentFactorMatrixData = _context.UserLatentFactorMatrices.ToList();
                var itemLatentFactorMatrixData = _context.ItemLatentFactorMatrices.ToList();

                List<List<double>> userLatentMatrix = new List<List<double>>();
                List<List<double>> itemLatentMatrix = new List<List<double>>();

                for (int i = 0; i < users.Count; i++)
                {
                    List<double> WK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        WK.Add(0);
                    }
                    userLatentMatrix.Add(WK);
                }
                for (int i = 0; i < items.Count; i++)
                {
                    List<double> HK = new List<double>();
                    for (int j = 0; j < MatrixFactorizationConstant.LatentFactorNumber; j++)
                    {
                        HK.Add(0);
                    }
                    itemLatentMatrix.Add(HK);
                }

                for (int i = 0; i < userLatentFactorMatrixData.Count; i++)
                {
                    userLatentMatrix[userLatentFactorMatrixData[i].Row][userLatentFactorMatrixData[i].Column] = userLatentFactorMatrixData[i].CellValue;
                }
                for (int i = 0; i < itemLatentFactorMatrixData.Count; i++)
                {
                    itemLatentMatrix[itemLatentFactorMatrixData[i].Row][itemLatentFactorMatrixData[i].Column] = itemLatentFactorMatrixData[i].CellValue;
                }

                var searchItemIndex = items.FindIndex(item => item.Id == itemId);
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Role == UserRole.Customer)
                    {
                        var ratingOfUser = customerRatings.SingleOrDefault(customerRating =>
                            customerRating.UserId == users[i].Id && customerRating.ProductId == itemId);
                        if (ratingOfUser == null)
                        {
                            var userItemPredict = new UserItemPredict
                            {
                                ProductId = itemId,
                                UserId = users[i].Id,
                                PredictedRating = 0
                            };
                            for (int k = 0; k < MatrixFactorizationConstant.LatentFactorNumber; k++)
                            {
                                userItemPredict.PredictedRating += userLatentMatrix[i][k] * itemLatentMatrix[searchItemIndex][k];
                            }

                            _context.Add(userItemPredict);
                        }
                    }
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
