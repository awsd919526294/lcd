namespace lcd.api
{
    /// <summary>
    /// 手持设备上的逻辑按键。
    /// 宿主（UI）代码应当将具体的物理按键（键盘或按钮）事件转换为这些值，
    /// 并转发给当前活动的游戏。
    /// </summary>
    public enum GameButton
    {
        Up,
        Down,
        Left,
        Right,
        /// <summary>
        /// 主要功能/动作按钮（例如旋转、开火、确认）。
        /// </summary>
        Function
    }
}
