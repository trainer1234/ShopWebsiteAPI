using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
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
                Id = productView.Id,
                ManufactureYear = productView.ManufactureYear,
                Name = productView.Name,
                Price = productView.Price,
                Type = productView.Type,
                ProductSpecificType = productView.SpecificType,
                PromotionAvailable = productView.PromotionAvailable,
                PromotionRate = productView.PromotionRate,
                Manufacture = new Manufacture(),
                ManufactureId = productView.Manufacture.Id
            };
            product.Manufacture.Id = productView.Manufacture.Id;
            product.Manufacture.Name = productView.Manufacture.Name;

            if(productView.Id != null && productView.ProductImageUrls != null && productView.ProductImageUrls.Count > 0)
            {
                product.ProductImages = new List<ProductImage>();
                var handler = new ImageHandler();
                foreach (var image in productView.ProductImageUrls)
                {
                    var productImage = new ProductImage
                    {
                        ImageModelId = handler.GetImageId(ImageUrlOption.Original, image),
                        ProductId = productView.Id
                    };
                    product.ProductImages.Add(productImage);
                }
            }
            return product;
        }

        public ProductViewModel ParseProductViewFrom(Product product)
        {
            var productView = new ProductViewModel
            {
                PromotionAvailable = product.PromotionAvailable,
                PromotionRate = product.PromotionRate,
                Manufacture = new ManufactureViewModel(),
                ManufactureYear = product.ManufactureYear,
                Name = product.Name,
                Price = product.Price,
                Id = product.Id,
                SpecificType = product.ProductSpecificType,
                Type = product.Type
            };
            productView.Manufacture.Id = product.ManufactureId;
            productView.Manufacture.Name = product.Manufacture.Name;

            if(product.ProductImages != null && product.ProductImages.Count > 0)
            {
                var handler = new ImageHandler();
                productView.ProductImageUrls = new List<string>();
                foreach (var image in product.ProductImages)
                {
                    var url = handler.GetImage(ImageDirectoryOption.Original, ImageUrlOption.Original, image.ImageModelId);
                    productView.ProductImageUrls.Add(url);
                }
            }
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
