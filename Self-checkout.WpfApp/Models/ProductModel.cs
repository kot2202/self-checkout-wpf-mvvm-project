using Self_checkout.WpfApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Self_checkout.WpfApp.Models
{
    public class ProductModel : Product
    {
        public ProductModel(int q = 1) : base()
        {
            CalculatedQuantity = q;
        }

        public float CalculatedQuantity { get; set; }
        public BitmapImage convertedImage { get; set; }
    }
}
