using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ProductSpecificTypeService : IProductSpecificTypeService
    {
        private IProductRepository _productRepository;
        private IProductSpecificTypeRepository _productSpecificTypeRepository;
        private IErrorLogRepository _errorLogRepository;

        public ProductSpecificTypeService(IProductSpecificTypeRepository productSpecificTypeRepository, IProductRepository productRepository,
            IErrorLogRepository errorLogRepository)
        {
            _productRepository = productRepository;
            _productSpecificTypeRepository = productSpecificTypeRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<bool>> AddProductSpecificType(ProductSpecificType newProductSpecificType)
        {
            var result = new Result<bool>();
            try
            {
                var addResult = await _productSpecificTypeRepository.Add(newProductSpecificType);
                if (addResult)
                {
                    result.Content = result.Succeed = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(8, "Product Specific Type Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<bool>> EditProductSpeicificType(ProductSpecificType newProductSpecificType)
        {
            var result = new Result<bool>();
            try
            {
                var editResult = await _productSpecificTypeRepository.Edit(newProductSpecificType);
                if (editResult)
                {
                    result.Content = result.Succeed = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(9, "Product Specific Type Not Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<List<ProductSpecificType>>> GetAllProductSpecificType()
        {
            var result = new Result<List<ProductSpecificType>>();
            try
            {
                var productSpecificTypes = await _productSpecificTypeRepository.GetAll();
                if (productSpecificTypes != null && productSpecificTypes.Count > 0)
                {
                    result.Content = productSpecificTypes;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(10, "No Product Specific Types");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<ProductSpecificType>> GetProductSpecificTypeBy(string productSpecificTypeId)
        {
            var result = new Result<ProductSpecificType>();
            try
            {
                var productSpecificType = await _productSpecificTypeRepository.GetBy(productSpecificTypeId);
                if (productSpecificType != null)
                {
                    result.Content = productSpecificType;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(9, "Product Specific Type Not Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<bool>> RemoveProductSpecificType(string productSpecificTypeId)
        {
            var result = new Result<bool>();
            try
            {
                var removeAllProductBySpecificTypeResult = await _productRepository.RemoveAllProductBy(productSpecificTypeId);
                var removeSpecificTypeResult = await _productSpecificTypeRepository.Remove(productSpecificTypeId);
                if (removeSpecificTypeResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(9, "Product Specific Type Not Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }
    }
}
