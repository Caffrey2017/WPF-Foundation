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
        bool _temporary = false;
        public bool IsTemporary { get { return _temporary; } private set; }

        public PencilCommand(DrawingModel model,DrawingAction action)
        {
            _model = model;
            _action = action;
        }

        public void Execute()
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


        public void Undo()
        {
            _model.TakeOfAction(_action);
            _model.Refresh();
        }
        
        public void SetTemporary(bool enable)
        {
            _temporary = enable;
        }
    }
}
