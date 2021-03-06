﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataCompression
{
    public partial class DataCompression : Form
    {
        private byte[] inputData;

        public DataCompression()
        {
            InitializeComponent();
        }

        private void DataCompression_Load(object sender, EventArgs e)
        {
            cbAlgorithm.SelectedIndex = 0;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            // Show the open file dialog.
            if (openInputFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Set the text in the text box to the file path.
                    inputTB.Text = openInputFile.FileName;

                    // Read data from the file into the buffer.
                    inputData = File.ReadAllBytes(inputTB.Text);

                    // Get the file size
                    labelFSbytes.Text = inputData.Length.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to read file from disk. Error details: " + ex.Message);
                }
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            // Encode
            if (rbEncode.Checked == true)
            {
                if (cbAlgorithm.SelectedItem.ToString() == "Huffman")
                {
                    HuffmanEncoder encoder;
                    encoder = new HuffmanEncoder(inputData, inputTB.Text);
                }
                else if (cbAlgorithm.SelectedItem.ToString() == "Delta Encoding")
                {
                    DeltaEncoding delta;
                    delta = new DeltaEncoding(inputData, inputTB.Text, true);
                }
                else if (cbAlgorithm.SelectedItem.ToString() == "Run Length Encoding")
                {
                    RunLengthEncoding rle;
                    rle = new RunLengthEncoding(inputData, inputTB.Text, true);
                }
            }

            // Decode
            if (rbDecode.Checked == true)
            {
                if(cbAlgorithm.SelectedItem.ToString() == "Huffman")
                {
                    HuffmanDecoder decoder;
                    decoder = new HuffmanDecoder(inputData, inputTB.Text);
                }
                else if (cbAlgorithm.SelectedItem.ToString() == "Delta Encoding")
                {
                    DeltaEncoding delta;
                    delta = new DeltaEncoding(inputData, inputTB.Text, false);
                }
                else if (cbAlgorithm.SelectedItem.ToString() == "Run Length Encoding")
                {
                    RunLengthEncoding rle;
                    rle = new RunLengthEncoding(inputData, inputTB.Text, false);
                }
            }
        }
    }
}
