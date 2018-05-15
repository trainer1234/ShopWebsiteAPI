using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.TrainingModels
{
    public static class MatrixFactorizationConstant
    {
        public static double LearningRate { get; set; }
        public static double Regularization { get; set; }
        public static int Iteration { get; set; }
        public static double Threshold { get; set; }
        public static int LatentFactorNumber { get; set; } = 5;
    }
}
