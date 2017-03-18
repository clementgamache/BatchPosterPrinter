using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xerox_Printer
{
    public partial class Form1 : Form
    {
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            labelSelectedFile.Text = openFileDialog.FileName;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(openFileDialog.FileName);
            //MessageBox.Show(
            //    printDocument.DefaultPageSettings.Margins.Bottom.ToString() + "\n" +
            //    printDocument.DefaultPageSettings.Margins.Bottom.ToString() + "\n" +
            //    printDocument.DefaultPageSettings.Margins.Bottom.ToString() + "\n" +
            //    printDocument.DefaultPageSettings.Margins.Bottom.ToString() + "\n");
            printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(30,30,30,30);
            //printDocument.DefaultPageSettings.PaperSource = printDocument.PrinterSettings.PaperSources[0];
            printDocument.DefaultPageSettings.PaperSize = printDocument.PrinterSettings.PaperSizes[11];
            MessageBox.Show(printDocument.DefaultPageSettings.PaperSize.ToString());
            MessageBox.Show(printDocument.DefaultPageSettings.PaperSource.ToString());
            //printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("12x18", 1200, 1800);//printDocument.PrinterSettings.PaperSizes[11];
            //printDocument.DefaultPageSettings.PrinterSettings.
            //MessageBox.Show(printDocument.PrinterSettings.PaperSizes[11].ToString());
            //for (int i = 0; i < printDocument.PrinterSettings.PaperSources.Count; i++)
            //{
            //    MessageBox.Show(printDocument.PrinterSettings.PaperSources[i].ToString());
            //}
            if (img.Width > img.Height)
            {
                printDocument.DefaultPageSettings.Landscape = true;
            }
            else
            {
                printDocument.DefaultPageSettings.Landscape = false;
            }
            img.Dispose();
            //printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(openFileDialog.FileName);
                
                img = ScaleImage(img, e.MarginBounds.Width, e.MarginBounds.Height); //.Size = e.PageBounds.Size;
                Point loc = new Point(0,0);
                e.Graphics.DrawImage(img, loc);
                img.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
