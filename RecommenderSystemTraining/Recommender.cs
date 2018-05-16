using ShopWebsite.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecommenderSystemTraining
{
    public class Recommender
    {
        private IRecommenderService _recommenderService;

        public Recommender(IRecommenderService recommenderService)
        {
            _recommenderService = recommenderService;
        }

        public void MatrixFactor()
        {
            _recommenderService.MatrixFactor();
        }

        public void Training()
        {
            _recommenderService.TrainRecommenderSystem();
        }
    }
}
