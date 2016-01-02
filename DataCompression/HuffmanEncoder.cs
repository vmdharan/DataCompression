using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class HuffmanEncoder
    {
        private byte[] data;
        private CFNode huffmanTree;
        private int[] keyFrequencies;

        public HuffmanEncoder(byte[] sourceData)
        {
            data = sourceData;
            keyFrequencies = new int[256];

            storeKeyFrequencies();
        }

        // Read the input data and store the frequency of occurence of
        // all the characters.
        public void storeKeyFrequencies()
        {
            int i;

            // Update the character's frequency of occurrence.
            for (i = 0; i < data.Length; i++)
            {
                keyFrequencies[(int)(data[i])]++;
            }

            // Print out the list of character frequencies for debugging.
            File.Create("cf_val.out");
            for (i = 0; i < 256; i++)
            {
                File.AppendAllText("cf_val.out", keyFrequencies[i].ToString() + "\n");
            }
        }
    }
}
