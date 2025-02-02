using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Xml.Linq;

namespace TicTacToe
{

    // this is the top level class of the neural network
    public class Net
    {
        const int BiasNodeCount = 2;

        // required for Json deserialization
        public Net()
        { }

        public Net(List<LayerDescription> topology)
        {
            // add input layer
            var layer1 = new Layer(topology[0].Size, topology[0].BiasCount, true);
            Layers.Add(layer1);

            // add hidden layers and output layer
            for (int i = 1; i < topology.Count; i++)
            {
                var layer = new Layer(topology[i].Size, topology[i].BiasCount);
                Layers.Add(layer);
            }

            // Link layers...
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                var thisLayer = Layers[i];
                var nextLayer = Layers[i + 1];
                thisLayer.CreateLinks(nextLayer);
            }
        }

        public Net(int inputSize, int hiddenLayerNeurons, int hiddenLayerDepth, int outputSize)
        {
            // Input layer
            var layer1 = new Layer(inputSize, BiasNodeCount, true);
            Layers.Add(layer1);

            // Middle layers
            for (int i = 0; i < hiddenLayerDepth; i++)
            {
                var middleLayer = new Layer(hiddenLayerNeurons, BiasNodeCount);
                Layers.Add(middleLayer);
            }

            // Output layer
            var outputLayer = new Layer(outputSize, 0);
            Layers.Add(outputLayer);

            // Link layers...
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                var thisLayer = Layers[i];
                var nextLayer = Layers[i + 1];
                thisLayer.CreateLinks(nextLayer);
            }
        }

        // Two configuration operations to complete the network
        // 1. the ForwardNodes in the Net could not be serialized.
        // 2. The Sigmoids can not be serialized.
        // Populate both here to complete the Net configuration.
        public void PopulateDeserializedNetwork()
        {
            // Add Sigmoids
            for (int i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.Width = layer.Nodes.Count;

                for(int j = 0; j < layer.Nodes.Count; j++)
                {
                    Node node = layer.Nodes[j];
                    if (i == 0)
                    {
                        InputNode inputNode = new InputNode((double input) =>
                        {
                            // Mathematical sigmoid function
                            return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                        });
                        inputNode.ForwardNodes = node.ForwardNodes;
                        layer.Nodes[j] = inputNode;
                    }
                    else
                    {
                        node.Sigmoid = (double input) =>
                        {
                            // Mathematical sigmoid function
                            return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                        };
                    }
                }
                foreach (Node node in layer.BiasInputs)
                {
                    node.Sigmoid = (double input) =>
                    {
                        // Mathematical sigmoid function
                        return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                    };
                }
            }

            // link forward nodes
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                var thisLayer = Layers[i];
                var nextLayer = Layers[i + 1];
                thisLayer.PopulateLinks(nextLayer);
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
            firstLayer.CalcLayer(1);

            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.CalcLayer(Layers[i - 1].Nodes.Count + Layers[i - 1].BiasInputs.Count);
            }

            var lastLayer = Layers.Last();

            var results = lastLayer.Nodes.Select(x => x.Result).ToList();

            return results;
        }

        public List<Double> Play(List<int> board)
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
            return results;
        }

        public void BackPropagate(List<double> errors)
        {
            var lastLayer = Layers.Last();
            for (int i = 0; i < lastLayer.Nodes.Count; i++)
            {
                lastLayer.Nodes[i].Error = errors[i];
            }
            for (int i = Layers.Count - 2; i >=0; i--)
            {
                Layers[i].BackPropagate();
            }
        }
    }
}
