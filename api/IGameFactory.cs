namespace lcd.api
{
    /// <summary>
    /// 用于创建游戏实例的工厂接口。
    /// 允许宿主维护一份可用游戏列表，并在游戏被选中时才延迟实例化。
    /// </summary>
    public interface IGameFactory
    {
        /// <summary>
        /// 与所创建游戏的 Id 相匹配的唯一标识符。
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 在菜单中显示的简短名称。
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// 创建一个新的游戏实例。宿主随后会调用 Initialize 方法。
        /// </summary>
        IGame Create();
    }
}
