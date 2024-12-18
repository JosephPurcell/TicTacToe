using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Layer
    {
        int Width { get; set; } = 0;
        public Layer(int width, int Bias, bool inputLayer = false)
        {
            Width = width;
            for (int i = 0; i < width; i++) 
            {
                if (inputLayer)
                {
                    Nodes.Add(new InputNode((double input) =>
                    {
                        // Mathematical sigmoid function
                        return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                    }));
                }
                else
                {
                    Nodes.Add(new Node((double input) =>
                    {
                        // Mathematical sigmoid function
                        return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                    }));
                }
            }

            // could just make these nodes part of the Nodes<> list but it is
            // easier to set a breakpoint on them this way or change the activation
            // function for biases only
            int bias = -1;
            for (int i = 0; i < Bias; i++)
            {
                bias = bias * -1;
                BiasInputs.Add(new BiasNode((double input) =>
                {
                    // Mathematical sigmoid function
                    return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                }));
                BiasInputs[i].Bias = bias;
            }
        }

        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Node> BiasInputs { get; set; } = new List<Node>();

        public void CalcLayer(int previousLayerNodeCount)
        {
            foreach (var node in Nodes)
            {
                node.CalcNode(previousLayerNodeCount);
            }

            foreach (var node in BiasInputs)
            {
                node.CalcNode();
            }
        }

        public void BackPropagate()
        {
            // update weights to forward nodes
            foreach (Node node in Nodes)
            {
                node.BackPropagate();
            }
            foreach (Node node in BiasInputs)
            {
                node.BackPropagate();
            }
        }

        public void ClearResults()
        {
            for(int i=0; i<Width; i++)
            {
                Nodes[i].Result = 0;
            }
        }

        public void LinkLayer(Layer nextLayer)
        {
            foreach (var node in Nodes)
            {
                for (int i = 0; i < nextLayer.Width; i++)
                {
                    node.AddForwardNode(nextLayer.Nodes[i]);
                }
            }

            foreach (var node in BiasInputs)
            {
                for (int i = 0; i < nextLayer.Width; i++)
                {
                    node.AddForwardNode(nextLayer.Nodes[i]);
                }
            }
        }
    }
}
