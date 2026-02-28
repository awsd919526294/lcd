using System;

namespace lcd.api
{
    /// <summary>
    /// 游戏分数发生变化时的事件数据。宿主可以监听此事件来更新 UI 显示或进行其他响应（如音效）。
    /// </summary>
    public sealed class ScoreChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 新的分数值。当游戏逻辑更新分数时，应该触发 ScoreChanged 事件，并传递新的分数值。宿主可以利用这个值来更新显示或进行其他响应（如播放音效）。
        /// </summary>
        public int NewScore { get; private set; }

        public ScoreChangedEventArgs(int newScore)
        {
            NewScore = newScore;
        }
    }
}
