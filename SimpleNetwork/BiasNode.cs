using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicTacToe
{
    public class BiasNode : Node
    {
        public BiasNode(Func<double, double> sigmoid, double result = 0) : base(sigmoid, result)
        {
            Sigmoid = sigmoid;
            Result = result;
        }

        public override void CalcNode(int nodeCount)
        {
            // bias nodes never average and always reapply the bias
            if (double.IsNaN(Result) || double.IsInfinity(Result) || double.IsNegativeInfinity(Result))
            {
                throw new Exception("Bad result!");
            }
            foreach (var valuePair in ForwardNodes)
            {
                var node = valuePair.Key;
                var weight = valuePair.Value;
                node.Result += weight * Sigmoid(Bias);
            }
        }

        public override void BackPropagate()
        {
            foreach (var valuePair in ForwardNodes)
            {
                var forwardNode = valuePair.Key;
                var weight = valuePair.Value;

                // update this weight
                weight += forwardNode.Error * Result;
                ForwardNodes[valuePair.Key] = weight;
            }
        }
    }
}
