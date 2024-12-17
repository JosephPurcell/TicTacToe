using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicTacToe
{
    public class Net
    {
        public Net(int inputSize, int outputSize, int depth)
        {
            // Input layer
            var layer1 = new Layer(inputSize,0);
            Layers.Add(layer1);

            // Middle layers
            for(int i = 0; i < depth; i++) 
            {
                var middleLayer = new Layer(inputSize,0);
                Layers.Add(middleLayer);
            }

            // Output layer
            var outputLayer = new Layer(outputSize,0);
            Layers.Add(outputLayer);

            // Link layers...
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                var thisLayer = Layers[i];
                var nextLayer = Layers[i+1];
                thisLayer.LinkLayer(nextLayer);
            }
        }

        public List<Layer> Layers { get; set; } = new List<Layer>();

        public List<double> GetResults(List<double> inputs)
        {
            // Load inputs first
            var firstLayer = Layers[0];
            var localInputs = new List<double>(inputs);
            for(int i = 0; i < localInputs.Count; i++)
            {
                firstLayer.Nodes[i].Result = localInputs[i];
            }

            for(int i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.CalcLayer();
            }

            var lastLayer = Layers.Last();

            var results = lastLayer.Nodes.Select(x => x.Result).ToList();

            return results;
        }

        //public void Train(List<Trainer> trainingData, double acceptableScore)
        //{
        //    Random r = new Random(17);
        //    while (true)
        //    {
        //        var layers = Layers[0..];
        //        foreach (var layer in layers)
        //        {
        //            foreach (var node in layer.Nodes)
        //            {
        //                foreach (var key in node.InputNodes.Keys)
        //                {
        //                    var originalScore = GetScore(trainingData);
        //                    var originalValue = node.InputNodes[key];
        //                    node.InputNodes[key] += r.NextDouble() < 0.5 ? -1* r.NextDouble() * 10 : r.NextDouble()* 10;
        //                    var newScore = GetScore(trainingData);

        //                    // Less is a better score!
        //                    if(newScore < originalScore)
        //                    {
        //                        Console.WriteLine("Improved node!");
        //                    }
        //                    else
        //                    {
        //                        // Set value back to original value
        //                        node.InputNodes[key] = originalValue;
        //                    }
        //                }
        //            }
        //        }

        //        var score = GetScore(trainingData);
        //        Console.WriteLine($"Scored: {score}");

        //        if(score < acceptableScore)
        //        {
        //            Console.WriteLine("Training passed!");
        //            return;
        //        }
        //    }
        //}

        public double Play(List<int> board)
        {
            // clear last results
            foreach (var layer in Layers)
            {
                layer.ClearResults();
            }

            List<double> inputs = new List<double>();
            for (int i = 0; i < board.Count; i++)
            {
                inputs.Add(board[i]);
            }
            var results = this.GetResults(inputs);
            return results[0];
        }

        public void BackPropagate(List<double> errors)
        {
            var lastLayer = Layers.Last();
            for (int i = 0; i < lastLayer.Nodes.Count; i++)
            {
                lastLayer.Nodes[i].Error = errors[i];
            }
            for (int i = Layers.Count - 2; i >0; i--)
            {
                Layers[i].BackPropagate();
            }
        }


        public double GetScore(List<double> inputData)
        {
            // clear last results
            foreach (var layer in Layers)
            {
                layer.ClearResults();
            }

            // apply inputs and process
            var score = 0.0;
            for(int i=0; i<9; i++)
            {
                Layers[0].Nodes[i].Result = inputData[i];
            }

            for(int i=0; i<Layers.Count-1; i++)
            {
                var layer = Layers[i];
                foreach(var node in layer.Nodes)
                {
                    node.CalcNode();
                }
            }
            return score;
        }
    }
}
