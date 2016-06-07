namespace WebConfigForm
{
    partial class MainFrom
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dropDataType = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.lblLoginText = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblDbname = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.dropBb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comExecType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dropDataType
            // 
            this.dropDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDataType.FormattingEnabled = true;
            this.dropDataType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dropDataType.Items.AddRange(new object[] {
            "Ass",
            "Sql",
            "Oracle"});
            this.dropDataType.Location = new System.Drawing.Point(107, 16);
            this.dropDataType.Name = "dropDataType";
            this.dropDataType.Size = new System.Drawing.Size(121, 20);
            this.dropDataType.TabIndex = 0;
            this.dropDataType.SelectedIndexChanged += new System.EventHandler(this.dropDataType_SelectedIndexChanged);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(24, 19);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(77, 12);
            this.lblDataType.TabIndex = 1;
            this.lblDataType.Text = "数据库类型：";
            // 
            // lblLoginText
            // 
            this.lblLoginText.AutoSize = true;
            this.lblLoginText.Location = new System.Drawing.Point(36, 99);
            this.lblLoginText.Name = "lblLoginText";
            this.lblLoginText.Size = new System.Drawing.Size(65, 12);
            this.lblLoginText.TabIndex = 1;
            this.lblLoginText.Text = "登陆账号：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(108, 95);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.TabIndex = 3;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(36, 126);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(65, 12);
            this.lblPwd.TabIndex = 1;
            this.lblPwd.Text = "登陆密码：";
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(108, 123);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Size = new System.Drawing.Size(120, 21);
            this.txtPWD.TabIndex = 4;
            // 
            // lblDbname
            // 
            this.lblDbname.AutoSize = true;
            this.lblDbname.Location = new System.Drawing.Point(24, 72);
            this.lblDbname.Name = "lblDbname";
            this.lblDbname.Size = new System.Drawing.Size(77, 12);
            this.lblDbname.TabIndex = 1;
            this.lblDbname.Text = "数据库名称：";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(108, 69);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(120, 21);
            this.txtDBName.TabIndex = 2;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(25, 241);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(202, 47);
            this.btnSet.TabIndex = 6;
            this.btnSet.Text = "生成";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(107, 42);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(120, 21);
            this.txtIP.TabIndex = 1;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(23, 45);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(77, 12);
            this.lblIp.TabIndex = 5;
            this.lblIp.Text = "数据库地址：";
            // 
            // dropBb
            // 
            this.dropBb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropBb.FormattingEnabled = true;
            this.dropBb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dropBb.Items.AddRange(new object[] {
            "Microsoft.Jet.OLEDB.4.0",
            "Microsoft.ACE.OLEDB.12.0",
            "Microsoft.ACE.OLEDB.12.0;jet oledb"});
            this.dropBb.Location = new System.Drawing.Point(106, 150);
            this.dropBb.Name = "dropBb";
            this.dropBb.Size = new System.Drawing.Size(121, 20);
            this.dropBb.TabIndex = 5;
            this.dropBb.SelectedIndexChanged += new System.EventHandler(this.dropDataType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库版本：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "WEBUIBase",
            "FrmUIBase",
            "AjaxUIBase"});
            this.comboBox1.Location = new System.Drawing.Point(106, 176);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(122, 20);
            this.comboBox1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "显示UITypeName";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "操作类型：";
            // 
            // comExecType
            // 
            this.comExecType.FormattingEnabled = true;
            this.comExecType.Items.AddRange(new object[] {
            "MDB",
            "JSON"});
            this.comExecType.Location = new System.Drawing.Point(105, 202);
            this.comExecType.Name = "comExecType";
            this.comExecType.Size = new System.Drawing.Size(122, 20);
            this.comExecType.TabIndex = 7;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 300);
            this.Controls.Add(this.comExecType);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.lblDbname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLoginText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.dropBb);
            this.Controls.Add(this.dropDataType);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrom";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox dropDataType;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.Label lblLoginText;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblDbname;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.ComboBox dropBb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comExecType;
    }
}

