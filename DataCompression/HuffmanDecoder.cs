using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class HuffmanDecoder : Huffman
    {
        private int w;

        public HuffmanDecoder(byte[] sourceData, string fName)
        {
            char c = '\0';
            int f = 0;
            w = 0;

            data = sourceData;
            keyFrequencies = new int[256];

            charList = new CFList();
            huffmanTree = new CFNode();

            countCF = 0;
            bytebuffer = new char[8];

            byte[] intBytes = new byte[4];

            fileName = fName;

            // Get the file length.
            fileLength = 0;
            intBytes[0] = data[w];
            intBytes[1] = data[w + 1];
            intBytes[2] = data[w + 2];
            intBytes[3] = data[w + 3];
            w += 4;
            fileLength = convertBytesToInt(intBytes);

            finalBytes = new byte[fileLength];

            // Get the header length.
            headerLength = 0;
            intBytes[0] = data[w];
            intBytes[1] = data[w + 1];
            intBytes[2] = data[w + 2];
            intBytes[3] = data[w + 3];
            w += 4;
            headerLength = convertBytesToInt(intBytes);
            countCF = (headerLength - 8) / 5;
            
            // Identify character frequencies.
            for (int i = 0; i < countCF; i++)
            {
                c = (char) data[w];
                w++;

                intBytes[0] = data[w];
                intBytes[1] = data[w + 1];
                intBytes[2] = data[w + 2];
                intBytes[3] = data[w + 3];
                w += 4;
                f = convertBytesToInt(intBytes);
                insertKeyWithFrequency(c, f);
            }

            // Process the Huffman compression algorithm.
            processHuffman();

            // Decode the source data.
            decodeData();
        }

        public void decodeData()
        {
            int x = 0, y = 0, z = 0;
            char[] bits8 = new char[8];
            bytebuffer = new char[8 * data.Length];
            char outputByte = 'x';

            for (x = w; x < data.Length; x++)
            {
                // Fill buffer with the character bitstring.
                bits8 = convertFromByte(data[x]);
                for (y = 0; y < 8; y++)
                {
                    bytebuffer[8 * (x - w) + y] = bits8[y];
                }
            }

            for (x = 0; x < fileLength; x++)
            {
                // Use bitstring to parse the Huffman tree, searching for the key.
                outputByte = traverseHuffmanTree2(huffmanTree, bytebuffer);

                // Write outputByte into the buffer.
                finalBytes[x] = (byte) outputByte;
            }

            File.WriteAllBytes(fileName +".dec", finalBytes);
        }
    }
}
