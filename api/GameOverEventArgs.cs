using System;

namespace lcd.api
{
    /// <summary>
    /// 游戏结束事件的数据，包含玩家是否获胜以及最终分数等信息。宿主可以利用这些信息来显示结果、记录分数或返回菜单。
    /// </summary>
    public sealed class GameOverEventArgs : EventArgs
    {
        /// <summary>
        /// 指示玩家是否获胜。
        /// </summary>
        public bool IsWin { get; private set; }

        /// <summary>
        /// 游戏结束时的最终分数。
        /// </summary>
        public int FinalScore { get; private set; }

        public GameOverEventArgs(bool isWin, int finalScore)
        {
            IsWin = isWin;
            FinalScore = finalScore;
        }
    }
}
