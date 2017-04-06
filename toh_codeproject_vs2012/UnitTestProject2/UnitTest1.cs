using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using toh;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTestProject1
    {
    [TestClass]
    public class UnitTest1
    {
        int numDisks = 3;
        Pole pole0 = new Pole(0);
        Pole pole1 = new Pole(1);
        Pole pole2 = new Pole(2);

        List<Move> OneMoves = MoveCalculator.GetMoves(1);
        List<Move> TwoMoves = MoveCalculator.GetMoves(2);
        List<Move> ThreeMoves = MoveCalculator.GetMoves(3);

        [TestMethod]
        [TestCategory("Moving")]
        public void TestMoves3()
        {
            //Assert that each move in the solution exists
            Assert.AreEqual(new Move(pole0, pole2), ThreeMoves[0]);
            Assert.AreEqual(new Move(pole0, pole1), ThreeMoves[1]);
            Assert.AreEqual(new Move(pole2, pole1), ThreeMoves[2]);
            Assert.AreEqual(new Move(pole0, pole2), ThreeMoves[3]);
            Assert.AreEqual(new Move(pole1, pole0), ThreeMoves[4]);
            Assert.AreEqual(new Move(pole1, pole2), ThreeMoves[5]);
            Assert.AreEqual(new Move(pole0, pole2), ThreeMoves[6]);
        }

        [TestMethod]
        [TestCategory("Moving")]
        public void TestMoves1()
        {
            //Assert that each move is different
            Assert.AreEqual(new Move(pole0, pole2), OneMoves[0]);
        }


        [TestMethod]
        [TestCategory("Moving")]
        public void testMoves2()
        {
            //Bug, Can't be solved, array is only of size 3
            //Program requires 5 moves
            Assert.AreEqual(new Move(pole0, pole2), TwoMoves[0]);
            Assert.AreEqual(new Move(pole0, pole1), TwoMoves[1]);
            Assert.AreEqual(new Move(pole2, pole0), TwoMoves[2]);
        }

        //Test Moves for 3 disk game
        [TestMethod]
        [TestCategory("Move Count")]
        public void testMoveCount()
        {
            GameState.RestartGame();
            solveGame();
            Assert.AreEqual(7, GameState.MoveCount);
        }

        //Test Moves for 4 disk game
        [TestMethod]
        [TestCategory("Move Count")]
        public void fourDiskMoveCount()
        {
            GameState.RestartGame();
            solveGame(4);
            Assert.AreEqual(14, GameState.MoveCount);
        }

        //Test Moves for 5 disk game
        [TestMethod]
        [TestCategory("MoveCount")]
        public void fiveMoves()
        {
            GameState.RestartGame();
            solveGame(5);
            Assert.AreEqual(28, GameState.MoveCount);
        }

        //Test moves for 1 disk game
        [TestMethod]
        [TestCategory("MoveCount")]
        public void oneDiskMoves()
        {
            GameState.RestartGame();
            solveGame(1);
            Assert.AreEqual(1, GameState.MoveCount);
        }

        //Test moves for 0 disk game
        [TestMethod]
        [TestCategory("MoveCount")]
        public void zeroDiskMoves()
        {
            GameState.RestartGame();
            solveGame(0);
            Assert.AreEqual(0, GameState.MoveCount);
        }

        //Tests
        [TestMethod]
        [TestCategory("DiskPositions")]
        public void moreDisks()
        {
            GameState.RestartGame(5);
            int diskTotal = GameState.NumberOfDisks;
            Assert.AreEqual(5, diskTotal);
        }

        //Test that restart game resets DiskPositions
        [TestMethod]
        [TestCategory("DiskPositions"), TestCategory("Interface")]
        public void startPositions()
        {
            GameState.RestartGame(3);
            Assert.AreEqual(3, GameState.Poles[0].Disks.Count);
            Assert.AreEqual(0, GameState.Poles[1].Disks.Count);
            Assert.AreEqual(0, GameState.Poles[2].Disks.Count);
        }

        [TestMethod]
        [TestCategory("DiskPositions"), TestCategory("Interface")]
        public void startPositionsMultipleDisks()
        {
            GameState.RestartGame(5);
            Assert.AreEqual(5, GameState.Poles[0].Disks.Count);
            Assert.AreEqual(0, GameState.Poles[1].Disks.Count);
            Assert.AreEqual(0, GameState.Poles[2].Disks.Count);
        }

        /// <summary>
        ///A test for IsSolved
        ///</summary>
        [TestMethod()]
        [TestCategory("Solution")]
        public void testSolved()
        {
            //Restart the game then check if it's solved
            GameState.RestartGame(3);

            bool solve = GameState.IsSolved();
            Assert.AreEqual(false, solve);

            //Solve the game and check if it's solved
            solveGame();

            solve = GameState.IsSolved();
            Assert.AreEqual(true, solve);

            //Reset the game with 5 disks and redo above checks
            GameState.RestartGame(5);
            solve = GameState.IsSolved();
            Assert.AreEqual(false, solve);

            solveGame(5);
            solve = GameState.IsSolved();
            Assert.AreEqual(true, solve);
        }

        //Tests a valid move
        [TestMethod]
        [TestCategory("Moving"), TestCategory("Interface")]
        public void validMove()
        {
            GameState.RestartGame(3);
            GameState.MakeMove(new Move(0, 2));
            Assert.AreEqual(1, GameState.Poles[2].Disks.Count);
        }

        [TestMethod]
        [TestCategory("Moving")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Index was out of range. Must be non-negative and less than the size of the collection.")]
        public void inValidMove()
        {
            GameState.RestartGame(3);
            GameState.MakeMove(new Move(0, 3));
            Assert.AreEqual(1, GameState.Poles[3].Disks.Count);
        }

        [TestMethod]
        [TestCategory("Moving")]
        public void moveBiggerOnSmaller()
        {
            GameState.RestartGame(3);

            //Move Bigger piece on smaller
            GameState.MakeMove(new Move(0, 2));
            GameState.MakeMove(new Move(0, 2));

            //Check that pole 0 has 2 disks and that pole 2 only has the smaller
            Assert.AreEqual(2, GameState.Poles[0].Disks.Count);
            Assert.AreEqual(1, GameState.Poles[2].Disks.Count);
        }

        [TestMethod]
        [TestCategory("Moving"), TestCategory("Interface")]
        public void moveFromEmptyPole()
        {
            GameState.RestartGame(3);

            //pole1 has no Disks on it, we move a "disk" from it
            //to pole2 and pole 2 has 0 "disks" on it
            Assert.AreEqual(0, GameState.Poles[1].Disks.Count);
            GameState.MakeMove(new Move(pole1, pole2));
            Assert.AreEqual(0, GameState.Poles[2].Disks.Count);
        }

        [TestMethod]
        [TestCategory("Interface")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Index was out of range. Must be non-negative and less than the size of the collection.")]
        public void tooManyDisks()
        {
            GameState.RestartGame(7);
            Assert.AreEqual(7, GameState.Poles[0].Disks.Count);
        }

        //Overloaded helper methods to solve the game and calculate the moves
        private void solveGame()
        {
            List<Move> moves = MoveCalculator.GetMoves(numDisks);
            foreach (Move move in moves)
            {
                GameState.MakeMove(move);
            }
        }

        private void solveGame(int disks)
        {
            int diskNums = disks;
            List<Move> moves = MoveCalculator.GetMoves(diskNums);
            foreach (Move move in moves)
            {
                GameState.MakeMove(move);
            }
        }
    }
}

