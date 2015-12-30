using System;
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
            Stream stream1 = null;

            // Show the open file dialog.
            if(openInputFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Attempt to open the selected file.
                    if((stream1 = openInputFile.OpenFile()) != null)
                    {
                        // Set the text in the text box to the file path.
                        inputTB.Text = openInputFile.FileName;

                        using (stream1)
                        {
                            // Show the length of the input data.
                            labelFSbytes.Text = stream1.Length.ToString();

                            // Initialise the byte buffer.
                            inputData = new byte[stream1.Length];

                            // Read data from the file into the buffer.
                            stream1.ReadAsync(inputData, 0, (int)stream1.Length);
                        }

                        //using (Stream outfile = File.OpenWrite(inputTB.Text + ".out"))
                        //{
                        //    outfile.Write(inputData, 0, inputData.Length);
                        //}
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to read file from disk. Error details: " + ex.Message);
                }
            }
        }
    }
}
