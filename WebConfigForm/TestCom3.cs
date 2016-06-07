using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace WebConfigForm
{
    public partial class TestCom3 : Form
    {
        public TestCom3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SerialPort com = new SerialPort(txtComText.Text, 9600, Parity.Even, 1, StopBits.One);
            if (com.IsOpen)
            { com.Close(); }

            try
            {
                com.Open();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            if (com.IsOpen)
            { com.Close(); }
            MessageBox.Show("连接成功！");
        }
    }
}
