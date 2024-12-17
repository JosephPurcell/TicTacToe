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
        public Layer(int width, int Bias)
        {
            Width = width;
            for (int i = 0; i < width; i++) 
            {
                Nodes.Add(new Node((double input) =>
                {
                    // Mathematical sigmoid function
                    return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                }));
            }

            int bias = -1;
            for (int i = 0; i < Bias; i++)
            {
                bias = bias * -1;
                BiasInputs.Add(new Node((double input) =>
                {
                    // Mathematical sigmoid function
                    return (1 / (1 + Math.Pow(Math.E, -1 * input)));
                }));
                BiasInputs[i].Bias = bias;
            }
        }

        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Node> BiasInputs { get; set; } = new List<Node>();

        public void CalcLayer()
        {
            foreach (var node in Nodes)
            {
                node.CalcNode(Nodes.Count + BiasInputs.Count);
            }

            foreach (var node in BiasInputs)
            {
                node.Result = node.Bias;
                node.CalcNode(Nodes.Count + BiasInputs.Count);
            }
        }

        public void BackPropagate()
        {
            // update weights to forward node
            foreach (Node node in Nodes)
            {
                node.Error = 0;
                foreach (var valuePair in node.ForwardNodes)
                {
                    var forwardNode = valuePair.Key;
                    var weight = valuePair.Value;

                    // update this nodes error
                    node.Error += node.Result * (1.0 - node.Result) * (weight * forwardNode.Error);

                    // update this weight
                    weight += forwardNode.Error * node.Result;
                    node.ForwardNodes[valuePair.Key] = weight;
                }
                node.Error = node.Error / node.ForwardNodes.Count;
            }
            foreach (Node node in BiasInputs)
            {
                foreach (var valuePair in node.ForwardNodes)
                {
                    var forwardNode = valuePair.Key;
                    var weight = valuePair.Value;

                    // update this weight
                    weight += forwardNode.Error * node.Result;
                    node.ForwardNodes[valuePair.Key] = weight;
                }
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
