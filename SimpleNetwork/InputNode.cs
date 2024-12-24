using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class InputNode : Node
    {
        // required for Json deserialization
        public InputNode()
        { }

        public InputNode(Func<double, double> sigmoid, double result = 0) : base(sigmoid, result)
        {
            Sigmoid = sigmoid;
            Result = result;
        }

        public override void CalcNode(int nodeCount)
        {
            // input nodes never average.
            //Result = Sigmoid(Result);
            if (double.IsNaN(Result) || double.IsInfinity(Result) || double.IsNegativeInfinity(Result))
            {
                throw new Exception("Bad result!");
            }
            foreach (var forwardNode in ForwardNodes)
            {
                forwardNode.Node.Result += forwardNode.Weight * Result;
            }
        }

        // the BiasNodes and InputNodes could share a back propagate
        // but this makes it easier to set  a breakpoint just for this type
        public override void BackPropagate()
        {
            foreach (var forwardNode in ForwardNodes)
            {
                // update this weight
                forwardNode.Weight += 1.5 * forwardNode.Node.Error * Result;
            }
        }
    }
}
