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
        public DataCompression()
        {
            InitializeComponent();
        }

        private void DataCompression_Load(object sender, EventArgs e)
        {

        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            Stream stream1 = null;

            if(openInputFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if((stream1 = openInputFile.OpenFile()) != null)
                    {
                        inputTB.Text = openInputFile.FileName;
                        using (stream1)
                        {
                            labelFSbytes.Text = stream1.Length.ToString();
                        }
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
