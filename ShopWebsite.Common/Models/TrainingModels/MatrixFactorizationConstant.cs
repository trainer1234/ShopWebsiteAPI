using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.TrainingModels
{
    public static class MatrixFactorizationConstant
    {
        public static double LearningRate { get; set; } = 0.01;
        public static double Regularization { get; set; } = 0.01;
        public static int Iteration { get; set; } = 30;
        public static double Threshold { get; set; } = 1;
        public static int LatentFactorNumber { get; set; } = 5;
    }
}
