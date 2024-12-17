
using System;
using System.ComponentModel.Design;
using System.Numerics;
using TicTacToe;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicTacToeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        const int x = -1;
        const int o = 1;

        int[] row1 = { 0, 1, 2 };
        int[] row2 = { 3, 4, 5 };
        int[] row3 = { 6, 7, 8 };

        int[] col1 = { 0, 3, 6 };
        int[] col2 = { 1, 4, 7 };
        int[] col3 = { 2, 5, 8 };

        int[] diag1 = { 0, 4, 8 };
        int[] diag2 = { 2, 4, 6 };

        enum Status
        {
            Won,
            Lost,
            NeutralMove
        }

        private Status evaluateMove(List<int> board)
        {
            if ((board[0] + board[1] + board[2] < -1.5)
                 || (board[3] + board[4] + board[5] < -1.5)
                 || (board[6] + board[7] + board[8] < -1.5)
                 || (board[0] + board[3] + board[6] < -1.5)
                 || (board[1] + board[4] + board[7] < -1.5)
                 || (board[2] + board[5] + board[8] < -1.5)
                 || (board[0] + board[4] + board[8] < -1.5)
                 || (board[2] + board[4] + board[6] < -1.5))
            {
                return Status.Lost;
            }
            if ((board[0] + board[1] + board[2] > 2.5)
                 || (board[3] + board[4] + board[5] > 2.5)
                 || (board[6] + board[7] + board[8] > 2.5)
                 || (board[0] + board[3] + board[6] > 2.5)
                 || (board[1] + board[4] + board[7] > 2.5)
                 || (board[2] + board[5] + board[8] > 2.5)
                 || (board[0] + board[4] + board[8] > 2.5)
                 || (board[2] + board[4] + board[6] > 2.5))
            {
                return Status.Won;
            }
            return Status.NeutralMove;
        }

        double FindError(List<int> board, double orig, int move)
        {
            int closest = 100;
            for (int i = 0; i < board.Count; i++)
            {
                if ((board[i] == 0)
                    && (i != move))
                {
                    if (Math.Abs(i - move) < Math.Abs(closest - move))
                    {
                        closest = i;
                    }
                }
            }

            double wanted = (((double)closest) / 8);
            double error = orig * (1 - orig) * (wanted - orig);
            return error;
        }

        public ulong LearnLegalMoves(Net network)
        {
            Random rng = new Random();
            ulong invalidMoves = 0;
            List<double> errors = new List<double>();

            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0, 0, 0,
                                            0, 0, 0,
                                            0, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    int xMove = rng.Next(9);
                    while (board[xMove] != 0)
                    {
                        xMove = rng.Next(9);
                    }
                    board[xMove] = x;

                    double orig = network.Play(board);
                    int oMove = Convert.ToInt32(orig * 8);
                    while (board[oMove] != 0)
                    {
                        double error = FindError(board, orig, oMove);
                        errors.Clear();
                        errors.Add(error);
                        network.BackPropagate(errors);
                        orig = network.Play(board);
                        oMove = Convert.ToInt32(orig * 8);
                        invalidMoves++;
                    }
                    board[oMove] = o;
                }
            }
            return invalidMoves;
        }

        public int Play(Net network)
        {
            int lostGames = 0;
            int wonGames = 0;
            int tieGames = 0;
            List<double> errors = new List<double>();

            Random rng = new Random();

            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0, 0, 0,
                                            0, 0, 0,
                                            0, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    int xMove = rng.Next(9);
                    while (board[xMove] != 0)
                    {
                        xMove = rng.Next(9);
                    }
                    board[xMove] = x;

                    double orig = network.Play(board);
                    int oMove = Convert.ToInt32(orig * 8);
                    while (board[oMove] != 0)
                    {
                        double error = FindError(board, orig, oMove);
                        errors.Clear();
                        errors.Add(error);
                        network.BackPropagate(errors);
                        orig = network.Play(board);
                        oMove = Convert.ToInt32(orig * 8);
                    }
                    board[oMove] = o;

                    Status status = evaluateMove(board);

                    if (status == Status.Lost)
                    {
                        // feedback that we lost
                        // start new game
                        double error = rng.NextDouble() / 5;
                        errors.Clear();
                        errors.Add(error);
                        network.BackPropagate(errors);
                        lostGames++;
                        break;
                    }
                    else if (status == Status.Won)
                    {
                        // start new game
                        wonGames++;
                        break;
                    }
                    if (i == 3)
                    {
                        tieGames++;
                    }
                }
            }
            return wonGames;
        }

        [TestMethod]
        public void TTTTestMethod()
        {
            var network2 = new Net(9, 1, 1);
            int[] results2 = new int[1000];
            ulong[] invalidMoves2 = new ulong[1000];
            for (int i = 0; i < 1000; i++)
            {
                invalidMoves2[i] = LearnLegalMoves(network2);
            }

            for (int i = 0; i < 1000; i++)
            {
                results2[i] = Play(network2);
            }

            var network1 = new Net(9, 1, 3);
            int[] results = new int[100];
            ulong[] invalidMoves = new ulong[100];
            for (int i = 0; i < 100; i++)
            {
                invalidMoves[i] = LearnLegalMoves(network1);
            }
            for (int i = 0; i < 100; i++)
            {
                results[i] = Play(network1);
            }

            for (int i = 1; i < 100; i++)
            {
                Assert.IsTrue(results2[i] > results2[i - 1]);
            }
        }
    }
}