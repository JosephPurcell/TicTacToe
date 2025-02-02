
using Microsoft.ML.OnnxRuntime;
using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text.Json;
using System.Text;
using TicTacToe;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

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

        private Status evaluateMove(List<int> board, int myMove, int player, out int bestMove)
        {
            // prioritize winning moves over preventing losses
            // big switch with all possible winning moves
            // rows *****************************************************/
            if (board[0] == player && board[1] == player && board[2] == 0 && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            if (board[0] == player && board[1] == 0 && board[2] == player && myMove == 1)
            {
                bestMove = 1;
                return Status.Won;
            }

            if (board[0] == 0 && board[1] == player && board[2] == player  && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[3] == player && board[4] == player && board[5] == 0 && myMove == 5)
            {
                bestMove = 5;
                return Status.Won;
            }

            if (board[3] == player && board[4] == 0 && board[5] == player && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[3] == 0 && board[4] == player && board[5] == player && myMove == 3)
            {
                bestMove = 3;
                return Status.Won;
            }

            if (board[6] == player && board[7] == player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[6] == player && board[7] == 0 && board[8] == player && myMove == 7)
            {
                bestMove = 7;
                return Status.Won;
            }

            if (board[6] == 0 && board[7] == player && board[8] == player && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            // columns
            if (board[0] == player && board[3] == player && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            if (board[0] == player && board[3] == 0 && board[6] == player && myMove == 3)
            {
                bestMove = 3;
                return Status.Won;
            }

            if (board[0] == 0 && board[3] == player && board[6] == player && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[1] == player && board[4] == player && board[7] == 0 && myMove == 7)
            {
                bestMove = 7;
                return Status.Won;
            }

            if (board[1] == player && board[4] == 0 && board[7] == player && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[1] == 0 && board[4] == player && board[7] == player && myMove == 1)
            {
                bestMove = 1;
                return Status.Won;
            }

            if (board[2] == player && board[5] == player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[2] == player && board[5] == 0 && board[8] == player && myMove == 5)
            {
                bestMove = 5;
                return Status.Won;
            }

            if (board[2] == 0 && board[5] == player && board[8] == player && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            // diagonals
            if (board[0] == player && board[4] == player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.Won;
            }

            if (board[0] == player && board[4] == 0 && board[8] == player && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[0] == 0 && board[4] == player && board[8] == player && myMove == 0)
            {
                bestMove = 0;
                return Status.Won;
            }

            if (board[2] == player && board[4] == player && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.Won;
            }

            if (board[2] == player && board[4] == 0 && board[6] == player && myMove == 4)
            {
                bestMove = 4;
                return Status.Won;
            }

            if (board[2] == 0 && board[4] == player && board[6] == player && myMove == 2)
            {
                bestMove = 2;
                return Status.Won;
            }

            // successful blocks 
            if (board[0] == -player && board[1] == -player && board[2] == 0 && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            if (board[0] == -player && board[1] == 0 && board[2] == -player && myMove == 1)
            {
                bestMove = 1;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[1] == -player && board[2] == -player && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[3] == -player && board[4] == -player && board[5] == 0 && myMove == 5)
            {
                bestMove = 5;
                return Status.NeutralMove;
            }

            if (board[3] == -player && board[4] == 0 && board[5] == -player && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[3] == 0 && board[4] == -player && board[5] == -player && myMove == 3)
            {
                bestMove = 3;
                return Status.NeutralMove;
            }

            if (board[6] == -player && board[7] == -player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[6] == -player && board[7] == 0 && board[8] == -player && myMove == 7)
            {
                bestMove = 7;
                return Status.NeutralMove;
            }

            if (board[6] == 0 && board[7] == -player && board[8] == -player && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            // columns
            if (board[0] == -player && board[3] == -player && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            if (board[0] == -player && board[3] == 0 && board[6] == -player && myMove == 3)
            {
                bestMove = 3;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[3] == -player && board[6] == -player && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[1] == -player && board[4] == -player && board[7] == 0 && myMove == 7)
            {
                bestMove = 7;
                return Status.NeutralMove;
            }

            if (board[1] == -player && board[4] == 0 && board[7] == -player && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[1] == 0 && board[4] == -player && board[7] == -player && myMove == 1)
            {
                bestMove = 1;
                return Status.NeutralMove;
            }

            if (board[2] == -player && board[5] == -player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[2] == -player && board[5] == 0 && board[8] == -player && myMove == 5)
            {
                bestMove = 5;
                return Status.NeutralMove;
            }

            if (board[2] == 0 && board[5] == -player && board[8] == -player && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            // diagonals
            if (board[0] == -player && board[4] == -player && board[8] == 0 && myMove == 8)
            {
                bestMove = 8;
                return Status.NeutralMove;
            }

            if (board[0] == -player && board[4] == 0 && board[8] == -player && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[0] == 0 && board[4] == -player && board[8] == -player && myMove == 0)
            {
                bestMove = 0;
                return Status.NeutralMove;
            }

            if (board[2] == -player && board[4] == -player && board[6] == 0 && myMove == 6)
            {
                bestMove = 6;
                return Status.NeutralMove;
            }

            if (board[2] == -player && board[4] == 0 && board[6] == -player && myMove == 4)
            {
                bestMove = 4;
                return Status.NeutralMove;
            }

            if (board[2] == 0 && board[4] == -player && board[6] == -player && myMove == 2)
            {
                bestMove = 2;
                return Status.NeutralMove;
            }

            // unsuccessful blocks - losses
            // rows *****************************************************/
            if (board[0] == -player && board[1] == -player && board[2] == 0 && myMove != 2)
            {
                bestMove = 2;
                return Status.Lost;
            }

            if (board[0] == -player && board[1] == 0 && board[2] == -player && myMove != 1)
            {
                bestMove = 1;
                return Status.Lost;
            }

            if (board[0] == 0 && board[1] == -player && board[2] == -player && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[3] == -player && board[4] == -player && board[5] == 0 && myMove != 5)
            {
                bestMove = 5;
                return Status.Lost;
            }

            if (board[3] == -player && board[4] == 0 && board[5] == -player && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[3] == 0 && board[4] == -player && board[5] == -player && myMove != 3)
            {
                bestMove = 3;
                return Status.Lost;
            }

            if (board[6] == -player && board[7] == -player && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[6] == -player && board[7] == 0 && board[8] == -player && myMove != 7)
            {
                bestMove = 7;
                return Status.Lost;
            }

            if (board[6] == 0 && board[7] == -player && board[8] == -player && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            // columns
            if (board[0] == -player && board[3] == -player && board[6] == 0 && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            if (board[0] == -player && board[3] == 0 && board[6] == -player && myMove != 3)
            {
                bestMove = 3;
                return Status.Lost;
            }

            if (board[0] == 0 && board[3] == -player && board[6] == -player && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[1] == -player && board[4] == -player && board[7] == 0 && myMove != 7)
            {
                bestMove = 7;
                return Status.Lost;
            }

            if (board[1] == -player && board[4] == 0 && board[7] == -player && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[1] == 0 && board[4] == -player && board[7] == -player && myMove != 1)
            {
                bestMove = 1;
                return Status.Lost;
            }

            if (board[2] == -player && board[5] == -player && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[2] == -player && board[5] == 0 && board[8] == -player && myMove != 5)
            {
                bestMove = 5;
                return Status.Lost;
            }

            if (board[2] == 0 && board[5] == -player && board[8] == -player && myMove != 2)
            {
                bestMove = 2;
                return Status.Lost;
            }

            // diagonals
            if (board[0] == -player && board[4] == -player && board[8] == 0 && myMove != 8)
            {
                bestMove = 8;
                return Status.Lost;
            }

            if (board[0] == -player && board[4] == 0 && board[8] == -player && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[0] == 0 && board[4] == -player && board[8] == -player && myMove != 0)
            {
                bestMove = 0;
                return Status.Lost;
            }

            if (board[2] == -player && board[4] == -player && board[6] == 0 && myMove != 6)
            {
                bestMove = 6;
                return Status.Lost;
            }

            if (board[2] == -player && board[4] == 0 && board[6] == -player && myMove != 4)
            {
                bestMove = 4;
                return Status.Lost;
            }

            if (board[2] == 0 && board[4] == -player && board[6] == -player && myMove != 2)
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

        public ulong LearnLegalMoves(Net network, int player)
        {
            Random rng = new Random();
            ulong invalidMoves = 0;
            List<double> errors = new List<double>();
            int autoMove = rng.Next(9);

            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0,0,0,
                                            0,0,0,
                                            0,0,0 };

                int count = rng.Next(8) + 1; //rng() will return 0 -> 7, but I want at least 1 move
                int scale = -1;
                for (int i=0;  i < count; i++)
                {
                    while(board[autoMove] != 0)
                    {
                        autoMove = rng.Next(9); // squares 0 -> 8
                    }
                    board[autoMove] = scale;
                    scale = -scale; // x's and o's
                }

                List<double> orig = network.Play(board);
                int playerMove = FindMove(orig);
                if (board[playerMove] != 0)
                {
                    invalidMoves++;
                }
                errors = FindMoveError(board, orig, playerMove);
                network.BackPropagate(errors);
                board[playerMove] = player;
            }
            return invalidMoves;
        }

        public int Play(Net network, int player)
        {
            int lostGames = 0;
            int wonGames = 0;
            int tieGames = 0;
            int turns = 0;
            int playerMove;
            List<double> orig;
            List<double> errors = new List<double>();

            Random rng = new Random();


            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0, 0, 0,
                                            0, 0, 0,
                                            0, 0, 0 };
                while(((turns < 9) && (player == x))
                    || ((turns < 8) && (player == o)))
                {
                    turns += 2; // each player moves once
                    if (player == o)
                    {
                        int xMove = rng.Next(9);
                        while (board[xMove] != 0)
                        {
                            xMove = rng.Next(9);
                        }
                        board[xMove] = x;

                        orig = network.Play(board);
                        playerMove = FindMove(orig);
                        if (board[playerMove] != 0)
                        {
                            // should it be done learning legal moves
                            errors = FindMoveError(board, orig, playerMove);
                            network.BackPropagate(errors);
                            lostGames++;
                            break;
                        }
                    }
                    else
                    {
                        orig = network.Play(board);
                        playerMove = FindMove(orig);
                        if (board[playerMove] != 0)
                        {
                            // should it be done learning legal moves
                            errors = FindMoveError(board, orig, playerMove);
                            network.BackPropagate(errors);
                            lostGames++;
                            break;
                        }

                        int oMove = rng.Next(9);
                        while (board[oMove] != 0)
                        {
                            oMove = rng.Next(9);
                        }
                        board[oMove] = x;
                    }


                    Status status = evaluateMove(board, playerMove, player, out int bestMove);
                    board[playerMove] = player;
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
                    if (turns == 9)
                    {
                        tieGames++;
                    }
                }
            }
            return lostGames;
        }

        [TestMethod]
        public void AllTrainingTestPlayerO()
        {
            int loopMax = 10000;
            int stability = 0;
            int player = o;

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
                invalidMoves[i] = LearnLegalMoves(network, player);
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
                results[i] = Play(network, o);
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
            int player = 1; //o

            int loopMax = 10000;
            int stability = 0;
            ulong[] invalidMoves = new ulong[loopMax];

            for (int i = 0; i < loopMax; i++)
            {
                invalidMoves[i] = LearnLegalMoves(network, player);
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
            PlayingTest(o, "FinalWeightsO.json");
            PlayingTest(x, "FinalWeightsX.json");
        }

        public void PlayingTest(int player, string finalweights)
        { 
            var weightsJson = File.ReadAllText("weights.json");
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;
            Net network = JsonSerializer.Deserialize<Net>(weightsJson, jsonOptions);
            network.PopulateDeserializedNetwork();

            int loopMax = 10000;
            int stability = 0;

            int[] results = new int[loopMax];
            for (int i = 0; i < loopMax; i++)
            {
                results[i] = Play(network, player);
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
            File.WriteAllText(finalweights, savedLayerWeights.ToString());

            Assert.IsTrue(results[loopMax-1] == 0);
        }

        [TestMethod]
        public void PlayingAgainstItself()
        {
            int lostGames = 0;
            int wonGames = 0;
            int tieGames = 0;
            int moveCount = 0;

            var weightsJsonX = File.ReadAllText("FinalWeightsX.json");
            var weightsJsonO = File.ReadAllText("FinalWeightsO.json");
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.WriteIndented = true;

            // player #1
            Net player1 = JsonSerializer.Deserialize<Net>(weightsJsonX, jsonOptions);
            player1.PopulateDeserializedNetwork();

            // player #2
            Net player2 = JsonSerializer.Deserialize<Net>(weightsJsonO, jsonOptions);
            player2.PopulateDeserializedNetwork();

            for (int loop = 0; loop < 100000; loop++)
            {
                List<int> board = new List<int>() { 0, 0, 0,
                                            0, 0, 0,
                                            0, 0, 0 };

                moveCount = 0;
                while(true)
                {
                    List<double> player1Move = player1.Play(board);
                    moveCount++;
                    int xMove = FindMove(player1Move);
                    Status status = EvaluatePlayerMove(xMove, x, board, out int bestMoveX);
                    board[xMove] = x;

                    if (status == Status.Lost)
                    {
                        List<double> errors = FindGameError(board, player1Move, bestMoveX);
                        player1.BackPropagate(errors);
                        lostGames++;
                        break;
                    }
                    else if (status == Status.Won)
                    {
                        wonGames++;
                        break;
                    }

                    if(moveCount == 9)
                    {
                        tieGames++;
                        break;
                    }

                    List<double> player2Move = player2.Play(board);
                    moveCount++;
                    int oMove = FindMove(player2Move);
                    status = EvaluatePlayerMove(oMove, o, board, out int bestMoveO);
                    board[oMove] = o;
                    if (status == Status.Lost)
                    {
                        List<double> errors = FindGameError(board, player2Move, bestMoveO);
                        player2.BackPropagate(errors);
                        lostGames++;
                        break;
                    }
                    else if (status == Status.Won)
                    {
                        wonGames++;
                        break;
                    }
                }
            }
            return;
        }

        Status EvaluatePlayerMove(int oMove, int player, List<int> board, out int bestMove)
        {
            bestMove = 0;
            if (board[oMove] != 0)
            {
                for(int i = 0; i < board.Count; i++)
                {
                    if(board[i] == 0)
                    {
                        bestMove = i;
                        break;
                    }
                }
                return Status.Lost;
            }

            Status status = evaluateMove(board, oMove, player, out int bestMove2);
            bestMove = bestMove2;
            return status;
        }
    }
}