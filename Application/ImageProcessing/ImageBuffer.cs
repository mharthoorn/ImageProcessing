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

        public ImageBuffer Clone()
        {
            ImageBuffer buffer = new ImageBuffer();
            buffer.Length = this.Length;
            buffer.Width = this.Width;
            buffer.Height = this.Height;
            buffer.Stride = this.Stride;
            return buffer;
        }
    }

   

    public static class BufferExtensions
    {
        public static ImageBuffer Copy(this ImageBuffer buffer)
        {
            ImageBuffer output = buffer.Clone();
            for (int k = 0; k < output.Length; k += 4)
            {
                output.Bytes[k + 0] = buffer.Bytes[k + 0];
                output.Bytes[k + 1] = buffer.Bytes[k + 1];
                output.Bytes[k + 2] = buffer.Bytes[k + 2];
                output.Bytes[k + 3] = buffer.Bytes[k + 3];
            }
            return output;
        }

        public static ImageBuffer ToImageBuffer(this Bitmap bitmap)
        {
            return new ImageBuffer(bitmap);
            
        }

        public static ImageBuffer When(this ImageBuffer buffer, bool condition, Func<ImageBuffer, ImageBuffer> action)
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
