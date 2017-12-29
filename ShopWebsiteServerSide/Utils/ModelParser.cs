using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsite.DAL.Models.PropertyModels;
using ShopWebsiteServerSide.Models.LogModels;
using ShopWebsiteServerSide.Models.ManufactureModels;
using ShopWebsiteServerSide.Models.ProductModels;
using ShopWebsiteServerSide.Models.PropertyModels;
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
                ProductSpecificType = productView.SpecificType,
                PromotionAvailable = productView.PromotionAvailable,
                PromotionRate = productView.PromotionRate,
                ManufactureId = productView.Manufacture.Id
            };
            if(productView.Id != null)
            {
                product.Id = productView.Id;
            }

            if(productView.ProductPropertyViews != null && productView.ProductPropertyViews.Count > 0)
            {
                product.ProductProperties = new List<ProductProperty>();
                foreach (var productPropView in productView.ProductPropertyViews)
                {
                    var productProp = new ProductProperty
                    {
                        ProductId = product.Id,
                        PropertyId = productPropView.PropertyId,
                        PropertyDetail = productPropView.PropertyDetail
                    };
                    product.ProductProperties.Add(productProp);
                }
            }

            if(productView.ProductImageUrls != null && productView.ProductImageUrls.Count > 0)
            {
                product.ProductImages = new List<ProductImage>();
                var handler = new ImageHandler();
                foreach (var image in productView.ProductImageUrls)
                {
                    var productImage = new ProductImage
                    {
                        ImageModelId = handler.GetImageId(ImageUrlOption.Original, image),
                        ProductId = product.Id
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

            if(product.ProductProperties != null && product.ProductProperties.Count > 0)
            {
                productView.ProductPropertyViews = new List<ProductPropertyViewModel>();
                foreach (var productProp in product.ProductProperties)
                {
                    var productPropView = new ProductPropertyViewModel
                    {
                        PropertyId = productProp.PropertyId,
                        PropertyDetail = productProp.PropertyDetail
                    };
                    productView.ProductPropertyViews.Add(productPropView);
                }
            }

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
                Name = manufacture.Name,
            };
            if(manufacture.ManufactureTypes != null && manufacture.ManufactureTypes.Count > 0)
            {
                manufactureView.Types = new List<ProductType>();
                foreach (var type in manufacture.ManufactureTypes)
                {
                    manufactureView.Types.Add(type.Type);
                }
            }
            return manufactureView;
        }

        public Manufacture ParseManufactureFrom(ManufactureViewModel manufactureView)
        {
            var manufacture = new Manufacture
            {
                Name = manufactureView.Name
            };
            if(manufactureView.Id != null)
            {
                manufacture.Id = manufactureView.Id;
            }
            if(manufactureView.Types != null && manufactureView.Types.Count > 0)
            {
                manufacture.ManufactureTypes = new List<ManufactureType>();
                foreach (var typeView in manufactureView.Types)
                {
                    var type = new ManufactureType
                    {
                        ManufactureId = manufacture.Id,
                        Type = typeView
                    };
                    manufacture.ManufactureTypes.Add(type);
                }
            }
            return manufacture;
        }

        public Property ParsePropertyFrom(PropertyViewModel propertyView)
        {
            var property = new Property
            {
                Name = propertyView.Name
            };
            if(propertyView.Id != null)
            {
                property.Id = propertyView.Id;
            }
            return property;
        }

        public PropertyViewModel ParsePropertyViewFrom(Property property)
        {
            var propertyView = new PropertyViewModel
            {
                Id = property.Id,
                Name = property.Name
            };
            return propertyView;
        }
    }
}
