using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PixelCanvasApp
{
    public partial class MainWindow : Window
    {
        private Canvas _canvas;
        private readonly int _pixelSize = 10;
        private readonly SolidColorBrush _brush = Brushes.Black;

        public MainWindow()
        {
            InitializeComponent();
            SetupCanvas();
        }

        private void SetupCanvas()
        {
            _canvas = new Canvas
            {
                Background = Brushes.White
            };
            Content = _canvas;
            _canvas.MouseDown += Canvas_MouseDown;
            _canvas.MouseMove += Canvas_MouseMove;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawPixel(e.GetPosition(_canvas));
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DrawPixel(e.GetPosition(_canvas));
            }
        }

        private void DrawPixel(Point position)
        {
            int x = (int)(position.X / _pixelSize) * _pixelSize;
            int y = (int)(position.Y / _pixelSize) * _pixelSize;

            Rectangle pixel = new Rectangle
            {
                Width = _pixelSize,
                Height = _pixelSize,
                Fill = _brush
            };

            Canvas.SetLeft(pixel, x);
            Canvas.SetTop(pixel, y);
            _canvas.Children.Add(pixel);
        }
    }
}
