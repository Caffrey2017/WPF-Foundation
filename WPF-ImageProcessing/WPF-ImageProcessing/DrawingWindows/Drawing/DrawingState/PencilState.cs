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
        public PencilState(DrawingCommander drawingCommander)
            : base(drawingCommander)
        {
        }

        public override void PointMove(Point pt)
        {
            base.PointMove(pt);
            IDrawingCommand command = new PencilCommand();
            command.SetTemporary(true);
            _commander.Execute(command);
        }

        public override void PointUp(Point pt)
        {
            base.PointUp(pt);
        }
    }
}
