using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingState
{
    public abstract class BaseDrawingState
    {
        protected Point _firstPoint;
        protected Point _movePoint;
        protected Point _lastPoint;
        protected DrawingCommander _commander; //

        public BaseDrawingState(DrawingCommander commander)
        {
            _commander = commander;
        }
        public virtual void PointDown(Point pt) { _firstPoint = pt; }
        public virtual void PointMove(Point pt) { _movePoint = pt; }
        public virtual void PointUp(Point pt) { _lastPoint = pt; }
    }
}
