using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public abstract class BaseDrawingCommand
    {
        private bool _temporary;
        public abstract void Execute();
        public abstract void Undo();
        public bool IsTemporary { get { return _temporary; } }
        public void SetTemporary(bool enable) { _temporary = enable; }
    }
}
