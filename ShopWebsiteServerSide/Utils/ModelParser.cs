using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsiteServerSide.Models.LogModels;
using ShopWebsiteServerSide.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Utils
{
    public class ModelParser
    {
        public ErrorLogViewModel ParseErrorLogViewFrom(ErrorLog errorLog)
        {
            var errorLogView = new ErrorLogViewModel
            {
                Content = errorLog.Content,
                CreatedTime = errorLog.CreatedTime
            };
            return errorLogView;
        }

        public Product ParseProductFrom(ProductViewModel productView)
        {
            var product = new Product
            {
                ManufactureName = productView.ManufactureName,
                ManufactureYear = productView.ManufactureYear,
                Name = productView.Name,
                Price = productView.Price,
                ProductImageUrl = productView.ProductImageUrl,
                Type = productView.Type,
                ProductSpecificTypeId = productView.ProductSpecificType.Id,
            };
            return product;
        }

        public ProductViewModel ParseProductViewFrom(Product product)
        {
            var productView = new ProductViewModel
            {
                ManufactureName = product.ManufactureName,
                ManufactureYear = product.ManufactureYear,
                Name = product.Name,
                Price = product.Price,
                ProductImageUrl = product.ProductImageUrl,
                Id = product.Id,
                ProductSpecificType = new ProductSpecificTypeViewModel(),
                Type = product.Type
            };
            if(product.ProductSpecificType != null)
            {
                productView.ProductSpecificType.Id = product.ProductSpecificType.Id;
                productView.ProductSpecificType.Name = product.ProductSpecificType.Name;
                productView.ProductSpecificType.Type = product.ProductSpecificType.Type;
            }
            return productView;
        }

        public ProductSpecificType ParseProductSpecificTypeFrom(ProductSpecificTypeViewModel productSpecificTypeView)
        {
            var productSpecificType = new ProductSpecificType
            {
                Id = productSpecificTypeView.Id,
                Name = productSpecificTypeView.Name,
                Type = productSpecificTypeView.Type
            };
            return productSpecificType;
        }

        public ProductSpecificTypeViewModel ParseProductSpecificTypeViewFrom(ProductSpecificType productSpecificType)
        {
            var productSpecificTypeView = new ProductSpecificTypeViewModel
            {
                Id = productSpecificType.Id,
                Name = productSpecificType.Name,
                Type = productSpecificType.Type
            };
            return productSpecificTypeView;
        }
    }
}
