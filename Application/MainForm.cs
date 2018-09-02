using System;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing;
using System.IO;

namespace ImageApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitPreview(@"..\..\..\SampleImages");
        }

        private void InitPreview(string folder)
        {
            var preview = new FolderPreview(this, 50, 50, folder);
            preview.Show();
            preview.ImageClick += Preview_ImageClick;
        }

        private void Preview_ImageClick(Image image)
        {
            ImageBuffer buffer = FolderPreview.CroppedBitmap(image, 400).ToImageBuffer();
            Process(buffer);
        }

        private void Process(ImageBuffer original)
        {
            box1.Display(original);

            ImageBuffer buffer = original.GrayScale();
            box2.Display(buffer);

            ImageBuffer buffer2 = buffer.CreepArea(20);
          
            box3.Display(buffer2);

            ImageBuffer buffer3 = buffer2.Kirsch();
            box4.Display(buffer3);
        }

    }
}
