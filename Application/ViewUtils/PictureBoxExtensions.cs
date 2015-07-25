using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public static class PictureBoxExtensions
    {
        public static void Display(this PictureBox box, ImageBuffer buffer)
        {
            box.Image = buffer.ToBitmap();
        }
    }
}
