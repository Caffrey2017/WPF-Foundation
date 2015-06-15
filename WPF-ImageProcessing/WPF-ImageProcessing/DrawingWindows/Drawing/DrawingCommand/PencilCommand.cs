using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class PencilCommand : IDrawingCommand
    {
        DrawingModel _model;
        DrawingAction _action;

        public PencilCommand(DrawingModel model,DrawingAction action)
        {
            _model = model;
            _action = action;
        }

        public void Execute()
        {
            _model.AddAnAction(_action);
            _model.Refresh();
        }

        public void Undo()
        {
            _model.TakeOfAction(_action);
            _model.Refresh();
        }
    }
}
