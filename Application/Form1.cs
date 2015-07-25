using System;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing;
using System.IO;

namespace ImageApp
{
    public partial class Form1 : Form
    {
        // Gezamelijke directory voor opslaan van plaatjes.
        // Voorlopig gekozen voor ...\MyDocuments\ImageProcessing

        public Form1()
        {
            InitializeComponent();

            string mydocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = Path.Combine(mydocuments, "ImageProcessing");

            Preview preview = new Preview(this, 50, 50, folder);
            preview.Show();
            preview.ImageClick += Preview_ImageClick;
        }

        private void ClearBoxes()
        {
            box1.Image = null;
            box2.Image = null;
            box3.Image = null;
            box4.Image = null;
        }

        private void Preview_ImageClick(Image image)
        {
            ClearBoxes();

            ImageBuffer buffer = Preview.CroppedBitmap(image, 400).ToImageBuffer();
            Process(buffer);
        }

        private void Process(ImageBuffer original)
        {
            box1.Display(original);

            ImageBuffer buffer = original.GrayScale();
            box2.Display(buffer);

            ImageBuffer buffer2 = buffer.CreepArea(20);
          
            box3.Display(buffer2);

            ImageBuffer buffer3 = buffer.NineColors();
            box4.Display(buffer3);
        }


    }
}
