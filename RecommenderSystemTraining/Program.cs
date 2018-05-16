using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.BLL.Implementations;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Implementations;
using System;

namespace RecommenderSystemTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IRecommenderService, RecommenderService>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddEntityFrameworkSqlServer().AddDbContext<ShopWebsiteSqlContext>(options =>
                options.UseSqlServer("Data Source=163.22.17.212;Initial Catalog=ShopWebsite;Integrated Security=False;User ID=sa;Password=abcd@1234;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            var provider = services.BuildServiceProvider();
            var recommenderService = provider.GetService<IRecommenderService>();

            Console.WriteLine("Start training!");
            Console.WriteLine("Matrix Factorization");
            Console.WriteLine("--------------------");

            recommenderService.MatrixFactor();

            Console.WriteLine("Matrix Factorization Completed");
            Console.WriteLine("Predicting Rating");
            Console.WriteLine("-----------------");

            recommenderService.TrainRecommenderSystem();

            Console.WriteLine("Training Completed");
            Console.ReadLine();
        }
    }
}
