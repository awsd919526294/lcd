using System;

namespace lcd.core
{
    /// <summary>
    /// 在屏幕上可以移动的形状实例，包含形状、中心点、方向和速度等信息。
    /// </summary>
    public class MovingShape
    {
        public IntPoint[] Offsets { get; set; }
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
}
