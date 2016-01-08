using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class HuffmanEncoder
    {
        private byte[] data;
        private CFList charList;
        private CFNode huffmanTree;
        private int[] keyFrequencies;

        private string KEY_NOT_FOUND = "x";
        private string KEY_FOUND = "y";

        private CFData[] header;

        // The number of unique (key, frequency) pairs.
        private int countCF;

        private char[] bytebuffer;
        private byte[] finalBytes;

        private string fileName;
        private string[] bitStrings;

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

            // Run Insertion Sort on the list.
            insertionSort();

            // Store the header.
            storeHeader();

            // Print out the (key, frequency) pairs.
            printList();

            // Generate the Huffman Tree.
            generateHuffmanTree();

            // Store the bit-strings.
            storeBitStrings();

            // Print out the bit-strings.
            printBitStrings();

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

        // Write the integer number to the output byte array.
        public void writeIntToBytes(int num, int w)
        {
            byte[] byteStream;
            byte[] byteStreamF;

            // Convert the integer to a byte array.
            byteStream = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteStream);
            }
            byteStreamF = byteStream;

            // Update the data to be written.
            finalBytes[w] = byteStreamF[0];
            finalBytes[w + 1] = byteStreamF[1];
            finalBytes[w + 2] = byteStreamF[2];
            finalBytes[w + 3] = byteStreamF[3];
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
            File.WriteAllText("cf_val.out","");
            for (i = 0; i < 256; i++)
            {
                File.AppendAllText("cf_val.out", keyFrequencies[i].ToString() + "\n");
            }
        }

        // Return true if the list of character frequencies is empty.
        public bool isListEmpty()
        {
            if(charList.head != null)
            {
                return false;
            }

            return true;
        }

        // Create a new CFNode.
        public CFNode createNode(char c, int f)
        {
            CFNode temp = new CFNode(c, f);
            temp.next = temp.prev = null;
            temp.left_link = temp.right_link = null;

            return temp;
        }

        // Insert a key into the list.
        public void insertKey(char c, int f)
        {
            CFNode temp = new CFNode();
            temp = createNode(c, f);

            // Check if list is empty.
            if (isListEmpty() == true)
            {
                // Add node to list.
                charList.head = charList.foot = temp;
            }

            // List is non-empty.
            else
            {
                // Add a new node into the list.
                (charList.head).prev = temp;
                temp.next = charList.head;
                charList.head = temp;
            }
        }

        // Insert all (key, frequency) pairs into the list.
        public void insertKeysIntoList()
        {
            int i;

            for (i = 0; i < 256; i++)
            {
                if (keyFrequencies[i] > 0)
                {
                    insertKey((char)i, keyFrequencies[i]);
                    countCF++;
                }
            }
        }

        // Sort the entries in the list based on frequency of occurrence.
        public void insertionSort()
        {
            // The sorted list.
            CFList sortedList = new CFList();

            // Temporary nodes.
            CFNode temp = new CFNode();
            CFNode node1 = new CFNode();
            CFNode node2 = new CFNode();

            // Start at the head of the list.
            node1 = charList.head;
            while(node1 != null)
            {
                temp = node1;

                // Check if this is the first entry in the sorted list.
                if(sortedList.head == null)
                {
                    // Assign the head and foot pointers.
                    sortedList.head = sortedList.foot = temp;

                    // Disconnect the node from the original list.
                    charList.head = temp.next;
                    if(temp.next != null)
                    {
                        (temp.next).prev = null;
                        temp.next = null;
                        temp.prev = null;
                    }
                }

                // There are already entries in the sorted list.
                else
                {
                    charList.head = temp.next;

                    // Insert in sorted order.
                    node2 = sortedList.head;
                    while(node2 != null)
                    {
                        // Current node has a greater frequency than node2.
                        if(temp.frequency > node2.frequency)
                        {
                            // Go to the next node.
                            if(node2.next != null)
                            {
                                node2 = node2.next;
                            }
                            // This is the last entry, so insert here.
                            else
                            {
                                node2.next = temp;
                                temp.prev = node2;
                                temp.next = null;
                                sortedList.foot = temp;
                                break;
                            }
                        }

                        // Fit the current node at this position.
                        else
                        {
                            // Connect the new node.
                            if(node2.prev != null)
                            {
                                (node2.prev).next = temp;
                            }
                            // There are no previous node, this is the head.
                            else
                            {
                                sortedList.head = temp;
                            }

                            temp.prev = (node2.prev);
                            temp.next = node2;
                            node2.prev = temp;
                            break;
                        }
                    }
                }

                node1 = charList.head;
            }

            // The list of character frequencies is now sorted.
            charList = sortedList;
        }

        // Print out the (key, frequency) pairs in the list.
        public void printList()
        {
            CFNode temp = charList.head;

            File.WriteAllText("cf_pairs.out", "");
            while(temp != null)
            {
                File.AppendAllText("cf_pairs.out", (temp.key).ToString() + " - ");
                File.AppendAllText("cf_pairs.out", (temp.frequency).ToString() + "\n");
                temp = temp.next;
            }
        }

        // De-couple the head node from the linked-list.
        public CFNode decoupleHeadNode()
        {
            CFNode temp = new CFNode();
            temp = charList.head;

            if(temp.next != null)
            {
                (temp.next).prev = null;
                charList.head = temp.next;
            }
            else
            {
                charList.head = charList.foot = null;
            }

            temp.next = temp.prev = null;

            return temp;
        }

        // Generate the Huffman Tree.
        public void generateHuffmanTree()
        {
            // Temporary nodes.
            CFNode temp = new CFNode();
            CFNode node1 = null;
            CFNode node2 = null;

            // Keep taking the first two nodes from the priority queue 
            // and combining them to generate a new node with the frequencies 
            // added.
            temp = charList.head;
            while(temp.next != null)
            {
                // De-couple the first two nodes.
                node1 = decoupleHeadNode();
                node2 = decoupleHeadNode();

                // Insert into the Huffman Tree.
                insertIntoTree(node1, node2);

                // Sort the nodes in the priority queue.
                insertionSort();

                // Set the temp node to the list head.
                temp = charList.head;
            }

            // Insert the last entry into the tree.
            huffmanTree = temp;
        }

        // Insert two nodes in the Huffman Tree.
        public void insertIntoTree(CFNode n1, CFNode n2)
        {
            // Temporary nodes.
            CFNode temp = new CFNode();
            CFNode node1 = n1;
            CFNode node2 = n2;

            // Check whether node1 is a leaf node.
            if(node1.isLeaf == true)
            {
                node1.left_link = node1.right_link = null;
            }

            // Check whether node2 is a leaf node.
            if(node2.isLeaf == true)
            {
                node2.left_link = node2.right_link = null;
            }

            // Define the new node.
            temp.isLeaf = false;
            temp.key = '\0';
            temp.frequency = node1.frequency + node2.frequency;
            temp.left_link = node1;
            temp.right_link = node2;
            temp.prev = null;
            temp.next = null;

            // Check if this is the only node left in the list.
            if(charList.head == null)
            {
                charList.head = charList.foot = temp;
            }
            else
            {
                temp.next = charList.head;
                (charList.head).prev = temp;
                charList.head = temp;
            }
        }

        // Traverse the Huffman Tree and obtain the bit-string 
        // for a given character.
        public string traverseHuffmanTree(CFNode rootNode, char k)
        {
            CFNode root = rootNode;
            string retval;
            retval = KEY_NOT_FOUND;

            // The current node is null.
            if(root == null)
            {
                return retval;
            }

            // This is a leaf node.
            if(root.isLeaf == true)
            {
                // Check whether this is the character being searched for.
                if(root.key == k)
                {
                    retval = KEY_FOUND;
                    return retval;
                }
                // This is not the character we are searching for.
                else
                {
                    return retval;
                }
            }

            // This is an intermediate node.
            else
            {
                string bitstring;

                // Search left sub-tree.
                if(root.left_link != null)
                {
                    retval = traverseHuffmanTree(root.left_link, k);

                    if(String.Compare(retval.ToString(), KEY_NOT_FOUND) != 0)
                    {
                        if(String.Compare(retval.ToString(), KEY_FOUND) == 0)
                        {
                            bitstring = "0";
                        }
                        else
                        {
                            bitstring = "0" + retval;
                        }

                        return bitstring;
                    }
                }

                // Search right sub-tree.
                if (root.right_link != null)
                {
                    retval = traverseHuffmanTree(root.right_link, k);

                    if (String.Compare(retval.ToString(), KEY_NOT_FOUND) != 0)
                    {
                        if (String.Compare(retval.ToString(), KEY_FOUND) == 0)
                        {
                            bitstring = "1";
                        }
                        else
                        {
                            bitstring = "1" + retval;
                        }

                        return bitstring;
                    }
                }
            }

            return retval;
        }

        // Record the header information for encoding the file.
        public void storeHeader()
        {
            int i = 0;

            CFNode temp = charList.head;
            header = new CFData[countCF];

            while(temp != null)
            {
                header[i] = new CFData();
                header[i].key = temp.key;
                header[i].frequency = temp.frequency;
                i++;

                temp = temp.next;
            }
        }

        // Traverse the Huffman Tree and store the bit-strings for 
        // each character.
        public void storeBitStrings()
        {
            int i;
            string bitstring;

            for(i = 0; i < header.Length; i++)
            {
                bitstring = traverseHuffmanTree(huffmanTree, header[i].key);
                header[i].bitstring = bitstring;
            }
        }

        // Store all the bit-strings in an easy to reference array.
        public void prepareBitStrings()
        {
            int i;

            for(i = 0; i < 256; i++)
            {
                bitStrings[i] = obtainBitString((char)i);
            }
        }

        // Print out the bit-strings for the (key, frequency) pairs.
        public void printBitStrings()
        {
            int i;

            File.WriteAllText("cf_bitstrings.out", "");
            for(i = 0; i < header.Length; i++)
            {
                File.AppendAllText("cf_bitstrings.out", (header[i].key).ToString() + " - ");
                File.AppendAllText("cf_bitstrings.out", (header[i].frequency).ToString() + " - ");
                File.AppendAllText("cf_bitstrings.out", (header[i].bitstring + "\n"));
            }
        }

        // Convert a bit-stringto a byte representation
        public byte convertToByte(char[] bytebuffer)
        {
            //int i;
            int n = 0;

            if(bytebuffer[0] == '1')
            {
                n += 128;
            }
            if (bytebuffer[1] == '1')
            {
                n += 64;
            }
            if (bytebuffer[2] == '1')
            {
                n += 32;
            }
            if (bytebuffer[3] == '1')
            {
                n += 16;
            }
            if (bytebuffer[4] == '1')
            {
                n += 8;
            }
            if (bytebuffer[5] == '1')
            {
                n += 4;
            }
            if (bytebuffer[6] == '1')
            {
                n += 2;
            }
            if (bytebuffer[7] == '1')
            {
                n += 1;
            }

            /*
            for(i = 0; i < 8; i++)
            {
                if(bytebuffer[i] == '1')
                {
                    n += (1 << (7 - i));
                }
            }*/

            return (byte)n;
        }

        // Return the bit-string for a given character.
        public string obtainBitString(char k)
        {
            int i;

            for(i = 0; i < header.Length; i++)
            {
                if(header[i].key == k)
                {
                    return header[i].bitstring;
                }
            }

            return "";
        }
    }
}
