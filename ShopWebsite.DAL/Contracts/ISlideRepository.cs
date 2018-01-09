using ShopWebsite.DAL.Models.SlideModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface ISlideRepository
    {
        Task<bool> Add(Slide newSlide);
        Task<bool> Edit(Slide newSlide);
        Task<bool> Remove(string id);
        Task<List<Slide>> GetList();
        Task<Slide> Get(string slideId);
    }
}
