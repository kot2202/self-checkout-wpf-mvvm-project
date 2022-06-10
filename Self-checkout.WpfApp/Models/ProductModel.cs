using Self_checkout.WpfApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Self_checkout.WpfApp.Models
{
    public class ProductModel : Product
    {
        public ProductModel(Product productToCopyFrom)
        {
            product_img = productToCopyFrom.product_img;
            product_id = productToCopyFrom.product_id;
            category_id = productToCopyFrom.category_id;
            product_name = productToCopyFrom.product_name;
            product_price = productToCopyFrom.product_price;
            CalculatedQuantity = 1;
        }

        public decimal CalculatedQuantity { get; set; }
    }
}
