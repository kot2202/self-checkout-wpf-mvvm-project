using Self_checkout.WpfApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Self_checkout.WpfApp.Models
{
    public class ProductModel : Product
    {
        public decimal CalculatedQuantity { get; set; }
        public decimal PriceSum { get; set; }
        public ProductModel(Product productToCopyFrom)
        {
            product_img = productToCopyFrom.product_img;
            if(product_img == null)
            {
                product_img = File.ReadAllBytes("Config/placeholderProductImg.jpg");
            }

            product_id = productToCopyFrom.product_id;
            category_id = productToCopyFrom.category_id;
            product_name = productToCopyFrom.product_name;
            product_price = decimal.Round(productToCopyFrom.product_price,2,MidpointRounding.AwayFromZero);
            CalculatedQuantity = 1;
        }

    }
}
