using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsiteServerSide.Models.LogModels;
using ShopWebsiteServerSide.Models.ManufactureModels;
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
                ManufactureYear = productView.ManufactureYear,
                Name = productView.Name,
                Price = productView.Price,
                Type = productView.Type,
                ProductSpecificType = productView.SpecificType
            };
            return product;
        }

        public ProductViewModel ParseProductViewFrom(Product product)
        {
            var productView = new ProductViewModel
            {
                ManufactureYear = product.ManufactureYear,
                Name = product.Name,
                Price = product.Price,
                Id = product.Id,
                SpecificType = product.ProductSpecificType,
                Type = product.Type
            };
            return productView;
        }

        public ManufactureViewModel ParserManufactureViewFrom(Manufacture manufacture)
        {
            var manufactureView = new ManufactureViewModel
            {
                Id = manufacture.Id,
                Name = manufacture.Name
            };
            return manufactureView;
        }

        public Manufacture ParseManufactureFrom(ManufactureViewModel manufactureView)
        {
            var manufacture = new Manufacture
            {
                Id = manufactureView.Id,
                Name = manufactureView.Name
            };
            return manufacture;
        }
    }
}
