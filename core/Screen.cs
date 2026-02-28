using System;
using System.Collections.Generic;

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
    }
}
