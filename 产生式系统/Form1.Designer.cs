
namespace 产生式系统
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rule_Column0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rule_Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rule_Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rule_Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(679, 80);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "正向推理";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(92, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "规则库";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(679, 246);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 62);
            this.button3.TabIndex = 5;
            this.button3.Text = "添加规则";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rule_Column0,
            this.rule_Column1,
            this.rule_Column2,
            this.rule_Column3});
            this.dataGridView1.Location = new System.Drawing.Point(44, 65);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(575, 424);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // rule_Column0
            // 
            this.rule_Column0.DataPropertyName = "规则编号";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rule_Column0.DefaultCellStyle = dataGridViewCellStyle1;
            this.rule_Column0.HeaderText = "规则编号";
            this.rule_Column0.MinimumWidth = 6;
            this.rule_Column0.Name = "rule_Column0";
            this.rule_Column0.ReadOnly = true;
            this.rule_Column0.Width = 60;
            // 
            // rule_Column1
            // 
            this.rule_Column1.DataPropertyName = "规则前件";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rule_Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.rule_Column1.HeaderText = "规则前件";
            this.rule_Column1.MinimumWidth = 6;
            this.rule_Column1.Name = "rule_Column1";
            this.rule_Column1.ReadOnly = true;
            this.rule_Column1.Width = 180;
            // 
            // rule_Column2
            // 
            this.rule_Column2.DataPropertyName = "规则后件";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rule_Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.rule_Column2.HeaderText = "规则后件";
            this.rule_Column2.MinimumWidth = 6;
            this.rule_Column2.Name = "rule_Column2";
            this.rule_Column2.ReadOnly = true;
            this.rule_Column2.Width = 120;
            // 
            // rule_Column3
            // 
            this.rule_Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rule_Column3.HeaderText = "";
            this.rule_Column3.MinimumWidth = 6;
            this.rule_Column3.Name = "rule_Column3";
            this.rule_Column3.ReadOnly = true;
            this.rule_Column3.Text = "删除";
            this.rule_Column3.UseColumnTextForButtonValue = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(679, 413);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 62);
            this.button4.TabIndex = 7;
            this.button4.Text = "退出";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 524);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产生式系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn rule_Column0;
        private System.Windows.Forms.DataGridViewTextBoxColumn rule_Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rule_Column2;
        private System.Windows.Forms.DataGridViewButtonColumn rule_Column3;
    }
}

