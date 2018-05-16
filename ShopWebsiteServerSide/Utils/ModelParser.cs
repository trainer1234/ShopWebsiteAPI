using ShopWebsite.Common.Models.AccountModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ManufactureModels;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.LogModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsite.DAL.Models.ProductOrderModels;
using ShopWebsite.DAL.Models.PropertyModels;
using ShopWebsite.DAL.Models.SlideModels;
using ShopWebsiteServerSide.Models.CustomerModels;
using ShopWebsiteServerSide.Models.LogModels;
using ShopWebsiteServerSide.Models.OrderModels;
using ShopWebsiteServerSide.Models.ProductModels;
using ShopWebsiteServerSide.Models.PropertyModels;
using ShopWebsiteServerSide.Models.SlideModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Utils
{
    public class ModelParser
    {
        public UserViewModel ParseUserViewFrom(User user)
        {
            var userView = new UserViewModel
            {
                Id = user.Id,
                AvatarUrl = user.AvatarUrl,
                Role = new RoleViewModel(),
                UserName = user.UserName,
                Birthday = user.Birthday,
                FullName = user.FullName,
                Income = user.Income
            };
            userView.Role.Id = user.Role;
            if (user.Role == UserRole.Admin)
            {
                userView.Role.Name = "Quản trị viên";
            }
            else if (user.Role == UserRole.Manager)
            {
                userView.Role.Name = "Quản lý";
            }
            else if (user.Role == UserRole.Staff)
            {
                userView.Role.Name = "Nhân viên";
            }
            else
            {
                if(user.UserHobbies != null && user.UserHobbies.Count > 0)
                {
                    userView.Hobbies = new List<ManufactureViewModel>();
                    foreach (var userHobby in user.UserHobbies)
                    {
                        var userHobbyView = new ManufactureViewModel
                        {
                            Id = userHobby.ManufactureId,
                            Name = userHobby.Manufacture.Name,
                            Types = new List<ProductType>()
                        };
                        foreach (var type in userHobby.Manufacture.ManufactureTypes)
                        {
                            userHobbyView.Types.Add(type.Type);
                        }
                        userView.Hobbies.Add(userHobbyView);
                    }
                }
            }

            return userView;
        }

        public User ParseUserFrom(UserViewModel userView)
        {
            var user = new User
            {
                AvatarUrl = userView.AvatarUrl,
                Role = userView.Role.Id,
                UserName = userView.UserName,
                FullName = userView.FullName,
                Birthday = userView.Birthday,
                Income = userView.Income
            };
            if (userView.Id != null)
            {
                user.Id = userView.Id;
            }
            if (userView.Hobbies != null && userView.Hobbies.Count > 0)
            {
                user.UserHobbies = new List<UserHobby>();
                foreach (var hobbyView in userView.Hobbies)
                {
                    user.UserHobbies.Add(new UserHobby
                    {
                        ManufactureId = hobbyView.Id,
                        UserId = user.Id
                    });
                }
            }
            return user;
        }
        public ErrorLogViewModel ParseErrorLogViewFrom(ErrorLog errorLog)
        {
            var errorLogView = new ErrorLogViewModel
            {
                Content = errorLog.Content,
                CreatedTime = errorLog.CreatedTime
            };
            return errorLogView;
        }

        public CustomerRating ParseCustomerRatingFrom(CustomerRatingViewModel customerRatingView)
        {
            var customerRating = new CustomerRating
            {
                ProductId = customerRatingView.ProductId,
                UserId = customerRatingView.UserId,
                Rating = customerRatingView.Rating
            };

            return customerRating;
        }

        public CustomerRatingViewModel ParseCustomerRatingViewFrom(CustomerRating customerRating)
        {
            var customerRatingView = new CustomerRatingViewModel
            {
                ProductId = customerRating.ProductId,
                UserId = customerRating.UserId,
                Rating = customerRating.Rating
            };

            return customerRatingView;
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
                PromotionRate = productView.PromotionRate,
                ManufactureId = productView.Manufacture.Id,
                Remain = productView.Remain,
                Detail = productView.Detail
            };
            if (productView.Id != null)
            {
                product.Id = productView.Id;
            }

            if (productView.Properties != null && productView.Properties.Count > 0)
            {
                product.ProductProperties = new List<ProductProperty>();
                foreach (var productPropView in productView.Properties)
                {
                    var productProp = new ProductProperty
                    {
                        ProductId = product.Id,
                        PropertyId = productPropView.Id,
                        PropertyDetail = productPropView.Detail
                    };
                    product.ProductProperties.Add(productProp);
                }
            }

            if (productView.ProductImageUrls != null && productView.ProductImageUrls.Count > 0)
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
                PromotionRate = product.PromotionRate,
                Manufacture = new ManufactureViewModel(),
                ManufactureYear = product.ManufactureYear,
                Name = product.Name,
                Price = product.Price,
                Remain = product.Remain,
                Id = product.Id,
                SpecificType = product.ProductSpecificType,
                Type = product.Type,
                Detail = product.Detail,
                View = product.View,
                PurchaseCounter = product.PurchaseCounter
            };
            productView.Manufacture.Id = product.ManufactureId;
            productView.Manufacture.Name = product.Manufacture.Name;

            if (product.ProductProperties != null && product.ProductProperties.Count > 0)
            {
                productView.Properties = new List<ProductPropertyViewModel>();
                foreach (var productProp in product.ProductProperties)
                {
                    var productPropView = new ProductPropertyViewModel
                    {
                        Id = productProp.PropertyId,
                        Detail = productProp.PropertyDetail,
                        Name = productProp.Property.Name
                    };
                    productView.Properties.Add(productPropView);
                }
            }

            if (product.ProductImages != null && product.ProductImages.Count > 0)
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
                Types = new List<ProductType>()
            };
            if (manufacture.ManufactureTypes != null && manufacture.ManufactureTypes.Count > 0)
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
            if (manufactureView.Id != null)
            {
                manufacture.Id = manufactureView.Id;
            }
            if (manufactureView.Types != null && manufactureView.Types.Count > 0)
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
            if (propertyView.Id != null)
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

        public Customer ParseCustomerFrom(CustomerViewModel customerView)
        {
            var customer = new Customer
            {
                FullName = customerView.FullName,
                Address = customerView.Address,
                Email = customerView.Email,
                Phone = customerView.Phone,
                Id = customerView.Id
            };
            return customer;
        }

        public CustomerViewModel ParseCustomerViewFrom(Customer customer)
        {
            var customerView = new CustomerViewModel
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone
            };
            return customerView;
        }

        public ProductOrder ParseProductOrderFrom(ProductOrderPostViewModel productOrderView)
        {
            var customer = ParseCustomerFrom(productOrderView.Customer);
            var productOrder = new ProductOrder
            {
                OrderStatus = productOrderView.OrderStatus,
                Customer = customer
            };
            if (productOrderView.OrderId != null)
            {
                productOrder.OrderId = productOrderView.OrderId;
            }
            if (productOrderView.Products != null && productOrderView.Products.Count > 0)
            {
                productOrder.ProductMapOrderDetails = new List<ProductMapOrderDetail>();
                foreach (var product in productOrderView.Products)
                {
                    if (product.Id != null)
                    {
                        var productMapOrderDetail = new ProductMapOrderDetail
                        {
                            ProductId = product.Id,
                            ProductAmount = product.Amount,
                            ProductOrderId = productOrder.Id
                        };
                        productOrder.ProductMapOrderDetails.Add(productMapOrderDetail);
                    }
                }
            }
            return productOrder;
        }

        public ProductOrder ParseProductOrderFrom(ProductOrderPutViewModel productOrderView)
        {
            var customer = ParseCustomerFrom(productOrderView.Customer);
            var productOrder = new ProductOrder
            {
                OrderStatus = productOrderView.OrderStatus,
                Customer = customer
            };
            if (productOrderView.OrderId != null)
            {
                productOrder.OrderId = productOrderView.OrderId;
            }
            if (productOrderView.Products != null && productOrderView.Products.Count > 0)
            {
                productOrder.ProductMapOrderDetails = new List<ProductMapOrderDetail>();
                foreach (var product in productOrderView.Products)
                {
                    if (product.Product.Id != null)
                    {
                        var productMapOrderDetail = new ProductMapOrderDetail
                        {
                            ProductId = product.Product.Id,
                            ProductAmount = product.Amount,
                            ProductOrderId = productOrder.Id
                        };
                        productOrder.ProductMapOrderDetails.Add(productMapOrderDetail);
                    }
                }
            }
            return productOrder;
        }

        public ProductOrderViewModel ParseProductOrderViewFrom(ProductOrder productOrder)
        {
            var customerView = ParseCustomerViewFrom(productOrder.Customer);
            var productOrderView = new ProductOrderViewModel
            {
                Id = productOrder.Id,
                Customer = customerView,
                OrderDate = productOrder.OrderDate,
                OrderId = productOrder.OrderId,
                OrderStatus = productOrder.OrderStatus,
                ProductAmount = productOrder.ProductTotalAmount,
                TotalCost = productOrder.TotalCost
            };
            if (productOrder.ProductMapOrderDetails != null && productOrder.ProductMapOrderDetails.Count > 0)
            {
                productOrderView.Products = new List<ProductOfProductOrderViewModel>();
                foreach (var productMapOrder in productOrder.ProductMapOrderDetails)
                {
                    var productView = ParseProductViewFrom(productMapOrder.Product);
                    var productOfProductOrderView = new ProductOfProductOrderViewModel
                    {
                        Amount = productMapOrder.ProductAmount,
                        Product = productView
                    };
                    productOrderView.Products.Add(productOfProductOrderView);
                }
            }
            return productOrderView;
        }

        public SlideViewModel ParseSlideViewFrom(Slide slide)
        {
            var slideView = new SlideViewModel
            {
                Title = slide.Title,
                Description = slide.Description,
                Id = slide.Id,
                SlideImageUrl = slide.SlideImageUrl
            };

            return slideView;
        }

        public Slide ParseSlideFrom(SlideViewModel slideView)
        {
            var slide = new Slide
            {
                Title = slideView.Title,
                Description = slideView.Description,
                SlideImageUrl = slideView.SlideImageUrl
            };

            return slide;
        }

    }
}
