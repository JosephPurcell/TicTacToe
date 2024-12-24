using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicTacToe
{
    public class ForwardNode
    {
        [JsonIgnore]
        public Node Node { get; set; }
        public double Weight { get; set; }

        public ForwardNode(Node node, double weight) 
        { 
            Node = node;
            Weight = weight;
        }
    }
    public class Node
    {
        // required for Json deserialization
        public Node()
        { }

        //Function to get random number
        private static readonly Random getrandom = new Random();

        public static double GetRandomNumber(double min = 0)
        {
            // purpose of min was allow negative out[ut from
            // the sigmoids. Didn't use it but left it in.
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
            ForwardNodes = new List<ForwardNode>();
        }

        public List<ForwardNode> ForwardNodes { get; set; }

        public double Result { get; set; }

        public double Bias { get; set; }

        public double Error { get; set;  }

        [JsonIgnore]
        public Func<double, double> Sigmoid { get; set; }

        public void AddForwardNode(Node node)
        {
            ForwardNode forwardNode = new ForwardNode(node, GetRandomNumber());
            ForwardNodes.Add(forwardNode);
        }

        public virtual void CalcNode(int nodeCount = 1)
        {
            // hidden nodes will weighted inputs before applying sigmoid
            Result = Sigmoid(Result / nodeCount);
            if (double.IsNaN(Result) || double.IsInfinity(Result) || double.IsNegativeInfinity(Result))
            {
                throw new Exception("Bad result!");
            }
            foreach (var forwardNode in ForwardNodes)
            {
                forwardNode.Node.Result += forwardNode.Weight * Result;
            }
        }

        public virtual void BackPropagate()
        {
            Error = 0;
            foreach (var forwardNode in ForwardNodes)
            {
                // update this nodes error
                Error += Result * (1.0 - Result) * (forwardNode.Weight * forwardNode.Node.Error);

                // update this weight
                forwardNode.Weight += 1.5 * forwardNode.Node.Error * Result;
            }
            Error = Error / ForwardNodes.Count;
        }
    }
}
