using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.SlideModels
{
    public class Slide
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int OrderId { get; set; }
        public string ImageModelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SlideImageUrl { get; set; }
        public ImageModel ImageModel { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
