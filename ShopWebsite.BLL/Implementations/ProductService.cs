using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IErrorLogRepository _errorLogRepository;
        private IProductImageRepository _productImageRepository;
        private IProductPropertyRepository _productPropertyRepository;
        private IProductMapOrderDetailRepository _productMapOrderDetailRepository;

        public ProductService(IProductRepository productRepository, IErrorLogRepository errorLogRepository,
            IProductImageRepository productImageRepository, IProductPropertyRepository productPropertyRepository,
            IProductMapOrderDetailRepository productMapOrderDetailRepository)
        {
            _productRepository = productRepository;
            _errorLogRepository = errorLogRepository;
            _productImageRepository = productImageRepository;
            _productPropertyRepository = productPropertyRepository;
            _productMapOrderDetailRepository = productMapOrderDetailRepository;
        }

        public async Task<Result<bool>> AddProduct(Product newProduct)
        {
            var result = new Result<bool>();
            try
            {
                var productPropTmps = newProduct.ProductProperties;
                var productImageTmps = newProduct.ProductImages;
                newProduct.ProductImages = null;
                newProduct.ProductProperties = null;
                var addResult = await _productRepository.Add(newProduct);
                if (addResult)
                {
                    if (productImageTmps != null && productImageTmps.Count > 0)
                    {
                        newProduct.ProductImages = productImageTmps;
                        foreach (var productImage in newProduct.ProductImages)
                        {
                            await _productImageRepository.Add(productImage);
                        }
                    }
                    if(productPropTmps != null && productPropTmps.Count > 0)
                    {
                        newProduct.ProductProperties = productPropTmps;
                        foreach (var productProp in newProduct.ProductProperties)
                        {
                            await _productPropertyRepository.Add(productProp);
                        }
                    }
                    result.Succeed = true;
                    result.Content = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(4, "Product Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> EditProduct(Product newProduct)
        {
            var result = new Result<bool>();
            try
            {
                if (newProduct.ProductImages != null && newProduct.ProductImages.Count > 0)
                {
                    await _productImageRepository.RemoveByProductId(newProduct.Id);
                    foreach (var productImage in newProduct.ProductImages)
                    {
                        await _productImageRepository.Add(productImage);
                    }
                }
                if(newProduct.ProductProperties != null && newProduct.ProductProperties.Count > 0)
                {
                    await _productPropertyRepository.RemoveByProductId(newProduct.Id);
                    foreach (var productProp in newProduct.ProductProperties)
                    {
                        await _productPropertyRepository.Add(productProp);
                    }
                }
                var editResult = await _productRepository.Edit(newProduct);
                if (editResult)
                {
                    result.Succeed = true;
                    result.Content = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(5, "Product Not Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Product>>> GetAllProduct()
        {
            var result = new Result<List<Product>>();
            try
            {
                var products = await _productRepository.GetAll();
                if (products != null && products.Count > 0)
                {
                    result.Succeed = true;
                    result.Content = products;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(6, "No Products");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Product>>> GetAllProductBy(ProductType type)
        {
            var result = new Result<List<Product>>();
            try
            {
                var products = await _productRepository.GetAllBy(type);
                if (products != null && products.Count > 0)
                {
                    result.Succeed = true;
                    result.Content = products;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(6, "No Products");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Product>>> SearchProduct(string key)
        {
            var result = new Result<List<Product>>();
            try
            {
                var products = await _productRepository.Search(key);
                if (products != null && products.Count > 0)
                {
                    result.Content = products;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(6, "No Products");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<Product>> GetProductBy(string productId)
        {
            var result = new Result<Product>();
            try
            {
                var product = await _productRepository.GetBy(productId);
                if (product != null)
                {
                    result.Succeed = true;
                    result.Content = product;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(7, "No Product");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Product>>> GetRecentProductBy(ProductType type, int num)
        {
            var result = new Result<List<Product>>();
            try
            {
                var products = new List<Product>();
                if (num == 0) products = await _productRepository.GetAll();
                else products = await _productRepository.GetProductBy(type, num);
                if (products != null && products.Count > 0)
                {
                    result.Succeed = true;
                    result.Content = products;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(6, "No Products");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> RemoveProduct(string productId)
        {
            var result = new Result<bool>();
            try
            {
                await _productMapOrderDetailRepository.RemoveByProductId(productId);
                var removeResult = await _productRepository.Remove(productId);
                if (removeResult)
                {
                    result.Succeed = true;
                    result.Content = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(5, "Product Not Exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> IncreaseRemain(string productId, long amount)
        {
            var result = new Result<bool>();
            try
            {
                var editResult = await _productRepository.IncreaseRemain(productId, amount);
                if (editResult)
                {
                    result.Succeed = true;
                    result.Content = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(5, "Product Not Exist");
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
