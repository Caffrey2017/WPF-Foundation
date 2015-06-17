using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public interface IDrawingCommand
    {
        void Execute();
        void Undo();
        bool IsTemporary { get; private set; }
    }
}
