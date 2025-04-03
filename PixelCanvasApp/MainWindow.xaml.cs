using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PixelCanvasApp
{
    public partial class MainWindow : Window
    {
        private readonly int _pixelSize = 10;
        private SolidColorBrush _currentBrush = Brushes.Black; // Текущий цвет
        private bool isDrawing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            DrawPixel(e.GetPosition(DrawingCanvas));
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                DrawPixel(e.GetPosition(DrawingCanvas));
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }

        private void DrawPixel(Point position)
        {
            int x = (int)(position.X / _pixelSize) * _pixelSize;
            int y = (int)(position.Y / _pixelSize) * _pixelSize;

            Rectangle pixel = new Rectangle
            {
                Width = _pixelSize,
                Height = _pixelSize,
                Fill = _currentBrush
            };

            Canvas.SetLeft(pixel, x);
            Canvas.SetTop(pixel, y);

            // Если включен "ластик", удаляем пиксели на этой позиции
            if (_currentBrush == Brushes.White)
            {
                foreach (UIElement child in DrawingCanvas.Children)
                {
                    if (child is Rectangle rect &&
                        Canvas.GetLeft(rect) == x && Canvas.GetTop(rect) == y)
                    {
                        DrawingCanvas.Children.Remove(child);
                        break;
                    }
                }
            }
            else
            {
                DrawingCanvas.Children.Add(pixel);
            }
        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (ColorPicker.SelectedItem as ComboBoxItem)?.Content.ToString();

            _currentBrush = selectedColor switch
            {
                "Red" => Brushes.Red,
                "Blue" => Brushes.Blue,
                "Green" => Brushes.Green,
                "Yellow" => Brushes.Yellow,
                "Eraser" => Brushes.White, // Ластик — белый цвет
                _ => Brushes.Black
            };
        }
    }
}
