using System.Diagnostics;
using System.IO;

namespace DataCompression
{
    class HuffmanEncoder : Huffman
    {
        // Constructor
        public HuffmanEncoder(byte[] sourceData, string fName)
        {
            data = sourceData;
            keyFrequencies = new int[256];

            charList = new CFList();
            huffmanTree = new CFNode();

            countCF = 0;
            bytebuffer = new char[8];

            fileName = fName;
            bitStrings = new string[256];

            // Obtain (key, frequency) pairs for data set.
            storeKeyFrequencies();

            // Insert (key, frequency) pairs into list.
            insertKeysIntoList();

            // Process the Huffman compression algorithm.
            processHuffman();

            // Prepare bit-strings array.
            prepareBitStrings();

            // Encode the source data.
            encodeData();
        }


        // Encode the data.
        public void encodeData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int w = 0, x = 0, y = 0, z = 0;
            int i, f;
            char k;
            string bitstring;
            int bitVal = 0;

            // Calculate header size.
            int headerFileSize = (2 * 4) + (countCF * 5);

            // Calculate output file size.
            int outputFileSize = 0;
            for (i = 0; i < header.Length; i++)
            {
                outputFileSize += (obtainBitString(header[i].key)).Length * (header[i].frequency);
            }

            // Align file size with the nearest byte.
            if (outputFileSize % 8 != 0)
            {
                outputFileSize = outputFileSize / 8;
                outputFileSize++;
            }
            else
            {
                outputFileSize = outputFileSize / 8;
            }

            finalBytes = new byte[headerFileSize + outputFileSize];

            // Write the file length to the file.
            writeIntToBytes(data.Length, w);
            w += 4;

            // Write the length of the header to the file.
            writeIntToBytes(headerFileSize, w);
            w += 4;

            // Write the characters and frequencies to file.
            for (i = 0; i < header.Length; i++)
            {
                k = header[i].key;
                f = header[i].frequency;

                // Update the data to be written.
                finalBytes[w] = (byte)k;
                writeIntToBytes(f, w + 1);
                w += 5;
            }

            // Process source data and output the bit-strings for each character.
            for (x = 0; x < data.Length; x++)
            {
                // Get the bit-string for the character.
                bitstring = bitStrings[(char)data[x]];

                for (y = 0; y < bitstring.Length; y++)
                {
                    // Process the bits into a byte.
                    // Subtract 48 from char '1' or '0' to get the integer 1 or 0.
                    bitVal |= bitstring[y] - 48;
                    bitVal = bitVal << 1;
                    z++;

                    // Eight bits have been processed, enough to make a byte.
                    if (z == 8)
                    {
                        // Reset index.
                        z = 0;

                        // Convert character bit-stream to a byte representation.
                        bitVal = bitVal >> 1;
                        finalBytes[w] = (byte)bitVal;
                        bitVal = 0x00;

                        w++;
                    }
                }
            }

            // Pad the with 0 and write the last byte.
            if (z != 0)
            {
                bitVal = bitVal << (7 - z);
                finalBytes[w] = (byte)bitVal;
            }

            File.WriteAllBytes(fileName + ".enc", finalBytes);

            sw.Stop();
            File.WriteAllText("encodingTime.log", sw.ElapsedMilliseconds.ToString());
        }
    }
}
