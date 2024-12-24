using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    // this class is used to define the layers in a topology
    // It is used to create the Layer objects in the network
    public class LayerDescription
    {
        public int Size { get; set; }
        public int BiasCount { get; set; }

        public LayerDescription(int size, int biasCount) 
        {
            Size = size;
            BiasCount = biasCount;
        }
    }
    
}
