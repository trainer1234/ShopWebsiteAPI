using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.LogModels
{
    public class ErrorLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
