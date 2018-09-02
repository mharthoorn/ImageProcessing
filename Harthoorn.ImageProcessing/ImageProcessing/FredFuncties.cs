using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class FredsImageBufferExtensions
    {
        public static ImageBuffer DoeIets(this ImageBuffer input)
        {
            return input.Clone(); 
        }
    }

}
