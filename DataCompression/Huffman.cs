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

        public Huffman(byte[] sourceData, string fileName, bool isEncode)
        {
            if(isEncode == true)
            {
                encoder = new HuffmanEncoder(sourceData, fileName);
            }
            else
            {
                decoder = new HuffmanDecoder(sourceData, fileName);
            }
        }
    }
}
