using System;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing;
using System.IO;

namespace ImageApp
{
    public partial class Form1 : Form
    {
        // Met Fred gezamelijk directory afspreken?

        Preview preview;

        public Form1()
        {
            InitializeComponent();

            string mydocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = Path.Combine(mydocuments, "ImageProcessing");
            
            preview = new Preview(this, 50, 50, folder);
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
            ImageBuffer buffer;

            buffer = original.IntensityToColor(Color.CadetBlue);
            box1.Display(buffer);

            buffer = original.IntensityToColor(Color.HotPink);
            box2.Display(buffer);
            
            buffer = original.IntensityToColor(Color.IndianRed);
            box3.Display(buffer);

            buffer = original.IntensityToColor(Color.LawnGreen);
            box4.Display(buffer);
        }


    }
}
