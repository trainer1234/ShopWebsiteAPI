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
            throw new NotImplementedException();
        }

        public ProductViewModel ParseProductViewFrom(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
