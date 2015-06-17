using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class NoCommand : BaseDrawingCommand
    {
        public override void Execute() { }
        public override void Undo() { }
    }

}
