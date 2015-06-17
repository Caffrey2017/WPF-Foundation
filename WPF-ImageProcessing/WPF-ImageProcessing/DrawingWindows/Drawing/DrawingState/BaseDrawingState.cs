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
        protected DrawingCommander _commander;
        protected DrawingModel _model;
        protected DrawingTools _mode;
        protected bool _clicked = false;

        public BaseDrawingState(DrawingModel model,DrawingCommander commander)
        {
            _model = model;
            _commander = commander;
        }

        public void PointDown(Point pt) 
        {
            _clicked = true;  
            _firstPoint = pt;
            PointDownHook();
        }

        protected virtual void PointDownHook() { }

        public void PointMove(Point pt) 
        {
            if (!_clicked)
                return;
            _movePoint = pt;
            PointMoveHook();
        }
        protected virtual void PointMoveHook() { }

        public void PointUp(Point pt) 
        { 
            _clicked = false; 
            _lastPoint = pt;
            PointUpHook();
        }
        protected virtual void PointUpHook() { }

    }
}
