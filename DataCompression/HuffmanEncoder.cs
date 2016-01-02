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
        private CFList charList;
        private CFNode huffmanTree;
        private int[] keyFrequencies;

        // The number of unique (key, frequency) pairs.
        private int countCF;

        // Constructor
        public HuffmanEncoder(byte[] sourceData)
        {
            data = sourceData;
            keyFrequencies = new int[256];

            charList = new CFList();
            huffmanTree = new CFNode();

            countCF = 0;
            
            // Obtain (key, frequency) pairs for data set.
            storeKeyFrequencies();

            // Insert (key, frequency) pairs into list.
            insertKeysIntoList();

            // Run Insertion Sort on the list.
            insertionSort();

            // Print out the (key, frequency) pairs.
            printList();

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
            int i = 0;
            CFNode temp = charList.head;

            File.WriteAllText("cf_pairs.out", "");
            while(temp != null)
            {
                File.AppendAllText("cf_pairs.out", (temp.key).ToString() + " - ");
                File.AppendAllText("cf_pairs.out", (temp.frequency).ToString() + "\n");
                temp = temp.next;
            }
        }
    }
}
