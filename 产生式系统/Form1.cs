using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 产生式系统
{
    public partial class Form1 : Form
    {
        public static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            method.Init();
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 F = new Form2();
            F.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_rule f = new Add_rule();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定退出程序?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (dataGridView1.Columns[e.ColumnIndex] == rule_Column3)
                if (MessageBox.Show("是否确定要删除这条规则", "确认！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int i = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    for (rulenode rnode_temp = method.rhead; rnode_temp.next != null; rnode_temp = rnode_temp.next)
                    {
                        if (rnode_temp.next.rulecount == i)
                        {
                            rnode_temp.next = rnode_temp.next.next;
                            break;
                        }
                    }
                    method.RewriteFile();
                }
        }
    }
}
