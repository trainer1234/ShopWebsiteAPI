using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IManufactureRepository
    {
        Task<bool> Add(Manufacture newManufacture);
        Task<bool> Edit(Manufacture newManufacture);
        Task<bool> Remove(string manufactureId);
        Task<Manufacture> GetBy(string manufactureId);
        Task<List<Manufacture>> GetAll();
    }
}
