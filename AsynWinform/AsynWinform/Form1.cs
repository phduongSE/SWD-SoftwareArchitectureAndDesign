using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsynWinform
{
    public partial class Form1 : Form
    {
        public delegate void SetLabelEventHandler(string data);
        public event SetLabelEventHandler SendData;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++)
            {
                Form2 form2 = new Form2();
                form2.Show();
                SendData += form2.SetLabel;
            }

            Form3 form3 = new Form3();
            form3.Show();
            SendData += form3.ProcessMessage;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            SendData(txtResult.Text);
        }
    }
}
