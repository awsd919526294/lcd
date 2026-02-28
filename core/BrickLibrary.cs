namespace lcd.core
{
    /// <summary>
    /// 预定义的像素形状库，例如赛车、方块等。
    /// 所有坐标都以“形状中心点”为原点的偏移量来定义。
    /// </summary>
    public class BrickLibrary
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
}
