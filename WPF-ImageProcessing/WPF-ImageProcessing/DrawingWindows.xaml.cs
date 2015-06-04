using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_ImageProcessing
{
    /// <summary>
    /// DrawingWindows.xaml 的互動邏輯
    /// </summary>
    public partial class DrawingWindows : Window
    {
        private bool testingPencilMode = false;
        private Point testingStartPoint;
        private bool testingFirstClick = false;

        private BitmapImage testingBitmap;

        public DrawingWindows()
        {
            InitializeComponent();
        }

        private void LoadWindows(object sender, RoutedEventArgs e)
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + "/images/test.jpg"; //open.FileName;
            BitmapImage testingBitmap = new BitmapImage(new Uri(location));
            //testingBitmap = new WriteableBitmap(bitmap);
        }

        private void DrawByPencil(object sender, RoutedEventArgs e)
        {
            testingPencilMode = true;
        }

        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            if (testingPencilMode)
            {
                testingStartPoint = e.GetPosition(sender as IInputElement);
                testingFirstClick = true;
            }
        }

        private void ClickMoveCanvas(object sender, MouseEventArgs e)
        {
            if(testingFirstClick)
            {
                Point testingNowPoint = e.GetPosition(sender as IInputElement);
                Line line = new Line();
                line.Stroke = SystemColors.WindowFrameBrush;
                line.X1 = testingStartPoint.X;
                line.Y1 = testingStartPoint.Y;
                line.X2 = testingNowPoint.X;
                line.Y2 = testingNowPoint.Y;
                testingStartPoint = testingNowPoint;
                _canvas.Children.Add(line);
            }
        }

        private void ClickUpCanvas(object sender, MouseButtonEventArgs e)
        {
            testingFirstClick = false;
        }

        private void TestingLoadPicture(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Source = testingBitmap;
            _canvas.Children.Add(image);
        }

    }
}
