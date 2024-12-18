using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
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
