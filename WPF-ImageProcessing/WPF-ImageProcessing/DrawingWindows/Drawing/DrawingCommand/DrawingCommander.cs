﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImageProcessing.DrawingWindows.Drawing.DrawingCommand
{
    public class DrawingCommander
    {
        private Stack<IDrawingCommand> _undoCommands;
        private Stack<IDrawingCommand> _redoCommands;
        private DrawingModel _model;
        private bool _temporary;

        public DrawingCommander(DrawingModel model)
        {
            _model = model;
            _undoCommands = new Stack<IDrawingCommand>();
            _redoCommands = new Stack<IDrawingCommand>();
        }

        public void Execute(IDrawingCommand command)
        {
            if (command.IsTemporary)
                DoTemporary(command);
            else
                DoGeneral(command);
        }

        private void DoTemporary(IDrawingCommand command) //MoveAction
        {
            command.Execute();
        }

        private void DoGeneral(IDrawingCommand command) //UpAction
        {
            _redoCommands.Clear();
            _undoCommands.Push(command);
            command.Execute();
        }

        public void Undo()
        {
            IDrawingCommand command = _undoCommands.Pop();
            command.Undo();
            _redoCommands.Push(command);
        }

        public void Redo()
        {
            IDrawingCommand command = _redoCommands.Pop();
            command.Execute();
            _undoCommands.Push(command);
        }
    }
}
