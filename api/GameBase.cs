using lcd.core;
using System;

namespace lcd.api
{
    /// <summary>
    /// IGame 接口的基础实现，提供了通用的底层支持：
    /// - 存储像素宿主和可选的 Screen 包装器
    /// - 基础的 Start/Stop 生命周期标记
    /// - 用于触发常用事件的辅助方法
    /// 
    /// 具体的游戏（如俄罗斯方块、坦克、赛车、贪吃蛇）可以继承此类，
    /// 从而专注于游戏规则和画面绘制。
    /// </summary>
    public abstract class GameBase : IGame
    {
        /// <summary>由 UI 提供的像素宿主。</summary>
        protected IPixelHost PixelHost { get; private set; }

        /// <summary>
        /// 可选的辅助 Screen 类，提供基于“移动形状（MovingShape）”的绘制功能。
        /// 倾向于直接操作像素的游戏可以忽略此项，直接操作 PixelHost。
        /// </summary>
        protected Screen Screen { get; private set; }

        /// <summary>由基类维护的当前分数。</summary>
        public int Score { get; protected set; }

        /// <summary>指示游戏当前是否处于激活/运行状态。</summary>
        protected bool IsRunning { get; private set; }

        public abstract string Id { get; }
        public abstract string DisplayName { get; }

        public event EventHandler<GameOverEventArgs> GameOver;
        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;

        public virtual void Initialize(IPixelHost pixelHost)
        {
            if (pixelHost == null)
            {
                throw new ArgumentNullException("pixelHost");
            }

            PixelHost = pixelHost;
            Screen = new Screen(pixelHost);
        }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public abstract void Update(TimeSpan deltaTime);

        public virtual void OnButtonDown(GameButton button)
        {
        }

        public virtual void OnButtonUp(GameButton button)
        {
        }

        /// <summary>
        /// 供子类游戏调用的辅助方法，用于发出游戏结束信号。
        /// </summary>
        protected void RaiseGameOver(bool isWin)
        {
            var handler = GameOver;
            if (handler != null)
            {
                handler(this, new GameOverEventArgs(isWin, Score));
            }
        }

        /// <summary>
        /// 供子类游戏调用的辅助方法，用于更新分数并通知监听者。
        /// </summary>
        protected void SetScore(int newScore)
        {
            if (Score == newScore)
            {
                return;
            }

            Score = newScore;
            var handler = ScoreChanged;
            if (handler != null)
            {
                handler(this, new ScoreChangedEventArgs(newScore));
            }
        }
    }
}
