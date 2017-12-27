using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IImageRepository
    {
        Task<bool> Add(ImageModel imageModel);
        Task<bool> Remove(string id);
        Task<ImageModel> GetBy(string id);
        Task<List<ImageModel>> GetAll();
    }
}
