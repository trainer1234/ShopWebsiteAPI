using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IImageService
    {
        Task<Result<string>> AddImage(ImageModel imageModel, byte[] imageData);
        Task<Result<ImageModel>> GetImage(string id);
        Task<Result<bool>> RemoveImage(string id);
    }
}
