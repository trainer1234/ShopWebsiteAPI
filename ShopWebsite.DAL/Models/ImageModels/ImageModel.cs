using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ImageModels
{
    public class ImageModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Type { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
