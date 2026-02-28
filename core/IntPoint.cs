namespace lcd.core
{
    /// <summary>
    /// 表示屏幕上的一个整数像素坐标。
    /// </summary>
    public struct IntPoint
    {
        public int X { get; }
        public int Y { get; }

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static IntPoint operator +(IntPoint a, IntPoint b)
        {
            return new IntPoint(a.X + b.X, a.Y + b.Y);
        }
    }
}
