using GetUnItems.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetUnItems
{
    public partial class Form1 : Form
    {
        private DFS xixi;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            xixi = new DFS("C:\\Users\\magic\\Documents\\Git\\unturned-Chinese\\Items");
            xixi.Search();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("111");
            }

        }

    }
}
