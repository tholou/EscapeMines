using Betsson.EscapeMines.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Betsson.EscapeMines.UnitTest
{
    [TestClass]
    public class GameTests
    {
        private GameScene CreateGameScene(MineField mineField, Turtle turtle)
        {
            return new GameScene(mineField, turtle);
        }

        private MineField CreateMineField()
        {
            return new MineField()
            {
                Size = new int[] { 4, 5 },
                MinesLocations = new List<int[]> { new int[] { 1, 1 }, new int[] { 1, 3 }, new int[] { 3, 3 } },
                ExitPoint = new int[] { 2, 4 }
            };
        }

        private Turtle CreateTurtle()
        {
            return new Turtle()
            {
                Position = new int[] { 1, 0 },
                Direction = "N"
            };
        }

        [TestMethod]
        public void TestStartShouldReturnInvalidMoveIfAtLeastOneOfTheMovementsIsInvalid()
        {
            var game = CreateGameScene(CreateMineField(), CreateTurtle());
            const string wrongMove = "W";
            string moves = "L L M " + wrongMove;

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Invalid Movement " + wrongMove, result);
        }

        [TestMethod]
        public void TestStartShouldReturnInvalidTurtlePositionIfTurtlePositionIsInvalid()
        {
            var game = CreateGameScene(CreateMineField(), CreateTurtle());
            string moves = "M M";

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Invalid Turtle Position", result);
        }

        [TestMethod]
        public void TestStartShouldReturnInvalidDirectionIfDirectionIsInvalid()
        {
            var turtle = CreateTurtle();
            turtle.Direction = "J";
            var mineField = CreateMineField();
            var game = CreateGameScene(mineField, turtle);
            string moves = "M M M R M M";

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Invalid Direction", result);
        }

        [TestMethod]
        public void TestStartShouldReturnSuccessIfTheTurtleHitsTheExitPoint()
        {
            var game = CreateGameScene(CreateMineField(), CreateTurtle());
            string moves = "M R M M M M R M M";

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void TestStartShouldReturnStillInDangerIfTheTurtleDoesntHitBothTheExitPointNorTheMines()
        {
            var game = CreateGameScene(CreateMineField(), CreateTurtle());
            string moves = "M R M M M M R M M M";

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Still in Danger", result);
        }

        [TestMethod]
        public void TestStartShouldReturnMineHitIfAnyMovementHitsAMine()
        {
            var game = CreateGameScene(CreateMineField(), CreateTurtle());
            string moves = "M R M M M R M";

            var result = game.Run(moves.Split(" "));

            Assert.AreEqual("Mine Hit", result);
        }
    }
}