using System;
using System.Collections.Generic;

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

    /// <summary>
    /// 移动或旋转的方向枚举，表示形状在屏幕上移动的方向。
    /// </summary>
    public enum Direction
    {
        Up, Down, Left, Right
    }

    /// <summary>
    /// 屏幕宿主接口，由具体 UI（例如 WPF 窗口）实现，用于真正点亮/熄灭像素。
    /// </summary>
    public interface IPixelHost
    {
        /// <summary>屏幕的宽度（列数）。</summary>
        int PixelWidth { get; }

        /// <summary>屏幕的高度（行数）。</summary>
        int PixelHeight { get; }

        /// <summary>清空所有像素。</summary>
        void ClearPixels();

        /// <summary>设置指定像素的开关状态。</summary>
        /// <param name="x">列坐标，从 0 开始。</param>
        /// <param name="y">行坐标，从 0 开始。</param>
        /// <param name="isOn">是否点亮。</param>
        void SetPixel(int x, int y, bool isOn);
    }

    /// <summary>
    /// 预定义的像素形状库，例如赛车、方块等。
    /// 所有坐标都以“形状中心点”为原点的偏移量来定义。
    /// </summary>
    public static class BrickLibrary
    {
        /// <summary>
        /// 赛车游戏中的一级赛车形状，相对于中心点的偏移。
        /// </summary>
        public static readonly IntPoint[] FormulaOneCar = new IntPoint[]
        {
            new IntPoint(0, 0),				 // 车头
			new IntPoint(-1, 1), new IntPoint(0, 1), new IntPoint(1, 1), // 前轮/车身
			new IntPoint(0, 2),				 // 车腰
			new IntPoint(-1, 3), new IntPoint(1, 3) // 后轮
		};
        /// <summary>
        /// 坦克大战游戏中的玩家坦克形状，相对于中心点的偏移。
        /// </summary>
        public static readonly IntPoint[] PlayerTank = new IntPoint[] {
            new IntPoint(0, -1),				 // 炮口
			new IntPoint(-1, 0), new IntPoint(0, 0), new IntPoint(1, 0), // 车身
			new IntPoint(-1, 1), new IntPoint(0, 1), new IntPoint(1, 1) // 履带
		};

        /// <summary>
        /// 坦克大战游戏中的敌方坦克形状，需要区分玩家坦克和敌方坦克，相对于中心点的偏移。
        /// </summary>
        public static readonly IntPoint[] EnermyTank = new IntPoint[] {
             new IntPoint(0, -1),				 // 炮口
			new IntPoint(-1, 0), new IntPoint(0, 0), new IntPoint(1, 0), // 车身
			new IntPoint(-1, 1), new IntPoint(1, 1) // 履带
        };

        /// <summary>
        /// 赛车或坦克爆炸时的碎片形状，相对于爆炸中心点的偏移。
        /// </summary>
        public static readonly IntPoint[] Explode = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(2, 0),
            new IntPoint(0, 1),new IntPoint(1, 1),
            new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(2, 3)
        };
    }

    /// <summary>
    /// 在屏幕上可以移动的形状实例，包含形状、中心点、方向和速度等信息。
    /// </summary>
    public class MovingShape
    {
        public IntPoint[] Offsets { get; }
        public IntPoint Center { get; set; }

        /// <summary>
        /// 当前形状朝向的方向（例如坦克炮口朝上/下/左/右），主要用于射击等逻辑。
        /// </summary>
        public Direction Facing { get; set; }

        /// <summary>
        /// 移动方向（用于决定每次 Step 向哪一个方向移动一格，可以与 Facing 不同，实现平移/侧移等效果）。
        /// </summary>
        public Direction MoveDirection { get; set; }

        /// <summary>移动速度，每次 Step 移动的格数。</summary>
        public int Speed { get; set; }

        /// <summary>
        /// 兼容旧代码的构造函数：通过一个方向向量创建形状，
        /// 同时将 Facing 和 MoveDirection 都设置为该向量所代表的方向。
        /// </summary>
        public MovingShape(IntPoint[] offsets, IntPoint center, IntPoint direction, int speed)
            : this(offsets, center, VectorToDirection(direction), VectorToDirection(direction), speed)
        {
        }

        /// <summary>
        /// 使用明确的面对方向和移动方向创建一个可移动形状。
        /// </summary>
        public MovingShape(IntPoint[] offsets, IntPoint center, Direction facing, Direction moveDirection, int speed)
        {
            if (offsets == null)
            {
                throw new ArgumentNullException("offsets");
            }
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException("speed");
            }

            Offsets = offsets;
            Center = center;
            Facing = facing;
            MoveDirection = moveDirection;
            Speed = speed;
        }

        /// <summary>
        /// 在给定屏幕范围内尝试按照当前方向和速度移动一次，如果会越界则保持原位。
        /// </summary>
		public void MoveWithin(Screen screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            // 速度为 0 或者没有有效移动方向时，不移动。
            if (Speed == 0)
            {
                return;
            }
            var dirVector = DirectionToVector(MoveDirection);
            if (dirVector.X == 0 && dirVector.Y == 0)
            {
                return;
            }

            var step = new IntPoint(dirVector.X * Speed, dirVector.Y * Speed);
            var candidateCenter = new IntPoint(Center.X + step.X, Center.Y + step.Y);
            // 屏幕宿主会负责裁剪越界像素，这里始终更新中心点，
            // 这样形状可以从屏幕外逐渐进入/离开（用于走马灯等效果）。
            Center = candidateCenter;
        }

        /// <summary>
        /// 将枚举方向转换为对应的单位向量，方便移动计算。
        /// </summary>
        private static IntPoint DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new IntPoint(0, -1);
                case Direction.Down:
                    return new IntPoint(0, 1);
                case Direction.Left:
                    return new IntPoint(-1, 0);
                case Direction.Right:
                    return new IntPoint(1, 0);
                default:
                    return new IntPoint(0, 0);
            }
        }

        /// <summary>
        /// 将一个简单的方向向量（如 0,1 / 0,-1 / 1,0 / -1,0）转换为枚举方向。
        /// 如果向量不是这四种之一，默认返回 Down，保证兼容性。
        /// </summary>
        private static Direction VectorToDirection(IntPoint vector)
        {
            if (vector.X == 0 && vector.Y < 0)
            {
                return Direction.Up;
            }
            if (vector.X == 0 && vector.Y > 0)
            {
                return Direction.Down;
            }
            if (vector.X < 0 && vector.Y == 0)
            {
                return Direction.Left;
            }
            if (vector.X > 0 && vector.Y == 0)
            {
                return Direction.Right;
            }

            // 默认向下，尽量保证旧代码行为接近预期
            return Direction.Down;
        }
    }

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
