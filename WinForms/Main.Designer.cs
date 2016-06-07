namespace WinForms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnSet;
            System.Windows.Forms.Button btnSuaxin;
            System.Windows.Forms.Button button1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.chkTableList = new System.Windows.Forms.CheckedListBox();
            this.dgidTableMessage = new System.Windows.Forms.DataGridView();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.skinStyle = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            btnSet = new System.Windows.Forms.Button();
            btnSuaxin = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgidTableMessage)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            btnSet.Location = new System.Drawing.Point(559, 62);
            btnSet.Margin = new System.Windows.Forms.Padding(2);
            btnSet.Name = "btnSet";
            btnSet.Size = new System.Drawing.Size(58, 21);
            btnSet.TabIndex = 3;
            btnSet.Text = "生成";
            btnSet.UseVisualStyleBackColor = true;
            btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnSuaxin
            // 
            btnSuaxin.Location = new System.Drawing.Point(447, 19);
            btnSuaxin.Margin = new System.Windows.Forms.Padding(2);
            btnSuaxin.Name = "btnSuaxin";
            btnSuaxin.Size = new System.Drawing.Size(58, 21);
            btnSuaxin.TabIndex = 3;
            btnSuaxin.Text = "刷新";
            btnSuaxin.UseVisualStyleBackColor = true;
            btnSuaxin.Click += new System.EventHandler(this.btnSuaxin_Click);
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(520, 19);
            button1.Margin = new System.Windows.Forms.Padding(2);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(58, 21);
            button1.TabIndex = 3;
            button1.Text = "连接";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.btnSuaxin_Click);
            // 
            // chkTableList
            // 
            this.chkTableList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTableList.CheckOnClick = true;
            this.chkTableList.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkTableList.FormattingEnabled = true;
            this.chkTableList.Location = new System.Drawing.Point(0, 0);
            this.chkTableList.Margin = new System.Windows.Forms.Padding(0);
            this.chkTableList.Name = "chkTableList";
            this.chkTableList.Size = new System.Drawing.Size(180, 559);
            this.chkTableList.TabIndex = 0;
            this.chkTableList.SelectedIndexChanged += new System.EventHandler(this.chkTableList_SelectedIndexChanged);
            // 
            // dgidTableMessage
            // 
            this.dgidTableMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgidTableMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgidTableMessage.Location = new System.Drawing.Point(3, 3);
            this.dgidTableMessage.Margin = new System.Windows.Forms.Padding(2);
            this.dgidTableMessage.Name = "dgidTableMessage";
            this.dgidTableMessage.ReadOnly = true;
            this.dgidTableMessage.RowTemplate.Height = 27;
            this.dgidTableMessage.ShowEditingIcon = false;
            this.dgidTableMessage.ShowRowErrors = false;
            this.dgidTableMessage.Size = new System.Drawing.Size(634, 428);
            this.dgidTableMessage.TabIndex = 1;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(248, 19);
            this.txtNamespace.Margin = new System.Windows.Forms.Padding(2);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(180, 21);
            this.txtNamespace.TabIndex = 2;
            this.txtNamespace.Text = "Models";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "命名空间：";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(248, 62);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(307, 21);
            this.txtFileName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "生成位置：";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(183, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 460);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgidTableMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(640, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "业务代码";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(634, 428);
            this.textBox1.TabIndex = 0;
            // 
            // skinStyle
            // 
            this.skinStyle.SerialNumber = "";
            this.skinStyle.SkinFile = null;
            this.skinStyle.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinStyle.SkinStreamMain")));
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(button1);
            this.Controls.Add(btnSuaxin);
            this.Controls.Add(btnSet);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.chkTableList);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgidTableMessage)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkTableList;
        private System.Windows.Forms.DataGridView dgidTableMessage;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private Sunisoft.IrisSkin.SkinEngine skinStyle;
    }
}