using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class SuperImpozing
    {
       
        public static ImageBuffer Add(params ImageBuffer[] buffers)
        {
            ImageBuffer output = buffers[0].Clone();
            for (int k = 0; k < output.Bytes.Length; k += 4)
            {
                for (int i = 0; i < buffers.Count(); i++)
                {
                    output.Bytes[k + 0] = ByteOp.Max(output.Bytes[k + 0] + buffers[i].Bytes[k + 0]);
                    output.Bytes[k + 1] = ByteOp.Max(output.Bytes[k + 1] + buffers[i].Bytes[k + 1]);
                    output.Bytes[k + 2] = ByteOp.Max(output.Bytes[k + 2] + buffers[i].Bytes[k + 2]);
                    output.Bytes[k + 3] = 255;
                }

            }
            return output;
        }

        public static ImageBuffer SuperImpoze(this ImageBuffer image, ImageBuffer impoze)
        {
            ImageBuffer output = image.Copy();
            for (int k = 0; k < output.Bytes.Length; k += 4)
            {
                output.Bytes[k + 0] = ByteOp.WhenLevel(impoze.Bytes[k + 0], 1, output.Bytes[k + 0]);
                output.Bytes[k + 1] = ByteOp.WhenLevel(impoze.Bytes[k + 1], 1, output.Bytes[k + 1]);
                output.Bytes[k + 2] = ByteOp.WhenLevel(impoze.Bytes[k + 2], 1, output.Bytes[k + 2]);
                output.Bytes[k + 3] = 255;

            }
            return output;
        }

        public static ImageBuffer SuperImpozeRGB(this ImageBuffer buffer)
        {
            ImageBuffer c3 = buffer.ThreeColors(0, 255);

            ImageBuffer red = buffer.Filter(Color.FromArgb(255, 0, 0)).Filter(c3);
            ImageBuffer green = buffer.Filter(Color.FromArgb(0, 255, 0)).Filter(c3);
            ImageBuffer blue = buffer.Filter(Color.FromArgb(0, 0, 255)).Filter(c3);

            ImageBuffer output = SuperImpozing.Add(red, green, blue);
            return output;

        }

    }
}
