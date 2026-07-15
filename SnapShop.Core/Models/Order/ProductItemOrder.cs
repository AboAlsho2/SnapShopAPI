using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapShop.Core.Models.Order
{
    public class ProductItemOrder
    {
        public ProductItemOrder(int productID, string productName, string pictureUrl)
        {
            ProductID = productID;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
