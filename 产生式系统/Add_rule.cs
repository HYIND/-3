using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 产生式系统
{
    public partial class Add_rule : Form
    {
        public Add_rule()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string t1 = textBox1.Text;
            string t2 = textBox2.Text;

            if (method.Addrule(t1, t2))
            {
                MessageBox.Show("添加成功！");
                    this.Close();
            }
        }
    }
}
