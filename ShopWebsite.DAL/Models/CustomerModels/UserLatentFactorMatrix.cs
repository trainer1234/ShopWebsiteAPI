using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.CustomerModels
{
    public class UserLatentFactorMatrix
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserItemPredictId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public double CellValue { get; set; }
        public bool IsDisabled { get; set; } = false;

        public UserItemPredict UserItemPredict { get; set; }
    }
}
