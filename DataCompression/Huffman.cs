using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class Huffman
    {
        private HuffmanEncoder encoder;
        private HuffmanDecoder decoder;

        public Huffman(byte[] sourceData)
        {
            encoder = new HuffmanEncoder(sourceData);
        }
    }
}
