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
        // required for Json deserialization
        public BiasNode()
        { }

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
            foreach (var forwardNode in ForwardNodes)
            {
                forwardNode.Node.Result += forwardNode.Weight * Bias;// Sigmoid(Bias);
            }
        }

        // the BiasNodes and InputNodes could share a back propagate
        // but this makes it easier to set  a breakpoint just for this type
        public override void BackPropagate()
        {
            foreach (var forwardNode in ForwardNodes)
            {
                // update this weight
                forwardNode.Weight += 1.5 * forwardNode.Node.Error * Bias;// Sigmoid(Bias);
            }
        }
    }
}
