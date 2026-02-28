using lcd.core;
using lcd.model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace lcd
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, IPixelHost
    {
        // 存放200个方块的数据源
        public ObservableCollection<PixelModel> Pixels { get; set; } = new ObservableCollection<PixelModel>();
        private DispatcherTimer _timer;
        private Screen _screen;
        private MovingShape _car;
        private Stopwatch _stopwatch;
        private double _accumulatedMs;

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                if (value <= 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(SpeedLevel));
                }
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged(nameof(Level));
                }
            }
        }

        private int _speedLevel = 1;
        public int SpeedLevel
        {
            get => _speedLevel;
            set
            {
                if (value <= 0 || value > 15)
                {
                    throw new ArgumentOutOfRangeException(nameof(SpeedLevel));
                }
                if (_speedLevel != value)
                {
                    _speedLevel = value;
                    OnPropertyChanged(nameof(SpeedLevel));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            // 让 XAML 中的绑定可以直接绑定到窗口自身的属性
            DataContext = this;

            // 初始化界面显示的分数与速度
            Score = 0;
            SpeedLevel = 10;
            Level = 1;

            // 1. 初始化 200 个像素点
            for (int i = 0; i < 200; i++)
            {
                Pixels.Add(new PixelModel { IsActive = false });
            }

            PixelGrid.ItemsSource = Pixels;

            // 2. 初始化底层 Screen，并启动一个简单的示例动画
            StartSimpleAnimation();
        }
        int index = 0;

        private void StartSimpleAnimation()
        {
            // 新实现：使用 CompositionTarget.Rendering + Stopwatch，实现固定刷新率驱动，
            // 按 500 / _speedLevel 毫秒作为一次“逻辑移动”的时间间隔。
            _screen = new Screen(this);
            _car = new MovingShape(
                // 初始使用第一个字符，后续在下落离开屏幕后再依次切换
                BrickLibrary.getChar(index),
                new IntPoint(4, -4),  // 初始中心位置（从屏幕上方逐渐进入）
                new IntPoint(0, 1),   // 向下移动
                1                     // 每次 Step 移动 1 格
            );
            _screen.AddShape(_car);

            _stopwatch = Stopwatch.StartNew();
            _accumulatedMs = 0;
            CompositionTarget.Rendering += OnRendering;
        }

        /// <summary>
        /// 每一帧屏幕刷新时触发，根据真实经过时间决定是否执行一步逻辑移动。
        /// </summary>
        private void OnRendering(object sender, EventArgs e)
        {
            if (_stopwatch == null || _screen == null || _car == null)
            {
                return;
            }

            // 距离上一帧实际经过的毫秒数
            double elapsedMs = _stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Restart();
            _accumulatedMs += elapsedMs;

            // 约定：移动一次消耗 500 / _speedLevel 毫秒（_speedLevel > 0）
            double moveInterval = 500.0 / _speedLevel;
            // 累积时间足够时，可能需要执行多步逻辑，以避免丢帧
            while (_accumulatedMs >= moveInterval)
            {
                _accumulatedMs -= moveInterval;

                _screen.Step();
                if (IsCarOffScreenBottom())
                {
                    // 每次完全离开屏幕后再切换到下一个字符，保证按顺序逐个显示
                    index++;
                    _car.Center = new IntPoint(4, -4); // 重置到屏幕上方，继续循环
                    _car.Offsets = BrickLibrary.getChar(index);
                }
            }
        }

        /// <summary>
        /// 判断当前赛车是否已经完全离开屏幕底部（用于决定何时重新生成）。
        /// </summary>
        private bool IsCarOffScreenBottom()
        {
            if (_car == null)
            {
                return false;
            }

            // 计算形状内部最小/最大 Y 偏移，用于推算实际占用的行范围
            int minOffsetY = int.MaxValue;
            int maxOffsetY = int.MinValue;
            foreach (var offset in _car.Offsets)
            {
                if (offset.Y < minOffsetY)
                {
                    minOffsetY = offset.Y;
                }
                if (offset.Y > maxOffsetY)
                {
                    maxOffsetY = offset.Y;
                }
            }

            int topY = _car.Center.Y + minOffsetY;
            int bottomY = _car.Center.Y + maxOffsetY;

            // 当整辆车的最上方一行都已经在屏幕底部之下时，视为完全离开屏幕
            return topY > PixelHeight - 1 && bottomY > PixelHeight - 1;
        }

        #region IPixelHost 实现
        public int PixelWidth
        {
            get { return 10; }
        }

        public int PixelHeight
        {
            get { return 20; }
        }

        public void ClearPixels()
        {
            foreach (var pixel in Pixels)
            {
                pixel.IsActive = false;
            }
        }

        public void SetPixel(int x, int y, bool isOn)
        {
            // 边界保护，防止形状坐标写出屏幕范围
            if (x < 0 || x >= PixelWidth || y < 0 || y >= PixelHeight)
            {
                return;
            }

            int index = y * PixelWidth + x;
            Pixels[index].IsActive = isOn;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
