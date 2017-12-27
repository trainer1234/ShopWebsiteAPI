using Microsoft.AspNetCore.Http;
using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.ImageModels
{
    public class ImageModelWrapper
    {
        public IFormFile ImgFile { get; set; }
        public ImageModel ImgModel { get; set; }
    }
}
