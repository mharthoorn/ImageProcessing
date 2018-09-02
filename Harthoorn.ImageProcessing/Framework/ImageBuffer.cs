using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{



    public class ImageBuffer // Mathematically Processable Image Pixel Buffer 
    {
        public int Width;
        public int Height;
        public byte[] Bytes;
        public int Stride;

        public ImageBuffer(Bitmap bitmap)
        {
            FromBitmap(bitmap);
        }

        public ImageBuffer()
        {

        }

        public int Length
        {
            get
            {
                return this.Bytes.Length;
            }
            set
            {
                this.Bytes = new byte[value];
            }
        }

        public void FromBitmap(Bitmap bitmap)
        {
            this.Width = bitmap.Width;
            this.Height = bitmap.Height;

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            Bytes = new byte[data.Stride * data.Height];
            this.Stride = data.Stride;

            Marshal.Copy(data.Scan0, Bytes, 0, Bytes.Length);
            bitmap.UnlockBits(data);
        }

        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(Bytes, 0, data.Scan0, Bytes.Length);
            bitmap.UnlockBits(data);

            return bitmap;
        }

        public ImageBuffer CloneFormat()
        {
            ImageBuffer buffer = new ImageBuffer();
            buffer.Length = this.Length;
            buffer.Width = this.Width;
            buffer.Height = this.Height;
            buffer.Stride = this.Stride;
            return buffer;
        }

        public ImageBuffer Clone()
        {
            ImageBuffer output = this.CloneFormat();
            for (int k = 0; k < output.Length; k += 4)
            {
                output.Bytes[k + 0] = this.Bytes[k + 0];
                output.Bytes[k + 1] = this.Bytes[k + 1];
                output.Bytes[k + 2] = this.Bytes[k + 2];
                output.Bytes[k + 3] = this.Bytes[k + 3];
            }
            return output;
        }
    }


    /// <summary>
    ///  A function that takes an <see cref="ImageBuffer"/> as input and outputs a new or modified <see cref="ImageBuffer"/>, so that you
    /// can chain (pipeline) them
    /// </summary>
    /// <param name="buffer">Input buffer</param>
    /// <returns>Output buffer</returns>
    public delegate ImageBuffer Pipeline(ImageBuffer buffer);

    public static class ImageBufferExtensions
    {
        static Func<ImageBuffer, ImageBuffer> pipe;

        public static ImageBuffer ToImageBuffer(this Bitmap bitmap)
        {
            return new ImageBuffer(bitmap);
            
        }

        /// <summary>
        /// A fluent if-statement that only puts the <see cref="ImageBuffer"/> through the pipeline when the <see cref="condition"/> is met
        /// </summary>
        public static ImageBuffer When(this ImageBuffer buffer, bool condition, Pipeline action)
        {
            if (condition)
            {
                return action(buffer);
            }
            else
            {
                return buffer;
            }
        }

    }
}
