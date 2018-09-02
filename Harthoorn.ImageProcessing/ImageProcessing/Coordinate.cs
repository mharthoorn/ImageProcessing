namespace ImageProcessing
{
    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }

    public static class CoordinateExtensions
    {
        public static Coordinate Left(this Coordinate c)
        {
            return new Coordinate(c.X - 1, c.Y);
        }

        public static Coordinate Right(this Coordinate c)
        {
            return new Coordinate(c.X + 1, c.Y);
        }
    }
}