using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class PencilCommand : BaseDrawingCommand
    {
        DrawingModel _model;
        DrawingAction _action;

        public PencilCommand(DrawingModel model,DrawingAction action)
        {
            _model = model;
            _action = action;
        }

        public override void Execute()
        {
            if (IsTemporary)
                DoTemporary();
            else
                DoGeneral();
        }

        private void DoTemporary()
        {
            _model.Refresh();
            _model.Draw(_action);
        }

        private void DoGeneral()
        {
            _model.AddAnAction(_action);
            _model.Refresh();
        }


        public override void Undo()
        {
            _model.TakeOfAction(_action);
            _model.Refresh();
        }
    }
}
