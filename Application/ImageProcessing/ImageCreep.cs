using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing
{

    public static partial class ImageCreep
    {

        private static int Index(this ImageBuffer buffer, Coordinate coord)
        {
            return (coord.X * 4) + (coord.Y * buffer.Stride);
        }

        public static ImageBuffer MapAndAverage(this ImageBuffer buffer, int x, int y)
        {
            var map = buffer.CloneFormat().Fill(Color.FromArgb(0));
            var work = new List<Coordinate>();
            var clone = buffer.Clone();

            var origin = new Coordinate(x, y);

            int i = map.Index(origin);
            map.Bytes[i] = 1;


            int j = map.Index(origin.Right());

            return map;
        }

        public static void AverageOut(this byte[] buffer, int p1, int p2)
        {
            int d = Math.Abs(buffer[p1] - buffer[p2]);
            if (d < 15 && d > 0)
            {
                byte value = ByteConversion.Max((buffer[p1] + buffer[p2]) / 2);
                buffer[p1 + 0] = value; // blue
                buffer[p1 + 1] = value; // green
                buffer[p1 + 2] = value; // red
                buffer[p1 + 3] = 255;   // transparency

                buffer[p2 + 0] = value; // blue
                buffer[p2 + 1] = value; // green
                buffer[p2 + 2] = value; // red
                buffer[p2 + 3] = 255;   // transparency
            }
        }

        /// <summary>
        /// Tries to build areas of similar coloured connected pixels
        /// (this Function assumes a grayscale image as imput(
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static ImageBuffer CreepArea(this ImageBuffer buffer)
        {
            byte[] source = buffer.Bytes;

            ImageBuffer output = buffer.Clone();
            byte[] target = output.Bytes;

            for (int y = 0 ; y < output.Height - 1 ; y++)
            {
                for (int x = 1 ; x < output.Width - 1 ; x++)
                {
                    int ix = y * output.Stride + x * 4;

                    if (ix % 2 == 0)
                    {
                        int il = ix - 4;
                        ImageCreep.AverageOut(target, ix, il);
                    }
                    else
                    {
                        int ir = ix + 4;
                        ImageCreep.AverageOut(target, ix, ir);
                    }
                    
                    int id = (y + 1) * output.Stride + x * 4;

                    ImageCreep.AverageOut(target, ix, id);


                }
            }

            return output;
        }

        public static ImageBuffer CreepArea(this ImageBuffer buffer, int repeats)
        {
            ImageBuffer clone = buffer.Clone();
            for (int i = 0 ; i < repeats ; i++)
            {
                clone = clone.CreepArea();
            }
            return clone;
        }
    }
}