using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingState
{
    public class DrawingStateController
    {
        BaseDrawingState _state;
        DrawingTools _mode;
        DrawingModel _model;
        DrawingCommander _commander;

        public DrawingStateController(DrawingModel model,DrawingCommander commander)
        {
            _model = model;
            _commander = commander;
            _state = CreateState(DrawingTools.None, model, commander);
        }

        public void ChangeMode(DrawingTools mode)
        {
            _mode = mode;
            _state = CreateState(_mode, _model, _commander);
        }

        public void PointDown(Point pt)
        {
            _state.PointDown(pt);
        }

        public void PointMove(Point pt)
        {
            _state.PointMove(pt);
        }

        public void PointUp(Point pt)
        {
            _state.PointUp(pt);
        }

        private BaseDrawingState CreateState(DrawingTools mode,DrawingModel model,DrawingCommander commander)
        {
            if (mode == DrawingTools.None)
                return new NoState(model, commander);
            else if (mode == DrawingTools.Pencil)
                return new PencilState(model, commander);
            else if (mode == DrawingTools.PointAndMove)
                return new PencilState(model, commander);
            throw new KeyNotFoundException("No Such Mode");
        }

    }
}
