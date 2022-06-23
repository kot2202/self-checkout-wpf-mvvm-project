using Self_checkout.WpfApp.Models;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Config
{
    public class Receipt
    {
        private readonly CheckoutViewModel _viewModel;

        public Receipt(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Print()
        {
            // PLACEHOLDER MICROSOFT PRINT TO PDF
            // ORDERS MADE IN QUICK SUCCESSION MAY OVERWRITE THE .PDF PRINTOUTS ( WON'T FIX )
            // CHANGE TO REAL PRINTER IN FUTURE

            string fileNamePrefix = "Receipt_";
            string fileNameSuffix = _viewModel.OrderTime.ToString("dd-MM-yyyy_HH-mm-ss");

            // the directory to store the output.
            string directory = "./PrintOuts/";

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var printDoc = new PrintDocument
            {
                PrinterSettings =
                {
                    // set the printer to 'Microsoft Print to PDF'
                    // change to real printer in future
                    PrinterName = "Microsoft Print to PDF",
                    PrintToFile = true,
                    PrintFileName = Path.Combine(directory, fileNamePrefix + fileNameSuffix + ".pdf")
        },
                PrintController = new StandardPrintController()
            };

            printDoc.PrintPage += OnPrintPage;
            printDoc.Print();
        }
        

        private void OnPrintPage(object sender, PrintPageEventArgs ev)
        {
            System.Drawing.Font headingFont = new System.Drawing.Font("Calibri", 9, System.Drawing.FontStyle.Bold);
            System.Drawing.Font boldFont = new System.Drawing.Font("Calibri", 7, System.Drawing.FontStyle.Bold);
            System.Drawing.Font normalFont = new System.Drawing.Font("Calibri", 7, System.Drawing.FontStyle.Regular);

            string currency = "PLN";


            float height = 30;

            string line = "---------------------------------------------------------------------------------------";

            //PRINT Company Logo
            Image logoImage = Image.FromFile("Config/Images/receiptLogo.jpg");
            ev.Graphics.DrawImage(logoImage, 128, 10);
            height += 30;

            //Print Company Name
            ev.Graphics.DrawString("SAMPLE BILL", headingFont, Brushes.Black, 108, height, new StringFormat());
            height += 30;
            //Print Company Address
            ev.Graphics.DrawString("12-345, City, Street ABC 5", normalFont, Brushes.Black,90 , height, new StringFormat());
            height += 30;

            //Print Receipt No
            ev.Graphics.DrawString("Receipt No : " + _viewModel.OrderId, boldFont, Brushes.Black, 10, height, new StringFormat());
            //Print Receipt Date
            ev.Graphics.DrawString("Date : " + _viewModel.OrderTime, boldFont, Brushes.Black, 165, height, new StringFormat());
            height += 20;

            //Print Line
            ev.Graphics.DrawString(line, normalFont, Brushes.Black, 10, height, new StringFormat());
            height += 20;

            ev.Graphics.DrawString("Name", normalFont, Brushes.Black, 10, height, new StringFormat());
            ev.Graphics.DrawString("Qty", normalFont, Brushes.Black, 165, height, new StringFormat());
            ev.Graphics.DrawString("Price", normalFont, Brushes.Black, 213, height, new StringFormat());
            ev.Graphics.DrawString("Sum", normalFont, Brushes.Black, 255, height, new StringFormat());
            height += 20;

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Price");
            dt.Columns.Add("Sum");

            foreach (ProductModel product in _viewModel.ListOfProducts)
            {
                DataRow dr = dt.NewRow();
                dr[0] = product.product_name;
                dr[1] = product.CalculatedQuantity;
                dr[2] = product.product_price;
                dr[3] = product.PriceSum;
                dt.Rows.Add(dr);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SizeF qtyWidth = ev.Graphics.MeasureString(dt.Rows[i][1].ToString(), normalFont);
                SizeF priceWidth = ev.Graphics.MeasureString(dt.Rows[i][2].ToString(), normalFont);
                SizeF totalWidth = ev.Graphics.MeasureString(dt.Rows[i][3].ToString(), normalFont);

                ev.Graphics.DrawString(dt.Rows[i][0].ToString(), normalFont, Brushes.Black, 10, height, new StringFormat());
                ev.Graphics.DrawString(dt.Rows[i][1].ToString(), normalFont, Brushes.Black, 134 + (50 - qtyWidth.Width), height, new StringFormat());
                ev.Graphics.DrawString(dt.Rows[i][2].ToString(), normalFont, Brushes.Black, 187 + (50 - priceWidth.Width), height, new StringFormat());
                ev.Graphics.DrawString(dt.Rows[i][3].ToString(), normalFont, Brushes.Black, 226 + (50 - totalWidth.Width), height, new StringFormat());
                height += 20;
            }
            //Print Line
            ev.Graphics.DrawString(line, normalFont, Brushes.Black, 10, height, new StringFormat());
            height += 20;

            //Print Total value
            ev.Graphics.DrawString($"Total: {decimal.Round(_viewModel.PriceSum,2,MidpointRounding.AwayFromZero)} {currency}", boldFont, Brushes.Black, 190, height, new StringFormat());
        }
    }
}
