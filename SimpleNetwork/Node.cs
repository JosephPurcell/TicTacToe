using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Node
    {
        //Function to get random number
        private static readonly Random getrandom = new Random();

        public static double GetRandomNumber(double min = 0)
        {
            lock (getrandom) // synchronize
            {
                double random = getrandom.NextDouble();

                return getrandom.NextDouble() * (1.0 - min) + min;
            }
        }

        public Node(Func<double, double> sigmoid, double result = 0)
        {
            Sigmoid = sigmoid;
            Result = result;
        }

        public Dictionary<Node, double> ForwardNodes = new Dictionary<Node, double>();

        public double Result { get; set; }

        public double Bias { get; set; }

        public double Error { get; set;  }
        public Func<double, double> Sigmoid { get; set; }

        public void AddForwardNode(Node node)
        {
            ForwardNodes.Add(node, GetRandomNumber());
        }

        public void CalcNode(int nodeCount = 9)
        {
            Result = Sigmoid(Result/ nodeCount);
            if (double.IsNaN(Result) || double.IsInfinity(Result) || double.IsNegativeInfinity(Result))
            {
                throw new Exception("Bad result!");
            }
            foreach (var valuePair in ForwardNodes)
            {
                var node = valuePair.Key;
                var weight = valuePair.Value;
                node.Result += weight * Result;
            }
        }
    }
}
