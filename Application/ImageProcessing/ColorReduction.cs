using System.Drawing;

namespace ImageProcessing
{

    public static class ColorReduction
    {
        public static ImageBuffer GrayScale(this ImageBuffer buffer)
        {
            byte[] source = buffer.Bytes;
            ImageBuffer output = buffer.Clone();
            byte[] dest = output.Bytes;

            float rgb = 0;
            for (int k = 0; k < source.Length; k += 4)
            {
                // http://en.wikipedia.org/wiki/Grayscale
                rgb =  source[k + 0] * 0.11f;
                rgb += source[k + 1] * 0.59f;
                rgb += source[k + 2] * 0.3f;

                dest[k] = (byte)rgb;
                dest[k + 1] = dest[k];
                dest[k + 2] = dest[k];
                dest[k + 3] = 255;
            }
            return output;
        }

        public static ImageBuffer SimpleGrayScale(this ImageBuffer buffer)
        {
            byte[] source = buffer.Bytes;
            ImageBuffer output = buffer.Clone();
            byte[] dest = output.Bytes;

            byte rgb = 0;
            for (int k = 0; k < source.Length; k += 4)
            {
                rgb = (byte) ( (source[k + 0] + source[k + 1]  + source[k + 2]) / 3 );

                dest[k + 0] = rgb;
                dest[k + 1] = rgb;
                dest[k + 2] = rgb;
                dest[k + 3] = 255;
            }
            return output;
        }

        public static ImageBuffer IntensityToColor(this ImageBuffer buffer, Color color)
        {
            ImageBuffer output = buffer.Clone();

            float intensity;
            for (int k = 0; k < buffer.Length; k += 4)
            {
                intensity  = buffer.Bytes[k + 0] * 0.11f;
                intensity += buffer.Bytes[k + 1] * 0.59f;
                intensity += buffer.Bytes[k + 2] * 0.3f;

                output.Bytes[k + 0] = (byte)(color.B * intensity / 255);
                output.Bytes[k + 1] = (byte)(color.G * intensity / 255);
                output.Bytes[k + 2] = (byte)(color.R * intensity / 255);
                output.Bytes[k + 3] = 255;

            }
            return output;
        }

        public static ImageBuffer Binary(this ImageBuffer buffer, byte limit)
        {
            byte[] source = buffer.Bytes;

            ImageBuffer output = buffer.Clone();
            byte[] bytes = output.Bytes;

            float rgb = 0;
            for (int k = 0; k < source.Length; k += 4)
            {
                // http://en.wikipedia.org/wiki/Grayscale
                rgb  = source[k + 0] * 0.11f;
                rgb += source[k + 1] * 0.59f;
                rgb += source[k + 2] * 0.3f;

                rgb = (rgb > limit) ? 255 : 0;
                bytes[k] = (byte)(rgb);
                bytes[k + 1] = bytes[k];
                bytes[k + 2] = bytes[k];
                bytes[k + 3] = 255;
            }
            return output;
        }

        public static ImageBuffer NineColors(this ImageBuffer buffer)
        {
            byte[] source = buffer.Bytes;
            ImageBuffer output = buffer.Clone();
            byte[] result = output.Bytes;

            for (int k = 0; k < source.Length; k += 4)
            {
                result[k + 0] = (source[k + 0] > 127) ? (byte)255 : (byte)0;
                result[k + 1] = (source[k + 1] > 127) ? (byte)255 : (byte)0;
                result[k + 2] = (source[k + 2] > 127) ? (byte)255 : (byte)0;
                result[k + 3] = 255;
            }
            return output;
        }

        public static ImageBuffer ThreeColors(this ImageBuffer buffer, int low, int high)
        {
            // Red, Green, Blue, Black or White
            byte[] source = buffer.Bytes;
            ImageBuffer output = buffer.Clone();
            byte[] result = output.Bytes;

            for (int k = 0; k < source.Length; k += 4)
            {
                byte r = source[k];
                byte g = source[k + 1];
                byte b = source[k + 2];

                if (r < low && g < low && b < low)
                {
                    r = 0; b = 0; g = 0;
                }
                else if (r > high && g > high && b > high)
                {
                    r = 255; b = 255; g = 255;
                }
                else if (r > g && r > b)
                {
                    r = 255; g = 0; b = 0;
                }
                else if (g > r && g > b)
                {
                    r = 0; g = 255; b = 0;
                }
                else if (b > r && b > g)
                {
                    r = 0; g = 0; b = 255;
                }
                result[k] = r;
                result[k + 1] = g;
                result[k + 2] = b;
                result[k + 3] = 255;
            }
            return output;
        }


        public static ImageBuffer Filter(this ImageBuffer buffer, Color color)
        {
            ImageBuffer output = buffer.Clone();

            for (int k = 0; k < buffer.Bytes.Length; k += 4)
            {
                output.Bytes[k + 0] = (byte)(color.B * buffer.Bytes[k + 0] / 255);
                output.Bytes[k + 1] = (byte)(color.G * buffer.Bytes[k + 1] / 255);
                output.Bytes[k + 2] = (byte)(color.R * buffer.Bytes[k + 2] / 255);
                output.Bytes[k + 3] = 255;
            }
            return output;
        }

        public static ImageBuffer PureRGB(this ImageBuffer buffer, byte level, Color color)
        {
            ImageBuffer output = buffer.Clone();

            for (int k = 0; k < buffer.Bytes.Length; k += 4)
            {
                output.Bytes[k + 0] = ByteOp.WhenLevel(buffer.Bytes[k + 0], level, color.B, 0);
                output.Bytes[k + 1] = ByteOp.WhenLevel(buffer.Bytes[k + 1], level, color.G, 0);
                output.Bytes[k + 2] = ByteOp.WhenLevel(buffer.Bytes[k + 2], level, color.R, 0);
                output.Bytes[k + 3] = 255;
            }
            return output;
        }

        
        public static ImageBuffer Filter(this ImageBuffer buffer, ImageBuffer filter)
        {
            // The filter image determines the intensity for each pixel R,G,B
            ImageBuffer output = buffer.Clone();

            for (int k = 0; k < buffer.Bytes.Length; k += 4)
            {
                output.Bytes[k + 0] = (byte)(filter.Bytes[k + 0] * buffer.Bytes[k + 0] / 255);
                output.Bytes[k + 1] = (byte)(filter.Bytes[k + 1] * buffer.Bytes[k + 1] / 255);
                output.Bytes[k + 2] = (byte)(filter.Bytes[k + 2] * buffer.Bytes[k + 2] / 255);
                output.Bytes[k + 3] = buffer.Bytes[k + 3];
            }
            return output;

        }

        public static ImageBuffer QuantizeColors(this ImageBuffer buffer, int partitions)
        {
            // Reduces the amount of colors by boxing them three dimensionally
            // http://en.wikipedia.org/wiki/Color_quantization
            byte[] bytes = buffer.Bytes;

            ImageBuffer output = buffer.Clone();

            int step = (256 / partitions);
            int half = (step / 2);
            byte r, g, b;

            for (int k = 0; k < bytes.Length; k += 4)
            {
                r = bytes[k + 0];
                g = bytes[k + 1];
                b = bytes[k + 2];

                r = (byte)((r / step) * step+half);
                g = (byte)((g / step) * step+half);
                b = (byte)((b / step) * step+half);

                output.Bytes[k + 0] = r;
                output.Bytes[k + 1] = g;
                output.Bytes[k + 2] = b;
                output.Bytes[k + 3] = 255;
            }

            return output;

        }
    }

    
}
