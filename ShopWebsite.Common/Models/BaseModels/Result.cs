using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.BaseModels
{
    public class Result<T>
    {
        public T Content { get; set; }
        public bool Succeed { get; set; }
        public Dictionary<int, string> Errors { get; set; }
    }
}
