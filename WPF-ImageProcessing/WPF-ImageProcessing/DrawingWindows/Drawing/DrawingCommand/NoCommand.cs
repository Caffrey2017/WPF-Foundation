using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class NoCommand : IDrawingCommand
    {
        public void Execute() { }
        public void Undo() { }
    }
}
