using System.Collections.Generic;
using System.Linq;

namespace Betsson.EscapeMines.Model
{
    public class GameScene
    {
        private readonly Turtle _turtle;
        private readonly MineField _mineField;

        public GameScene(MineField mineField, Turtle turtle)
        {
            _mineField = mineField;
            _turtle = turtle;
        }

        public string Run(string[] moves)
        {
            if (!TurtleDirectionIsValid()) return "Invalid Direction";

            foreach (var move in moves)
            {
                if (!MovementIsValid(move)) return "Invalid Movement " + move;

                DoAction(move);

                if (!TurtlePositionIsValid()) return "Invalid Turtle Position";
                if (MineHit()) return "Mine Hit";
            }

            return CheckActionsResult();
        }

        private string CheckActionsResult()
        {
            if (Enumerable.SequenceEqual(_turtle.Position, _mineField.ExitPoint))
            {
                return "Success";
            }

            return "Still in Danger";
        }

        private bool MineHit()
        {
            foreach (var mineLocation in _mineField.MinesLocations)
            {
                if (Enumerable.SequenceEqual(_turtle.Position, mineLocation)) return true;
            }

            return false;
        }

        private void DoAction(string move)
        {
            if (string.Equals(move, "M"))
            {
                _turtle.Move();
                return;
            }

            _turtle.Rotate(move);
        }

        private bool MovementIsValid(string move)
        {
            List<string> validMoves = new List<string>() { "M", "R", "L" };

            return validMoves.Contains(move);
        }

        private bool TurtlePositionIsValid()
        {
            int row = _turtle.Position[0];
            int column = _turtle.Position[1];

            bool rowIsValid = _mineField.Size[0] - 1 >= row && row >= 0;
            bool columnIsValid = _mineField.Size[1] - 1 >= column && column >= 0;

            return rowIsValid && columnIsValid;
        }

        private bool TurtleDirectionIsValid()
        {
            List<string> validDirections = new List<string>() { "N", "S", "E", "W" };

            return validDirections.Contains(_turtle.Direction);
        }
    }
}
