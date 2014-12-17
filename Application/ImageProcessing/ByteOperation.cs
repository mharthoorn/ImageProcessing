namespace ImageProcessing
{
    public static class ByteOp
    {
        public static byte Max(int i)
        {
            return (i < byte.MaxValue) ? (byte)i : byte.MaxValue;
        }

        public static byte Bounds(int i)
        {
            i = (i < byte.MaxValue) ? i : byte.MaxValue;
            i = (i > byte.MinValue) ? i : byte.MinValue;
            return (byte)i;
        }

        public static byte Bounds(double i)
        {
            i = (i < byte.MaxValue) ? i : byte.MaxValue;
            i = (i > byte.MinValue) ? i : byte.MinValue;
            return (byte)i;
        }

        public static byte WhenLevel(byte value, byte level, byte alternative)
        {
            return (value >= level) ? value : alternative;
        }

        public static byte WhenLevel(byte value, byte level, byte _then, byte _else)
        {
            return (value >= level) ? _then : _else;
        }

    }
}
