using System.ComponentModel;
using System.Windows.Media;

namespace lcd.model
{
    // 像素模型
    public class PixelModel : INotifyPropertyChanged
    {
        private bool _isActive;
        public bool IsActive
        {
			get => _isActive;
			set
			{
				// 避免重复触发属性变更，提升高帧率下的渲染流畅度
				if (_isActive == value)
				{
					return;
				}
				_isActive = value;
				OnPropertyChanged(nameof(IsActive));
				OnPropertyChanged(nameof(Color));
			}
        }

        // 激活时是深黑色，未激活时是极浅的灰色（模拟底纹）
        public Brush Color => IsActive ? Brushes.Black : new SolidColorBrush(System.Windows.Media.Color.FromArgb(20, 0, 0, 0));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
