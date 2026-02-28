namespace lcd.core
{
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
}
