using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_ImageProcessing.DrawingWindows.Drawing
{

    /// <summary>
    /// ====
    /// Specific on drawing on canvas, line picture polygon are all on its range;
    /// Responsibility on model and controler(view)
    /// ====
    /// </summary>
    public class DrawingControl
    {
        Canvas _canvas;

        //接受繪圖板
        public DrawingControl(Canvas canvas)
        {
            this._canvas = canvas;
        }

        //清除繪圖板
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }

        //畫線
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(line);
        }

        //畫矩形
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            CheckPoint(ref x1, ref x2);
            CheckPoint(ref y1, ref y2);
            Polygon rectangle = new Polygon();
            PointCollection pointCollection = new PointCollection();
            Point leftTopPoint = new Point(x1, y1);
            Point rightTopPoint = new Point(x2, y1);
            Point rightBottomPoint = new Point(x2, y2);
            Point leftBottomPoint = new Point(x1, y2);
            pointCollection.Add(leftTopPoint);
            pointCollection.Add(rightTopPoint);
            pointCollection.Add(rightBottomPoint);
            pointCollection.Add(leftBottomPoint);
            rectangle.Points = pointCollection;
            rectangle.Fill = new SolidColorBrush(Colors.Blue);
            rectangle.Stroke = new SolidColorBrush(Colors.Blue);
            _canvas.Children.Add(rectangle);
        }

        //畫三角形
        public void DrawTriangle(double x1, double y1, double x2, double y2)
        {
            /*CheckPoint(ref x1, ref x2);
            CheckPoint(ref y1, ref y2);*/
            double width = x2 - x1;
            double height = y2 - y1;
            double centerWidth = 0; // width / 2;
            double centerHeight = 0; // height / 2;
            Polygon triangle = new Polygon();
            PointCollection pointCollection = new PointCollection();
            //--一般三角形算法
            /*Point firstPoint = new Point(x1 - centerWidth, y2 - centerHeight);
            Point midPoint = new Point((x1 + x2) / 2 - centerWidth, y1 - centerHeight);
            Point lastPoint = new Point(x2 - centerWidth, y2 - centerHeight);*/
            //--作業需求算法
            Point firstPoint = new Point((float)x1 - centerWidth, (float)y1 - centerHeight);
            Point midPoint = new Point((float)(x1 + x2) / 2 - centerWidth, (float)y2 - centerHeight);
            Point lastPoint = new Point((float)x2 - centerWidth, (float)y1 - centerHeight);
            pointCollection.Add(firstPoint);
            pointCollection.Add(midPoint);
            pointCollection.Add(lastPoint);
            triangle.Points = pointCollection;
            triangle.Fill = new SolidColorBrush(Colors.Red);
            triangle.Stroke = new SolidColorBrush(Colors.Red);
            _canvas.Children.Add(triangle);
        }

        public void DrawCircle(double x1, double y1, double x2, double y2)
        {
            CheckPoint(ref x1, ref x2);
            CheckPoint(ref y1, ref y2);
            double width = x2 - x1;
            double height = y2 - y1;
            Ellipse circle = new Ellipse();
            circle.SetValue(Canvas.LeftProperty, x1);
            circle.SetValue(Canvas.TopProperty, y1);
            circle.Width = width;
            circle.Height = height;
            circle.Fill = new SolidColorBrush(Colors.Yellow);
            _canvas.Children.Add(circle);
        }

        //p1 > p2時交換兩點 動態繪製時使用
        private void CheckPoint(ref double p1, ref double p2)
        {
            double temp;
            if (p1 > p2)
            {
                temp = p1;
                p1 = p2;
                p2 = temp;
            }
        }

        //畫虛線模式
        public void DrawPointer(double x1, double y1, double x2, double y2)
        {
            CheckPoint(ref x1, ref x2);
            CheckPoint(ref y1, ref y2);
            float width = (float)(x2 - x1);
            float height = (float)(y2 - y1);
            Polygon rectangle = new Polygon();
            PointCollection pointCollection = new PointCollection();
            Point leftTopPoint = new Point(x1, y1);
            Point rightTopPoint = new Point(x2, y1);
            Point rightBottomPoint = new Point(x2, y2);
            Point leftBottomPoint = new Point(x1, y2);
            pointCollection.Add(leftTopPoint);
            pointCollection.Add(rightTopPoint);
            pointCollection.Add(rightBottomPoint);
            pointCollection.Add(leftBottomPoint);
            rectangle.Points = pointCollection;
            rectangle.Stroke = new SolidColorBrush(Colors.Blue);
            DoubleCollection dashCollision = new DoubleCollection();
            dashCollision.Add(2);
            dashCollision.Add(4);
            rectangle.StrokeDashArray = dashCollision;
            _canvas.Children.Add(rectangle);
        }
    }
}
