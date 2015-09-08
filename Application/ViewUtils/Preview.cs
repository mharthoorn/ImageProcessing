using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageApp
{

    public delegate void ImageEvent(Image image);

    public class FolderPreview
    {
        int top;
        int left;
        string folder;
        Control parent;

        public FolderPreview(Control parent, int left, int top, string folder)
        {
            this.parent = parent;
            this.top = top;
            this.left = left;
            this.folder = folder;
        }
        
        public event ImageEvent ImageClick;

        private void Click(object sender, EventArgs e)
        {
            if (ImageClick != null)
            {
                string file = (string)(sender as PictureBox).Tag;
                Image image = LoadImage(file);
                ImageClick(image);
            }
        }
        
        public void Show()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(folder, "*.png");
            int i = 0;
            foreach (string file in files)
            {
                i++;
                PictureBox box = new PictureBox();
                box.Left = 50 * i;
                box.Top = 30;
                box.Width = 40;
                box.Height = 40;
                box.Parent = parent;
                Image img = LoadImage(file);
                box.Image = CroppedBitmap(img, 40);
                box.Cursor = Cursors.Hand;
                box.Tag = file;
                box.Click += Click;
            }
        }

        public Image LoadImage(string path)
        {
            return Image.FromFile(path);
        }

        public static Bitmap CroppedBitmap(Image image, int width)
        {
            float a = (float)image.Width / (float)image.Height;
            int w = width;
            int h = (int)(w / a);
            Bitmap bitmap = new Bitmap(image, new Size(w, h));
            return bitmap;
        }

    }
}
