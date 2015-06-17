using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingState
{
    public class PencilState : BaseDrawingState
    {
        public PencilState(DrawingModel model,DrawingCommander drawingCommander)
            : base(model,drawingCommander)
        {
            _mode = DrawingTools.Pencil;
        }

        protected override void PointMoveHook()
        {
            DrawingAction action = _model.MakeAction(_mode, _firstPoint, _movePoint);
            BaseDrawingCommand command = new PencilCommand(_model, action);
            command.SetTemporary(true);
            _commander.Execute(command);
        }

        protected override void PointUpHook()
        {
            DrawingAction action = _model.MakeAction(_mode, _firstPoint, _lastPoint);
            BaseDrawingCommand command = new PencilCommand(_model, action);
            _commander.Execute(command);
        }
    }
}
