using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lcd.core
{
    /// <summary>
    /// 游戏机屏幕类，底层实现屏幕的显示功能，提供接口供游戏逻辑调用以更新屏幕显示。
    /// 该类只关心“像素坐标”和“形状”，不依赖具体 UI 技术。
    /// </summary>
    public class Screen
    {
        private readonly IPixelHost _host;
        private readonly List<MovingShape> _shapes = new List<MovingShape>();

        public int Width
        {
            get { return _host.PixelWidth; }
        }

        public int Height
        {
            get { return _host.PixelHeight; }
        }

        public Screen(IPixelHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            _host = host;
        }

        /// <summary>
        /// 注册一个要在屏幕上展示和移动的形状实例。
        /// </summary>
        public void AddShape(MovingShape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException("shape");
            }

            _shapes.Add(shape);
        }

        /// <summary>
        /// 执行一次屏幕更新：
        /// 1. 清空所有像素；
        /// 2. 按照各自的方向和速度移动所有形状；
        /// 3. 重新在新位置绘制所有形状。
        /// </summary>
        public void Step()
        {
            _host.ClearPixels();

            foreach (var shape in _shapes)
            {
                shape.MoveWithin(this);
                DrawShape(shape);
            }
        }

        /// <summary>
        /// 检查某个形状如果移动到新的中心点，是否会越出屏幕边界。
        /// </summary>
        internal bool WouldShapeOutOfBounds(MovingShape shape, IntPoint newCenter)
        {
            foreach (var offset in shape.Offsets)
            {
                int cellX = newCenter.X + offset.X;
                int cellY = newCenter.Y + offset.Y;

                if (cellX < 0 || cellX >= Width || cellY < 0 || cellY >= Height)
                {
                    return true;
                }
            }

            return false;
        }

        private void DrawShape(MovingShape shape)
        {
            foreach (var offset in shape.Offsets)
            {
                int cellX = shape.Center.X + offset.X;
                int cellY = shape.Center.Y + offset.Y;

                if (cellX < 0 || cellX >= Width || cellY < 0 || cellY >= Height)
                {
                    continue;
                }

                _host.SetPixel(cellX, cellY, true);
            }
        }

        /// <summary>
        /// NOTE: Comments are currently in English and should be translated to Chinese.
        /// 慢刷新效果：从下到上逐行填充屏幕，然后从上到下逐行清除。
        /// 用于增强效果。在20行屏幕上使用默认计时，整个循环大约需要4秒。
        /// </summary>
        /// <param name="rowDelayMilliseconds">每行填充/清除步骤之间的延迟。</param>
        public async Task SlowRefreshAsync(int rowDelayMilliseconds = 100)
        {
            // Start from a clean screen so the effect is clear.
            _host.ClearPixels();

            // Fill from bottom (Height - 1) to top (0).
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    _host.SetPixel(x, y, true);
                }

                await Task.Delay(rowDelayMilliseconds).ConfigureAwait(true);
            }

            // 然后从顶部（0）到底部（Height-1）清除。
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _host.SetPixel(x, y, false);
                }

                await Task.Delay(rowDelayMilliseconds).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// 快速刷新效果：与SlowRefreshAsync模式相同，但行之间的延迟更短。
        /// 用于快速反馈，例如当玩家失去生命但游戏尚未结束时。
        /// </summary>
        /// <param name="rowDelayMilliseconds">每行填充/清除步骤之间的延迟。</param>
        public Task FastRefreshAsync(int rowDelayMilliseconds = 25)
        {
            return SlowRefreshAsync(rowDelayMilliseconds);
        }

        /// <summary>
        /// 螺旋刷新效果：从右上角开始一次填充一个单元格，向左移动直到碰到边缘或已访问的单元格，
        /// 然后以螺旋模式向下、向右和向上旋转，直到所有单元格都被填充。
        /// 之后，使用相同的顺序清除细胞。用于游戏效果。
        /// </summary>
        /// <param name="cellDelayMilliseconds">每次单元格更新之间的延迟。</param>
        public async Task SpiralRefreshAsync(int cellDelayMilliseconds = 20)
        {
            // Precompute the spiral path over the entire screen.
            var order = BuildSpiralOrder();

            // Start from a clean screen.
            _host.ClearPixels();

            // Fill following the spiral order.
            foreach (var point in order)
            {
                _host.SetPixel(point.X, point.Y, true);
                await Task.Delay(cellDelayMilliseconds).ConfigureAwait(true);
            }

            // Then clear following the same spiral order.
            foreach (var point in order)
            {
                _host.SetPixel(point.X, point.Y, false);
                await Task.Delay(cellDelayMilliseconds).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// NOTE: Comments are currently in English and should be translated to Chinese.
        /// 构建一个螺旋遍历顺序，从右上角开始，最初向左移动，精确地覆盖屏幕上的每个单元格一次。
        /// </summary>
        private IList<IntPoint> BuildSpiralOrder()
        {
            var result = new List<IntPoint>(Width * Height);
            if (Width <= 0 || Height <= 0)
            {
                return result;
            }

            var visited = new bool[Width, Height];

            int x = Width - 1;
            int y = 0;

            // Directions: left, down, right, up
            int[,] directions =
            {
                { -1, 0 },
                { 0, 1 },
                { 1, 0 },
                { 0, -1 }
            };

            int dirIndex = 0;
            int totalCells = Width * Height;

            for (int i = 0; i < totalCells; i++)
            {
                result.Add(new IntPoint(x, y));
                visited[x, y] = true;

                int nextX = x + directions[dirIndex, 0];
                int nextY = y + directions[dirIndex, 1];

                bool outOfBounds = nextX < 0 || nextX >= Width || nextY < 0 || nextY >= Height;
                if (outOfBounds || visited[nextX, nextY])
                {
                    dirIndex = (dirIndex + 1) % 4;
                    nextX = x + directions[dirIndex, 0];
                    nextY = y + directions[dirIndex, 1];
                }

                x = nextX;
                y = nextY;
            }

            return result;
        }
    }
}
