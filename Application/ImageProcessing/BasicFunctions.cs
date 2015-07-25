using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class BasicFunctions
    {

        public static ImageBuffer Fill(this ImageBuffer buffer, Color color)
        {
            ImageBuffer output = buffer.CloneFormat();
            for (int k = 0; k < output.Bytes.Length; k += 4)
            {
                output.Bytes[k + 0] = color.B;
                output.Bytes[k + 1] = color.G;
                output.Bytes[k + 2] = color.R;
                output.Bytes[k + 3] = 255;

            }
            return output;
        }
        
    }
}
