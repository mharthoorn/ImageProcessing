using System;
using System.Drawing;

/*
 * A part of the following code was based on code with the following licence:
 * http://opensource.org/licenses/MS-PL
 *
 * This code is based on code developed by:
 * Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/


namespace ImageProcessing
{

    public static partial class Convolutions
    {
        // See also: http://matlabtricks.com/post-5/3x3-convolution-kernels-with-online-demo#demo

        public static ImageBuffer Convolution(this ImageBuffer buffer, double[,] kernel, double factor = 1, int bias = 0)
        {
            byte[] source = buffer.Bytes;

            ImageBuffer output = buffer.Fill(Color.Black);
            byte[] target = output.Bytes;

            double blue, green, red;

            int width = kernel.GetLength(0);
            //int height = kernel.GetLength(1);  // ?? wordt (nog?) niet gebruikt (20150101 FH)

            int matrixOffset = (width - 1) / 2;

            int calcOffset, byteOffset;

            for (int offsetY = matrixOffset; offsetY < buffer.Height - matrixOffset; offsetY++)
            for (int offsetX = matrixOffset; offsetX < buffer.Width - matrixOffset; offsetX++)
            {
                blue = 0;
                green = 0;
                red = 0;

                byteOffset = offsetY * buffer.Stride + offsetX * 4;

                for (int filterY = -matrixOffset; filterY <= matrixOffset; filterY++)
                for (int filterX = -matrixOffset; filterX <= matrixOffset; filterX++)
                {
                    calcOffset = byteOffset + (filterX * 4) + (filterY * buffer.Stride);

                    blue +=  (double)(source[calcOffset + 0]) * kernel[filterY + matrixOffset, filterX + matrixOffset];
                    green += (double)(source[calcOffset + 1]) * kernel[filterY + matrixOffset, filterX + matrixOffset];
                    red +=   (double)(source[calcOffset + 2]) * kernel[filterY + matrixOffset, filterX + matrixOffset];
                }

                blue =  factor * blue  + bias;
                green = factor * green + bias;
                red =   factor * red   + bias;

                target[byteOffset + 0] = ByteConversion.Bounded(blue);
                target[byteOffset + 1] = ByteConversion.Bounded(green);
                target[byteOffset + 2] = ByteConversion.Bounded(red);
                target[byteOffset + 3] = 255;
            }

            return output;
        }

        public static ImageBuffer Convolution(this ImageBuffer buffer, double[,] xKernel, double[,] yKernel, double factor = 1, int bias = 0)
        {
            byte[] source = buffer.Bytes;

            ImageBuffer output = buffer.CloneFormat();
            byte[] target = output.Bytes;

            double blueX, greenX, redX, blueY, greenY, redY;
            double blueTotal, greenTotal, redTotal;

            int filterOffset = 1;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY < buffer.Height - filterOffset; offsetY++)
            for (int offsetX = filterOffset; offsetX < buffer.Width - filterOffset; offsetX++)
            {
                blueX = greenX = redX = 0;
                blueY = greenY = redY = 0;

                blueTotal = greenTotal = redTotal = 0.0;

                byteOffset = offsetY * buffer.Stride + offsetX * 4;

                for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                {
                    calcOffset = byteOffset + (filterX * 4) + (filterY * buffer.Stride);

                    blueX  += (double)(source[calcOffset + 0]) * xKernel[filterY + filterOffset, filterX + filterOffset];
                    greenX += (double)(source[calcOffset + 1]) * xKernel[filterY + filterOffset, filterX + filterOffset];
                    redX   += (double)(source[calcOffset + 2]) * xKernel[filterY + filterOffset, filterX + filterOffset];

                    blueY  += (double)(source[calcOffset + 0]) * yKernel[filterY + filterOffset, filterX + filterOffset];
                    greenY += (double)(source[calcOffset + 1]) * yKernel[filterY + filterOffset, filterX + filterOffset];
                    redY   += (double)(source[calcOffset + 2]) * yKernel[filterY + filterOffset, filterX + filterOffset];
                }

                blueTotal =  Math.Sqrt((blueX * blueX)   + (blueY * blueY));
                greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                redTotal =   Math.Sqrt((redX * redX)     + (redY * redY));

                target[byteOffset + 0] = ByteConversion.Bounded(blueTotal);
                target[byteOffset + 1] = ByteConversion.Bounded(greenTotal);
                target[byteOffset + 2] = ByteConversion.Bounded(redTotal);
                target[byteOffset + 3] = 255;

            }
            return output;
        }

        public static ImageBuffer Laplacian3x3(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Laplacian3x3, 1.0, 0);
        }

        public static ImageBuffer Laplacian5x5(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Laplacian5x5, 1.0, 0);
        }

        public static ImageBuffer Laplacian7x7(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Martijn7x7, 1.0, 0);
        }

        public static ImageBuffer LaplacianOfGaussian(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.LaplacianOfGaussian, 1.0, 0);
        }

        public static ImageBuffer Laplacian3x3OfGaussian3x3(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian3x3, 1.0 / 16.0, 0).Convolution(Kernel.Laplacian3x3, 1.0, 0);
        }

        public static ImageBuffer Laplacian3x3OfGaussian5x5_One(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian5x5Type1, 1.0 / 159.0, 0).Convolution(Kernel.Laplacian3x3, 1.0, 0);
        }

        public static ImageBuffer Laplacian3x3OfGaussian5x5_Two(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian5x5Type2, 1.0 / 256.0, 0).Convolution(Kernel.Laplacian3x3, 1.0, 0);
        }

        public static ImageBuffer Laplacian5x5OfGaussian3x3(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian3x3, 1.0 / 16.0, 0).Convolution(Kernel.Laplacian5x5, 1.0, 0);
        }

        public static ImageBuffer Laplacian5x5OfGaussian5x5_One(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian5x5Type1, 1.0 / 159.0, 0).Convolution(Kernel.Laplacian5x5, 1.0, 0);
        }

        public static ImageBuffer Laplacian5x5OfGaussian5x5_Two(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Gaussian5x5Type2, 1.0 / 256.0, 0).Convolution(Kernel.Laplacian5x5, 1.0, 0);
        }

        public static ImageBuffer Sobel3x3(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Sobel3x3Horizontal, Kernel.Sobel3x3Vertical, 1.0, 0);
        }

        public static ImageBuffer Prewitt(this ImageBuffer buffer)
        {
            // Image should be grayscale 
            return buffer.Convolution(Kernel.Prewitt3x3Horizontal, Kernel.Prewitt3x3Vertical, 1.0, 0);
        }

        public static ImageBuffer Kirsch(this ImageBuffer buffer)
        {
            // Image should be grayscale
            return buffer.Convolution(Kernel.Kirsch3x3Horizontal, Kernel.Kirsch3x3Vertical, 1.0, 0);
        }

        public static ImageBuffer Invert(this ImageBuffer buffer)
        {
            ImageBuffer output = buffer.CloneFormat();

            for (int k = 0; k < buffer.Length; k += 4)
            {

                output.Bytes[k + 0] = (byte)(255 - buffer.Bytes[k + 0]);
                output.Bytes[k + 1] = (byte)(255 - buffer.Bytes[k + 1]);
                output.Bytes[k + 2] = (byte)(255 - buffer.Bytes[k + 2]);
                output.Bytes[k + 3] = buffer.Bytes[k + 3];
            }

            return output;
        }

    }


}
