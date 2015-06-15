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
using WPF_ImageProcessing.DrawingWindows.Drawing;

namespace WPF_ImageProcessing.DrawingWindows
{
    /// <summary>
    /// DrawingWindows.xaml 的互動邏輯
    /// </summary>
    public partial class DrawingWindow : Window
    {
        private bool testingPencilMode = false;
        private Point testingStartPoint;
        private bool testingFirstClick = false;

        private BitmapImage testingBitmap;
        private DrawingModel _drawingModel;

        public DrawingWindow()
        {
            InitializeComponent();
        }

        private void LoadWindows(object sender, RoutedEventArgs e)
        {
            //_drawingModel.Initialize();
            string location = AppDomain.CurrentDomain.BaseDirectory + "/images/test.jpg"; //open.FileName;
            testingBitmap = new BitmapImage(new Uri(location));
            _drawingModel = new DrawingModel(new DrawingControl(this._canvas));
            //testingBitmap = new WriteableBitmap(bitmap);
        }

        private void DrawByPencil(object sender, RoutedEventArgs e)
        {
            //_drawingModel.ChangeMode(DrawingTools.Pencil);
            //testingPencilMode = true;
            _drawingModel.ChangeMode(DrawingTools.Pencil);
        }

        private void ClickDownCanvas(object sender, MouseButtonEventArgs e)
        {
            //_drawingModel.SetPointDown(e.GetPosition(sender as IInputElement));
            Point pointDown =  e.GetPosition(sender as IInputElement);
            if (testingPencilMode)
            {
                testingStartPoint = e.GetPosition(sender as IInputElement);
                testingFirstClick = true;
            }
            _drawingModel.SetPointDown(pointDown.X, pointDown.Y);
            Console.WriteLine("Down at {0:F} {1:F}", pointDown.X, pointDown.Y);
        }

        private void ClickMoveCanvas(object sender, MouseEventArgs e)
        {
            //_drawingModel.SetPointMove(e.GetPosition(sender as IInputElement));
            Point pointMove = e.GetPosition(sender as IInputElement);
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
            _drawingModel.SetPointMove(pointMove.X, pointMove.Y);
        }

        private void ClickUpCanvas(object sender, MouseButtonEventArgs e)
        {
            //_drawingModel.SetPointUp(e.GetPosition(sender as IInputElement));
            testingFirstClick = false;
            Point pointUp = e.GetPosition(sender as IInputElement);
            _drawingModel.SetPointUp(pointUp.X, pointUp.Y);
            //Console.WriteLine("Up at {0:F} {1:F}", pointUp.X, pointUp.Y);
        }

        private void TestingLoadPicture(object sender, RoutedEventArgs e)
        {
            //_drawingModel.TestingPicture(wBitmap);
            //_canvas.Childern.Clear();
            //_canvas.Children.Add(image);

            WriteableBitmap wBitmap = new WriteableBitmap(testingBitmap);
            ModifiablePicture mPic = new ModifiablePicture(wBitmap);
            //mPic.SetBytes(mPic.BitmapBytes, 250, 250, 250, 250);
            Image image = new Image();


            var transform = image.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                image.RenderTransform = transform;
            }
            transform.X = 100;
            transform.Y = 100;

            image.Source = mPic.SourceBitmap;
            _canvas.Children.Add(image);
        }
    }
}
