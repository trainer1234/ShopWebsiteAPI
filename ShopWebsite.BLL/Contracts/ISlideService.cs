using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.SlideModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface ISlideService
    {
        Task<Result<bool>> AddSlide(Slide newSlide, string userId);
        Task<Result<bool>> Edit(Slide newSlide, string userId);
        Task<Result<bool>> Remove(string id, string userId);
        Task<Result<List<Slide>>> GetList();
        Task<Result<Slide>> Get(string slideId);
    }
}
