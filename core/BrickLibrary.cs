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

        /// <summary>
        /// 数字“0”的形状，相对于中心点的偏移。可以用来显示分数等信息。
        /// </summary>
        public static readonly IntPoint[] Num0 = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 数字“1”的形状，相对于中心点的偏移。可以用来显示分数等信息。
        /// </summary>
        public static readonly IntPoint[] Num1 = new IntPoint[] {
            new IntPoint(1, 0),
            new IntPoint(0, 1),new IntPoint(1, 1),
            new IntPoint(1, 2),
            new IntPoint(1, 3),
            new IntPoint(1, 4)
        };

        /// <summary>
        /// 数字“2”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“Z”混淆。
        /// </summary>
        public static readonly IntPoint[] Num2 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 数字“3”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“B”混淆。
        /// </summary>
        public static readonly IntPoint[] Num3 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),
        };

        /// <summary>
        /// 数字“4”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“A”混淆。
        /// </summary>
        public static readonly IntPoint[] Num4 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(1, 3),
            new IntPoint(1, 4)
        };

        /// <summary>
        /// 数字“5”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“S”混淆。
        /// </summary>
        public static readonly IntPoint[] Num5 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 数字“6”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“G”混淆。
        /// </summary>
        public static readonly IntPoint[] Num6 = new IntPoint[] {
            new IntPoint(-1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 数字“7”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“T”混淆。
        /// </summary>
        public static readonly IntPoint[] Num7 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(0, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 数字“8”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“B”混淆。
        /// </summary>
        public static readonly IntPoint[] Num8 = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 数字“9”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“g”混淆。
        /// </summary>
        public static readonly IntPoint[] Num9 = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(1, 3),
            new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“A”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“4”混淆。
        /// </summary>
        public static readonly IntPoint[] A = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(-1, 1) ,new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“B”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“8”混淆。
        /// </summary>
        public static readonly IntPoint[] B = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“C”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“0”混淆。
        /// </summary>
        public static readonly IntPoint[] C = new IntPoint[] {
            new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),
            new IntPoint(-1, 3),
            new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“D”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“0”和字母“C”混淆。
        /// </summary>
        public static readonly IntPoint[] D = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“E”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] E = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“F”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] F = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4)
        };

        /// <summary>
        /// 字母“G”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“6”和字母“B”混淆。
        /// </summary>
        public static readonly IntPoint[] G = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“H”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] H = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“I”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] I = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(0, 1),
            new IntPoint(0, 2),
            new IntPoint(0, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“J”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] J = new IntPoint[] {
            new IntPoint(1, 0),
            new IntPoint(1, 1),
            new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“K”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] K = new IntPoint[] {
            new IntPoint(-1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),
            new IntPoint(-1, 3),new IntPoint(0, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“L”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] L = new IntPoint[] {
            new IntPoint(-1, 0),
            new IntPoint(-1, 1),
            new IntPoint(-1, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“M”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] M = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(0, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“N”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] N = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“O”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“0”混淆。
        /// </summary>
        public static readonly IntPoint[] O = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“P”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] P = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4)
        };

        /// <summary>
        /// 字母“Q”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“O”混淆。
        /// </summary>
        public static readonly IntPoint[] Q = new IntPoint[] {
            new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(0, 3),
            new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“R”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] R = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(0, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“S”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] S = new IntPoint[] {
            new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),
            new IntPoint(0, 2),
            new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“T”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] T = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(0, 1),
            new IntPoint(0, 2),
            new IntPoint(0, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“U”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] U = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“V”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“U”混淆。
        /// </summary>
        public static readonly IntPoint[] V = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“W”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] W = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(-1, 2),new IntPoint(1, 2),
            new IntPoint(-1, 3),new IntPoint(0, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“X”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与字母“H”混淆。
        /// </summary>
        public static readonly IntPoint[] X = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(-1, 3),new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(1, 4)
        };

        /// <summary>
        /// 字母“Y”的形状，相对于中心点的偏移。采用 3x5 点阵。
        /// </summary>
        public static readonly IntPoint[] Y = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(1, 0),
            new IntPoint(-1, 1),new IntPoint(1, 1),
            new IntPoint(0, 2),new IntPoint(1, 2),
            new IntPoint(1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4)
        };

        /// <summary>
        /// 字母“Z”的形状，相对于中心点的偏移。采用 3x5 点阵，避免与数字“2”混淆。
        /// </summary>
        public static readonly IntPoint[] Z = new IntPoint[] {
            new IntPoint(-1, 0),new IntPoint(0, 0),new IntPoint(1, 0),
            new IntPoint(1, 1),
            new IntPoint(0, 2),
            new IntPoint(-1, 3),
            new IntPoint(-1, 4),new IntPoint(0, 4),new IntPoint(1, 4)
        };

        private static IntPoint[][] CharDics =
        {
            Num0, Num1,Num2,Num3,Num4,Num5,Num6,Num7,Num8,Num9,
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P,Q,R,S,T,U,V,W,X,Y,Z
        };

        public static IntPoint[] getChar(int i)
        {
            return CharDics[i % 36];
        }
    }
}
