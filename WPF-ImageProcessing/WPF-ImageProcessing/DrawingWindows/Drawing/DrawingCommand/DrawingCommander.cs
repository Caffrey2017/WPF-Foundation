using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class DrawingCommander
    {
        private Stack<BaseDrawingCommand> _undoCommands;
        private Stack<BaseDrawingCommand> _redoCommands;
        private DrawingModel _model;
        private bool _temporary;

        public DrawingCommander(DrawingModel model)
        {
            _model = model;
            _undoCommands = new Stack<BaseDrawingCommand>();
            _redoCommands = new Stack<BaseDrawingCommand>();
        }

        public void Execute(BaseDrawingCommand command)
        {
            if (command.IsTemporary)
                DoTemporary(command);
            else
                DoGeneral(command);
        }

        private void DoTemporary(BaseDrawingCommand command) //MoveAction
        {
            command.Execute();
        }

        private void DoGeneral(BaseDrawingCommand command) //UpAction
        {
            _redoCommands.Clear();
            _undoCommands.Push(command);
            command.Execute();
        }

        public void Undo()
        {
            BaseDrawingCommand command = _undoCommands.Pop();
            command.Undo();
            _redoCommands.Push(command);
        }

        public void Redo()
        {
            BaseDrawingCommand command = _redoCommands.Pop();
            command.Execute();
            _undoCommands.Push(command);
        }
    }
}
