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
        private string _currentShape = "Square"; // Форма по умолчанию
        private bool isDrawing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            DrawShape(e.GetPosition(DrawingCanvas));
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                DrawShape(e.GetPosition(DrawingCanvas));
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }

        private void DrawShape(Point position)
        {
            int x = (int)(position.X / _pixelSize) * _pixelSize;
            int y = (int)(position.Y / _pixelSize) * _pixelSize;

            Shape shape;

            switch (_currentShape)
            {
                case "Circle":
                    shape = new Ellipse { Width = _pixelSize, Height = _pixelSize, Fill = _currentBrush };
                    break;
                case "Rectangle":
                    shape = new Rectangle { Width = _pixelSize * 2, Height = _pixelSize, Fill = _currentBrush };
                    break;
                case "Eraser":
                    RemoveShapeAt(x, y);
                    return;
                default:
                    shape = new Rectangle { Width = _pixelSize, Height = _pixelSize, Fill = _currentBrush };
                    break;
            }

            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            DrawingCanvas.Children.Add(shape);
        }

        private void RemoveShapeAt(int x, int y)
        {
            foreach (UIElement child in DrawingCanvas.Children)
            {
                if (child is Shape shape &&
                    Canvas.GetLeft(shape) == x && Canvas.GetTop(shape) == y)
                {
                    DrawingCanvas.Children.Remove(child);
                    break;
                }
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
                _ => Brushes.Black
            };
        }

        private void ShapePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentShape = (ShapePicker.SelectedItem as ComboBoxItem)?.Content.ToString();
        }
    }
}
