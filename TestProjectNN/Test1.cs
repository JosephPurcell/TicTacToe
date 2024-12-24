
using Microsoft.ML.OnnxRuntime;
using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text.Json;
using System.Text;
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

        private Status evaluateMove(List<int> board, int myMove, out int bestMove)
        {
            // prioritize winning moves over preventing losses
            // big switch with all possible winning moves
            // rows *****************************************************/
            if (board[0] == 1 && board[1] == 1 && board[2] == 0 && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            if (board[0] == 1 && board[1] == 0 && board[2] == 1 && myMove == 1)
            {
                bestMove = 1;
                return Status.Won;
            }

            if (board[0] == 0 && board[1] == 1 && board[2] == 1  && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[3] == 1 && board[4] == 1 && board[5] == 0 && myMove == 5)
            {
                bestMove = 5;
                return Status.Won;
            }

            if (board[3] == 1 && board[4] == 0 && board[5] == 1 && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[3] == 0 && board[4] == 1 && board[5] == 1 && myMove == 3)
            {
                bestMove = 3;
                return Status.Won;
            }

            if (board[6] == 1 && board[7] == 1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[6] == 1 && board[7] == 0 && board[8] == 1 && myMove == 7)
            {
                bestMove = 7;
                return Status.Won;
            }

            if (board[6] == 0 && board[7] == 1 && board[8] == 1 && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            // columns
            if (board[0] == 1 && board[3] == 1 && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            if (board[0] == 1 && board[3] == 0 && board[6] == 1 && myMove == 3)
            {
                bestMove = 3;
                return Status.Won;
            }

            if (board[0] == 0 && board[3] == 1 && board[6] == 1 && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[1] == 1 && board[4] == 1 && board[7] == 0 && myMove == 7)
            {
                bestMove = 7;
                return Status.Won;
            }

            if (board[1] == 1 && board[4] == 0 && board[7] == 1 && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[1] == 0 && board[4] == 1 && board[7] == 1 && myMove == 1)
            {
                bestMove = 1;
                return Status.Won;
            }

            if (board[2] == 1 && board[5] == 1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[2] == 1 && board[5] == 0 && board[8] == 1 && myMove == 5)
            {
                bestMove = 5;
                return Status.Won;
            }

            if (board[2] == 0 && board[5] == 1 && board[8] == 1 && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            // diagonals
            if (board[0] == 1 && board[4] == 1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[0] == 1 && board[4] == 0 && board[8] == 1 && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[0] == 0 && board[4] == 1 && board[8] == 1 && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[2] == 1 && board[4] == 1 && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            if (board[2] == 1 && board[4] == 0 && board[6] == 1 && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[2] == 0 && board[4] == 1 && board[6] == 1 && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            // successful blocks 
            if (board[0] == -1 && board[1] == -1 && board[2] == 0 && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            if (board[0] == -1 && board[1] == 0 && board[2] == -1 && myMove == 1)
            {
                bestMove = 1;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[1] == -1 && board[2] == -1 && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[3] == -1 && board[4] == -1 && board[5] == 0 && myMove == 5)
            {
                bestMove = 5;
                return Status.NeutralMove;
            }

            if (board[3] == -1 && board[4] == 0 && board[5] == -1 && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[3] == 0 && board[4] == -1 && board[5] == -1 && myMove == 3)
            {
                bestMove = 3;
                return Status.NeutralMove;
            }

            if (board[6] == -1 && board[7] == -1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[6] == -1 && board[7] == 0 && board[8] == -1 && myMove == 7)
            {
                bestMove = 7;
                return Status.NeutralMove;
            }

            if (board[6] == 0 && board[7] == -1 && board[8] == -1 && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            // columns
            if (board[0] == -1 && board[3] == -1 && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            if (board[0] == -1 && board[3] == 0 && board[6] == -1 && myMove == 3)
            {
                bestMove = 3;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[3] == -1 && board[6] == -1 && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[1] == -1 && board[4] == -1 && board[7] == 0 && myMove == 7)
            {
                bestMove = 7;
                return Status.NeutralMove;
            }

            if (board[1] == -1 && board[4] == 0 && board[7] == -1 && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[1] == 0 && board[4] == -1 && board[7] == -1 && myMove == -1)
            {
                bestMove = 1;
                return Status.NeutralMove;
            }

            if (board[2] == -1 && board[5] == -1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[2] == -1 && board[5] == 0 && board[8] == -1 && myMove == 5)
            {
                bestMove = 5;
                return Status.NeutralMove;
            }

            if (board[2] == 0 && board[5] == -1 && board[8] == -1 && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            // diagonals
            if (board[0] == -1 && board[4] == -1 && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[0] == -1 && board[4] == 0 && board[8] == -1 && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[4] == -1 && board[8] == -1 && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[2] == -1 && board[4] == -1 && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            if (board[2] == -1 && board[4] == 0 && board[6] == -1 && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[2] == 0 && board[4] == -1 && board[6] == -1 && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            // unsuccessful blocks - losses
            // rows *****************************************************/
            if (board[0] == -1 && board[1] == -1 && board[2] == 0 && myMove != 2)
            {
                bestMove = 2;
                return Status.Lost;
            }

            if (board[0] == -1 && board[1] == 0 && board[2] == -1 && myMove != 1)
            {
                bestMove = 1;
                return Status.Lost;
            }

            if (board[0] == 0 && board[1] == -1 && board[2] == -1 && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[3] == -1 && board[4] == -1 && board[5] == 0 && myMove != 5)
            {
                bestMove = 5;
                return Status.Lost;
            }

            if (board[3] == -1 && board[4] == 0 && board[5] == -1 && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[3] == 0 && board[4] == -1 && board[5] == -1 && myMove != 3)
            {
                bestMove = 3;
                return Status.Lost;
            }

            if (board[6] == -1 && board[7] == -1 && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[6] == -1 && board[7] == 0 && board[8] == -1 && myMove != 7)
            {
                bestMove = 7;
                return Status.Lost;
            }

            if (board[6] == 0 && board[7] == -1 && board[8] == -1 && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            // columns
            if (board[0] == -1 && board[3] == -1 && board[6] == 0 && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            if (board[0] == -1 && board[3] == 0 && board[6] == -1 && myMove != 3)
            {
                bestMove = 3;
                return Status.Lost;
            }

            if (board[0] == 0 && board[3] == -1 && board[6] == -1 && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[1] == -1 && board[4] == -1 && board[7] == 0 && myMove != 7)
            {
                bestMove = 7;
                return Status.Lost;
            }

            if (board[1] == -1 && board[4] == 0 && board[7] == -1 && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[1] == 0 && board[4] == -1 && board[7] == -1 && myMove != 1)
            {
                bestMove = 1;
                return Status.Lost;
            }

            if (board[2] == -1 && board[5] == -1 && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[2] == -1 && board[5] == 0 && board[8] == -1 && myMove != 5)
            {
                bestMove = 5;
                return Status.Lost;
            }

            if (board[2] == 0 && board[5] == -1 && board[8] == -1 && myMove != 2)
            {
                bestMove = 2;
                return Status.Lost;
            }

            // diagonals
            if (board[0] == -1 && board[4] == -1 && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[0] == -1 && board[4] == 0 && board[8] == -1 && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[0] == 0 && board[4] == -1 && board[8] == -1 && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[2] == -1 && board[4] == -1 && board[6] == 0 && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            if (board[2] == -1 && board[4] == 0 && board[6] == -1 && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[2] == 0 && board[4] == -1 && board[6] == -1 && myMove != 2)
            {
                bestMove = 2;
                return Status.Lost;
            }

            bestMove = myMove;
            return Status.NeutralMove;
        }

        List<double> FindMoveError(List<int> board, List<double> orig, int move)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < board.Count; i++)
            {
                if (board[i] == 0)
                { // legal moves if missed this error is higher, if made this error is lower
                  // broken out for debugging. compiler will optimize in Release builds
                    double error = orig[i] * (1 - orig[i]) * (.75 - orig[i]);
                    errors.Add(error); // just flag the bad move
                }
                else
                { // illegal moves
                    double error = orig[i] * (1 - orig[i]) * (.25 - orig[i]);
                    errors.Add(error); // just flag the bad move
                }
            }
            return errors;
        }

        List<double> FindGameError(List<int> board, List<double> orig, int bestMove)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < board.Count; i++)
            {
                if (i == bestMove)
                { 
                    double error = orig[i] * (1 - orig[i]) * (.75 - orig[i]);
                    errors.Add(error); // just flag the bad move
                }
                else
                { 
                    double error = orig[i] * (1 - orig[i]) * (.25 - orig[i]);
                    errors.Add(error); // just flag the bad move
                }
            }
            return errors;
        }

        public int FindMove(List<double> nnOut)
        {
            int max = 0;
            for (int i = 0; i < nnOut.Count; i++)
            {
                if (nnOut[i] > nnOut[max])
                {
                    max = i;
                }
            }
            return max;
        }

        public ulong LearnLegalMoves(Net network)
        {
            Random rng = new Random();
            ulong invalidMoves = 0;
            List<double> errors = new List<double>();
            int xMove = rng.Next(9);
            int scale = 1;

            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0,0,0,
                                            0,0,0,
                                            0,0,0 };

                int count = rng.Next(8) + 1;
                for (int i=0;  i < count; i++)
                {
                    while(board[xMove] != 0)
                    {
                        xMove = rng.Next(9);
                    }
                    board[xMove] = scale;
                    scale = -scale;
                }
                List<double> orig = network.Play(board);
                int oMove = FindMove(orig);
                if (board[oMove] != 0)
                {
                    invalidMoves++;
                }
                errors = FindMoveError(board, orig, oMove);
                network.BackPropagate(errors);
                board[oMove] = o;
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

                    List<double> orig = network.Play(board);
                    int oMove = FindMove(orig);
                    if (board[oMove] != 0)
                    {
                        // should it be done learning legal moves
                        errors = FindMoveError(board, orig, oMove);
                        network.BackPropagate(errors);
                        lostGames++;
                        break;
                    }

                    Status status = evaluateMove(board, oMove, out int bestMove);
                    board[oMove] = o;
                    errors = FindGameError(board, orig, bestMove);
                    network.BackPropagate(errors);

                    if (status == Status.Lost)
                    {
                        // feedback that we lost
                        // start new game

                        // fix this, error is hard to pin down might not be the last move
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
            return lostGames;
        }

        [TestMethod]
        public void AllTrainingTest()
        {
            int loopMax = 10000;
            int stability = 0;
            List<LayerDescription> topology = new List<LayerDescription>();
            LayerDescription layer = new LayerDescription(9, 2);
            topology.Add(layer);

            layer = new LayerDescription(36, 2);
            topology.Add(layer);

            layer = new LayerDescription(36, 2);
            topology.Add(layer);

            layer = new LayerDescription(9, 0);
            topology.Add(layer);
            var network = new Net(topology);

            int[] results = new int[loopMax];
            ulong[] invalidMoves = new ulong[loopMax];
            for (int i = 0; i < loopMax; i++)
            {
                invalidMoves[i] = LearnLegalMoves(network);
                if (invalidMoves[i] == 0)
                {
                    if (stability++ > 20)
                    {
                        break;
                    }
                }
                else
                {
                    stability = 0;
                }
            }

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;
            StringBuilder savedLayerWeights = new StringBuilder();
            savedLayerWeights.Append(JsonSerializer.Serialize<Net>(network, jsonOptions));
            File.WriteAllText("weights.json", savedLayerWeights.ToString());

            stability = 0;
            for (int i = 0; i < loopMax; i++)
            {
                results[i] = Play(network);
                if (results[i] == 0)
                {
                    if (stability++ > 10)
                    {
                        break;
                    }
                }
                else
                {
                    stability = 0;
                }
            }

            savedLayerWeights = new StringBuilder();
            savedLayerWeights.Append(JsonSerializer.Serialize<Net>(network, jsonOptions));
            File.WriteAllText("FinalWeights.json", savedLayerWeights.ToString());

            Assert.IsTrue(results[loopMax - 1] == 0);
        }

        [TestMethod]
        public void LearningMovesTest()
        {
            var weightsJson = File.ReadAllText("FinalWeights.json");
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;
            Net network = JsonSerializer.Deserialize<Net>(weightsJson, jsonOptions);
            network.PopulateDeserializedNetwork();

            int loopMax = 10000;
            int stability = 0;
            ulong[] invalidMoves = new ulong[loopMax];

            for (int i = 0; i < loopMax; i++)
            {
                invalidMoves[i] = LearnLegalMoves(network);
                if (invalidMoves[i] == 0)
                {
                    if (stability++ > 20)
                    {
                        break;
                    }
                }
                else
                {
                    stability = 0;
                }
            }

            jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;
            StringBuilder savedLayerWeights = new StringBuilder();
            savedLayerWeights.Append(JsonSerializer.Serialize<Net>(network, jsonOptions));
            File.WriteAllText("FinalWeights.json", savedLayerWeights.ToString());

            Assert.IsTrue(invalidMoves[loopMax - 1] == 0);
        }


        [TestMethod]
        public void PlayingTest()
        {
            var weightsJson = File.ReadAllText("FinalWeights.json");
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;
            Net network = JsonSerializer.Deserialize<Net>(weightsJson, jsonOptions);
            network.PopulateDeserializedNetwork();

            int loopMax = 10000;
            int stability = 0;

            int[] results = new int[loopMax];
            for (int i = 0; i < loopMax; i++)
            {
                results[i] = Play(network);
                if (results[i] == 0)
                {
                    if (stability++ > 10)
                    {
                        break;
                    }
                }
                else
                {
                    stability = 0;
                }
            }

            StringBuilder savedLayerWeights = new StringBuilder();
            savedLayerWeights.Append(JsonSerializer.Serialize<Net>(network, jsonOptions));
            File.WriteAllText("FinalWeights.json", savedLayerWeights.ToString());

            Assert.IsTrue(results[loopMax-1] == 0);
        }
    }
}