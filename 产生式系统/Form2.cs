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
    public partial class Form2 : Form
    {
        public static Form2 form2;
        public Form2()
        {
            InitializeComponent();
            form2 = this;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("命题编号");
            dt.Columns.Add("命题");
            for (eventnode enode_temp = method.ehead;
                enode_temp.next != null; enode_temp = enode_temp.next)
            {
                DataRow row = dt.NewRow();
                row[0] = enode_temp.next.eventcount;
                row[1] = enode_temp.next.name;
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 100;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == "true")
                {
                    bool signal_repeat = true;
                    for (int n = 0; n < dataGridView2.Rows.Count; n++)
                    {
                        if (dataGridView2.Rows[n].Cells[0].Value == dataGridView1.Rows[i].Cells[1].Value)
                            signal_repeat = false;
                    }
                    if (signal_repeat)
                        dataGridView2.Rows.Add(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定要清空?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dataGridView2.Rows.Clear();
                textBox1.Text = string.Empty;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                eventnode event_selected_head = new eventnode();
                method.Reasoning(event_selected_head);
            }
            else{
                textBox1.Text = "未添加选项！";
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex] == Selected_Column2)
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            textBox1.Text = string.Empty;
        }
    }
}
