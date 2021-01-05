using System;

namespace Betsson.EscapeMines.Model
{
    public class Turtle
    {
        public int[] Position { get; set; }

        public string Direction { get; set; }

        public void Rotate(string side)
        {
            var currentDirection = (Enums.Direction)Enum.Parse(typeof(Enums.Direction), Direction);
            int nextDirection = (int)currentDirection;

            Enums.MoveType enumSide = (Enums.MoveType)Enum.Parse(typeof(Enums.MoveType), side);

            switch (enumSide)
            {
                case Enums.MoveType.R:
                    nextDirection++;
                    break;
                case Enums.MoveType.L:
                    nextDirection--;
                    break;
            }

            if (nextDirection > 3) nextDirection = 0;
            if (nextDirection < 0) nextDirection = 3;

            Direction = ((Enums.Direction)nextDirection).ToString();
        }

        public void Move()
        {
            Enums.Direction currentDirection = (Enums.Direction)Enum.Parse(typeof(Enums.Direction), Direction);
            int row = Position[0];
            int column = Position[1];

            switch (currentDirection)
            {
                case Enums.Direction.N:
                    Position = new int[] { row - 1, column };
                    break;
                case Enums.Direction.E:
                    Position = new int[] { row, column + 1 };
                    break;
                case Enums.Direction.S:
                    Position = new int[] { row + 1, column };
                    break;
                case Enums.Direction.W:
                    Position = new int[] { row, column - 1 };
                    break;
            }
        }
    }
}
