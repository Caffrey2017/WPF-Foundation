using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingState
{
    public class NoState : BaseDrawingState
    {
        public NoState(DrawingModel model, DrawingCommander drawingCommander)
            : base(model, drawingCommander)
        {
            _mode = DrawingTools.None;
        }
    }
}
