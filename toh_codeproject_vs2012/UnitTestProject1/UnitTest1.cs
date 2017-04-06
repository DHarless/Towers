using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using toh;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int amountOfDisks = 3;
            Pole pole0 = new Pole(0);
            Pole pole1 = new Pole(1);
            Pole pole2 = new Pole(2);

            //Set expected moves
            List<Move> expectedMoves = new List<Move>();
            Move move1 = new Move(pole0, pole2);
            Move move2 = new Move(pole0, pole1);
            Move move3 = new Move(pole2, pole1);
            Move move4 = new Move(pole0, pole2);
            Move move5 = new Move(pole1, pole0);
            Move move6 = new Move(pole1, pole2);
            Move move7 = new Move(pole0, pole2);
            expectedMoves.Add(move1);
            expectedMoves.Add(move2);
            expectedMoves.Add(move3);
            expectedMoves.Add(move4);
            expectedMoves.Add(move5);
            expectedMoves.Add(move6);
            expectedMoves.Add(move7);

            //Get actual moves
            List<Move> actualMoves = MoveCalculator.GetMoves(amountOfDisks);

            //Assert
            Assert.AreEqual(expectedMoves.Count, actualMoves.Count);
            Assert.AreEqual(expectedMoves[0], actualMoves[0]);
            Assert.AreEqual(expectedMoves[1], actualMoves[1]);
            Assert.AreEqual(expectedMoves[2], actualMoves[2]);
            Assert.AreEqual(expectedMoves[3], actualMoves[3]);
            Assert.AreEqual(expectedMoves[4], actualMoves[4]);
            Assert.AreEqual(expectedMoves[5], actualMoves[5]);
            Assert.AreEqual(expectedMoves[6], actualMoves[6]);
        }
    }
}
