using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class Css
    {
        public static Color ToColor(string value)
        {
            if (value.StartsWith("#")) 
                value = value.Remove(0, 1);
            
            byte R = Convert.ToByte(value.Substring(0, 2), 16);
            byte G = Convert.ToByte(value.Substring(2, 2), 16);
            byte B = Convert.ToByte(value.Substring(4, 2), 16);

            Color c = Color.FromArgb(R, G, B);

            return c;
        }
    }
   
}
