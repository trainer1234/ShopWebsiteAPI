using ShopWebsite.Common.Models.TrainingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Utils
{
    public class MatrixFactorization
    {
        public (List<List<double>> W, List<List<double>> H) MatrixFactorization_SGD(int latentFactorNumber, 
            int learningRate, int regularization, double threshold, List<List<double>> trainingMatrix)
        {
            List<List<double>> W, H;
            W = new List<List<double>>();
            H = new List<List<double>>();
            Random rnd = new Random();
            int numberOfUser = trainingMatrix.Count;
            int numberOfItem = trainingMatrix[0].Count;

            // Initialize 2 matrix W, H
            for (int i = 0; i < numberOfUser; i++)
            {
                List<double> WK = new List<double>();
                for (int j = 0; j < latentFactorNumber; j++)
                {
                    WK.Add(rnd.Next(0, 5));
                }
                W.Add(WK);
            }
            for (int i = 0; i < numberOfItem; i++)
            {
                List<double> HK = new List<double>();
                for (int j = 0; j < latentFactorNumber; j++)
                {
                    HK.Add(rnd.Next(0, 5));
                }
                H.Add(HK);
            }
            while (true)
            {
                double rmse = 0; // root mean squared error
                for (int i = 0; i < numberOfUser; i++)
                {
                    for (int j = 0; j < numberOfItem; j++)
                    {
                        double currentRating = trainingMatrix[i][j];
                        double predictedRating = 0;
                        for (int k = 0; k < latentFactorNumber; k++)
                        {
                            predictedRating = predictedRating + W[i][k] * H[j][k];
                        }
                        double error = currentRating - predictedRating;
                        rmse = rmse + error * error;
                        for (int k = 0; k < latentFactorNumber; k++)
                        {
                            W[i][k] = W[i][k] + learningRate * (error * H[j][k] - regularization * W[i][k]);
                            H[j][k] = H[j][k] + learningRate * (error * W[i][k] - regularization * H[j][k]);
                        }
                    }
                }
                if (rmse <= threshold)
                {
                    break;
                }
            }

            return (W, H);
        }
    }
}
