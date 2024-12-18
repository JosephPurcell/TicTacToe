using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class InputNode : Node
    {
        public InputNode(Func<double, double> sigmoid, double result = 0) : base(sigmoid, result)
        {
            Sigmoid = sigmoid;
            Result = result;
        }

        public override void CalcNode(int nodeCount)
        {
            // input nodes never average.
            Result = Sigmoid(Result);
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
