using lcd.core;
using System;

namespace lcd.api
{
    /// <summary>
    /// 运行在 LCD 屏幕上的单个游戏的基类接口。
    /// 宿主（如 WPF 窗口）负责创建游戏实例、绑定输入，
    /// 并通过不断调用 Update 方法来驱动游戏循环。
    /// </summary>
    public interface IGame
    {
        /// <summary>唯一标识符，用于在代码或存档数据中区分不同的游戏。</summary>
        string Id { get; }

        /// <summary>简短的显示名称，用于在菜单中展示。</summary>
        string DisplayName { get; }

        /// <summary>
        /// 游戏创建后的初始化调用。
        /// 游戏可以在此处保留对像素宿主的引用，并进行一次性的设置。
        /// 宿主拥有实际的屏幕渲染表面。
        /// </summary>
        void Initialize(IPixelHost pixelHost);

        /// <summary>
        /// 当游戏变为当前活动状态并开始运行时调用（例如玩家从菜单选择了该游戏）。
        /// </summary>
        void Start();

        /// <summary>
        /// 当游戏不再处于活动状态或被销毁时调用。
        /// 游戏应当停止逻辑更新，并在必要时释放临时状态。
        /// </summary>
        void Stop();

        /// <summary>
        /// 由宿主每帧调用。deltaTime 是自上次调用以来的真实时间间隔，
        /// 用于驱动内部逻辑（如移动、计时）。
        /// 游戏负责在每一帧将其画面绘制到像素宿主上。
        /// </summary>
        void Update(TimeSpan deltaTime);

        /// <summary>
        /// 当逻辑按键被按下时调用。
        /// </summary>
        void OnButtonDown(GameButton button);

        /// <summary>
        /// 当逻辑按键被释放时调用。
        /// </summary>
        void OnButtonUp(GameButton button);

        /// <summary>
        /// 当游戏结束（玩家获胜、失败或退出）时触发。
        /// 宿主可以利用此事件返回菜单或记录分数。
        /// </summary>
        event EventHandler<GameOverEventArgs> GameOver;

        /// <summary>
        /// 当显示分数发生变化时触发。
        /// </summary>
        event EventHandler<ScoreChangedEventArgs> ScoreChanged;
    }
}
