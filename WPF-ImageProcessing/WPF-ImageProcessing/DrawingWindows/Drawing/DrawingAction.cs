using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace WPF_ImageProcessing.DrawingWindows.Drawing
{
    /// <summary>
    /// A object to store information of drawing information.
    /// </summary>
    public class DrawingAction
    {
        public Point LeftTop
        {
            get;
            private set;
        }

        public Point RightBottom
        {
            get;
            private set;
        }

        public WriteableBitmap Bitmap
        {
            get;
            private set;
        }

        public DrawingTools Mode
        {
            get;
            private set;
        }

        public DrawingAction(DrawingTools mode, Point leftTop, Point rightBottom, WriteableBitmap bitmap = null)
        {
            LeftTop = leftTop;
            RightBottom = rightBottom;
            Bitmap = bitmap;
            Mode = mode;
        }
    }
}
