using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.ProductModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IProductSpecificTypeService
    {
        Task<Result<bool>> AddProductSpecificType(ProductSpecificType newProductSpecificType);
        Task<Result<bool>> EditProductSpeicificType(ProductSpecificType newProductSpecificType);
        Task<Result<bool>> RemoveProductSpecificType(string productSpecificTypeId);
        Task<Result<List<ProductSpecificType>>> GetAllProductSpecificType();
        Task<Result<ProductSpecificType>> GetProductSpecificTypeBy(string productSpecificTypeId);
    }
}
