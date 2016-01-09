using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            byte outputByte = 0x00;
            string bitstring;

            //File.AppendAllText("outputfile.enc", "");

            // Calculate header size.
            int headerFileSize = (2 * 4) + (countCF * 5);

            // Calculate output file size.
            int outputFileSize = 0;
            for (i = 0; i < header.Length; i++)
            {
                outputFileSize += (obtainBitString(header[i].key)).Length * (header[i].frequency);
            }
            // Align file size with the nearest byte.
            if(outputFileSize % 8 != 0)
            {
                outputFileSize = outputFileSize / 8;
                outputFileSize++;
            }
            else
            {
                outputFileSize = outputFileSize / 8;
            }

            File.AppendAllText("outputinfo.log", "header: " + headerFileSize.ToString() + "\n");
            File.AppendAllText("outputinfo.log", "output: " + outputFileSize.ToString());

            finalBytes = new byte[headerFileSize + outputFileSize];

            // Write the file length to the file.
            writeIntToBytes(data.Length, w);
            //writeIntToBytes(outputFileSize, w);
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
                finalBytes[w] = (byte) k;
                w++;
                writeIntToBytes(f, w);
                w += 4;
            }

            for (x = 0; x < data.Length; x++)
            {
                // Get the bit-string for the character.
                //bitstring = obtainBitString((char)data[x]);
                bitstring = bitStrings[(char)data[x]];

                for (y = 0; y < bitstring.Length; y++)
                {
                    bytebuffer[z] = bitstring[y];
                    z++;

                    // Byte buffer is full.
                    if (z >= 8)
                    {
                        // Reset index.
                        z = 0;

                        // Convert character bit-stream to a byte representation.
                        outputByte = convertToByte(bytebuffer);

                        // Write the byte to file.
                        //File.AppendAllText("outputfile.enc", outputByte.ToString());
                        finalBytes[w] = outputByte;
                        w++;

                        // Clear the output byte
                        outputByte = 0x00;
                    }
                }
            }

            // Pad the output byte with 0.
            if (z != 0)
            {
                for (y = z; y < 8; y++)
                {
                    bytebuffer[y] = '0';
                }

                outputByte = convertToByte(bytebuffer);
                //File.AppendAllText("outputfile.enc", outputByte.ToString());
                finalBytes[w] = outputByte;
                w++;
                outputByte = 0x00;
            }

            File.WriteAllBytes(fileName + ".enc", finalBytes);

            sw.Stop();
            File.WriteAllText("encodingTime.log", sw.ElapsedMilliseconds.ToString());
        }
    }
}
