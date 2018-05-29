using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Contracts
{
    public interface IRecommenderService
    {
        void MatrixFactor();
        void TrainRecommenderSystem();
        void RecommendNewUser(string userId);
        void RecomendNewItem(string itemId);
        void UpdateUserLatentFactorMatrixWhenAddingNewUser(string userId);
        void UpdateItemLatentFactorMatrixWhenAddingNewItem(string itemId);
    }
}
