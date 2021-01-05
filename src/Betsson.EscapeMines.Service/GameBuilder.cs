using Betsson.EscapeMines.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Betsson.EscapeMines.Service
{
    public class GameBuilder : IGameBuilder
    {
        private readonly Stream _stream;
        private string _fileName;

        private MineField _mineField;
        private Turtle _turtle;

        public GameBuilder(Stream stream, string FileName)
        {
            _stream = stream;
            _fileName = FileName;
            _mineField = new MineField();
            _turtle = new Turtle();
        }

        public void LoadData()
        {
            var fileName = _fileName;

            try
            {
                int lineNumber = 1;
                string lineContent;

                StreamReader reader = new StreamReader(_stream);

                while ((lineContent = reader.ReadLine()) != null)
                {
                    ProcessLine(lineNumber, lineContent);
                    lineNumber++;
                }

                reader.Close();
            }
            catch (FileLoadException e)
            {
                throw new ApplicationException($"Could not load game setting file '{fileName}'.", e);
            }
            catch (PathTooLongException e)
            {
                throw new ApplicationException($"Game setting file name '{fileName}' is too long.", e);
            }
            catch (DirectoryNotFoundException e)
            {
                throw new ApplicationException($"Invalid game setting file path '{fileName}'.", e);
            }
            catch (FileNotFoundException e)
            {
                throw new ApplicationException($"Could not find the specified game setting file '{fileName}'", e);
            }
            catch (IOException e)
            {
                throw new ApplicationException($"Could not load the game setting file '{fileName}'.", e);
            }
        }

        private void ProcessLine(int lineNumber, string lineContent)
        {
            var lineArray = lineContent.Split(' ');

            switch (lineNumber)
            {
                case 1:
                    SetBoardSize(lineNumber, lineContent);
                    break;
                case 2:
                    SetMinesLocations(lineNumber, lineContent);
                    break;
                case 3:
                    SetExitPoint(lineNumber, lineContent);
                    break;
                case 4:
                    SetTurtleStartPosition(lineNumber, lineContent);
                    break;
                default:
                    StartNewGame(lineNumber, lineContent);
                    break;
            }
        }

        private void StartNewGame(int lineNumber, string lineContent)
        {
            string[] movesData;
            movesData = lineContent.Split(' ');
            var turtle = new Turtle()
            {
                Direction = _turtle.Direction,
                Position = _turtle.Position
            };

            var game = new GameScene(_mineField, turtle);
            string gameResult = game.Run(movesData);

            Console.WriteLine(gameResult);
        }

        private void SetTurtleStartPosition(int lineNumber, string lineContent)
        {
            string[] turtleStartPositions;
            string errorMessage = $"Turtle start position data is invalid (line {lineNumber}): {lineContent}";

            if (String.IsNullOrEmpty(lineContent))
            {
                throw new ApplicationException($"Turtle start position data is empty (line {lineNumber})");
            }

            turtleStartPositions = lineContent.Split(' ');

            if (turtleStartPositions.Length < 3)
            {
                throw new ApplicationException(errorMessage);
            }

            if (int.TryParse(turtleStartPositions[0], out int column) && int.TryParse(turtleStartPositions[1], out int row))
            {
                _turtle.Position = new int[] { row, column };
                _turtle.Direction = turtleStartPositions[2];
            }
            else
            {
                throw new ApplicationException(errorMessage);
            }
        }

        private void SetExitPoint(int lineNumber, string lineContent)
        {
            string[] exitPoints;
            string errorMessage = $"Exit point data is invalid (line {lineNumber}): {lineContent}";

            if (String.IsNullOrEmpty(lineContent))
            {
                throw new ApplicationException($"Exit point data is empty (line {lineNumber})");
            }

            exitPoints = lineContent.Split(' ');

            if (exitPoints.Length < 2)
            {
                throw new ApplicationException(errorMessage);
            }

            if (int.TryParse(exitPoints[0], out int column) && int.TryParse(exitPoints[1], out int row))
            {
                _mineField.ExitPoint = new int[] { row, column };
            }
            else
            {
                throw new ApplicationException(errorMessage);
            }
        }

        private void SetMinesLocations(int lineNumber, string lineContent)
        {
            var minesLocations = new List<int[]>();
            string[] minesData;
            string errorMessage = $"Mines data is invalid (line {lineNumber}): {lineContent}";
                       
            if (String.IsNullOrEmpty(lineContent))
            {
                throw new ApplicationException($"Mines data is empty (line {lineNumber})");
            }

            minesData = lineContent.Split(' ');

            foreach (var mineData in minesData)
            {
                var minePosition = mineData.Split(",");
                if (minePosition.Length < 2)
                {
                    throw new ApplicationException(errorMessage);
                }
                if (int.TryParse(minePosition[0], out int row) && int.TryParse(minePosition[1], out int column))
                {
                    minesLocations.Add(new int[] { row, column });
                }
                else
                {
                    throw new ApplicationException(errorMessage);
                }
            }

            _mineField.MinesLocations = minesLocations;
        }

        private void SetBoardSize(int lineNumber, string lineContent)
        {
            string[] items;
            string errorMessage = $"Board size data is invalid (line {lineNumber}): {lineContent}";

            if (String.IsNullOrEmpty(lineContent))
            {
                throw new ApplicationException($"Board size data is empty (line {lineNumber})");
            }                

            items = lineContent.Split(' ');

            if (items.Length < 2)
            {
                throw new ApplicationException(errorMessage);
            }
                

            if (int.TryParse(items[0], out int column) && int.TryParse(items[1], out int row))
            {
                _mineField.Size = new int[] { row, column };
            }
            else
            {
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
