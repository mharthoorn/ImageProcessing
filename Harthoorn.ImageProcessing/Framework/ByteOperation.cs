namespace ImageProcessing
{
    public static class ByteConversion
    {
        public static byte Max(int i)
        {
            return (i < byte.MaxValue) ? (byte)i : byte.MaxValue;
        }

        public static byte Bounded(int i)
        {
            i = (i < byte.MaxValue) ? i : byte.MaxValue;
            i = (i > byte.MinValue) ? i : byte.MinValue;
            return (byte)i;
        }

        public static byte Bounded(double i)
        {
            i = (i < byte.MaxValue) ? i : byte.MaxValue;
            i = (i > byte.MinValue) ? i : byte.MinValue;
            return (byte)i;
        }

        public static byte MinAtLevel(byte value, byte level, byte alternative)
        {
            return (value >= level) ? value : alternative;
            
            // return WhenLevel(value, level, value, alternative);
        }

        public static byte LevelSplit(byte value, byte level, byte _then, byte _else)
        {
            return (value >= level) ? _then : _else;
        }

    }
}
