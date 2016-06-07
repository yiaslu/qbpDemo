namespace WinForms
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.skinStyle = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.comService = new System.Windows.Forms.ComboBox();
            this.comLoginName = new System.Windows.Forms.ComboBox();
            this.comLoginPwd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comDataBase = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lolMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skinStyle
            // 
            this.skinStyle.SerialNumber = "";
            this.skinStyle.SkinFile = null;
            this.skinStyle.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinStyle.SkinStreamMain")));
            // 
            // comService
            // 
            this.comService.FormattingEnabled = true;
            this.comService.Location = new System.Drawing.Point(70, 12);
            this.comService.Margin = new System.Windows.Forms.Padding(2);
            this.comService.Name = "comService";
            this.comService.Size = new System.Drawing.Size(142, 20);
            this.comService.TabIndex = 0;
            this.comService.SelectedIndexChanged += new System.EventHandler(this.comService_SelectedIndexChanged);
            this.comService.Leave += new System.EventHandler(this.combo_TextChanged);
            // 
            // comLoginName
            // 
            this.comLoginName.FormattingEnabled = true;
            this.comLoginName.Location = new System.Drawing.Point(70, 46);
            this.comLoginName.Margin = new System.Windows.Forms.Padding(2);
            this.comLoginName.Name = "comLoginName";
            this.comLoginName.Size = new System.Drawing.Size(142, 20);
            this.comLoginName.TabIndex = 1;
            this.comLoginName.SelectedIndexChanged += new System.EventHandler(this.comLoginName_SelectedIndexChanged);
            this.comLoginName.Leave += new System.EventHandler(this.combo_TextChanged);
            // 
            // comLoginPwd
            // 
            this.comLoginPwd.FormattingEnabled = true;
            this.comLoginPwd.Location = new System.Drawing.Point(70, 81);
            this.comLoginPwd.Margin = new System.Windows.Forms.Padding(2);
            this.comLoginPwd.Name = "comLoginPwd";
            this.comLoginPwd.Size = new System.Drawing.Size(142, 20);
            this.comLoginPwd.TabIndex = 2;
            this.comLoginPwd.Leave += new System.EventHandler(this.combo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "服务器：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "登陆名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "　密码：";
            // 
            // comDataBase
            // 
            this.comDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDataBase.FormattingEnabled = true;
            this.comDataBase.Location = new System.Drawing.Point(70, 115);
            this.comDataBase.Margin = new System.Windows.Forms.Padding(2);
            this.comDataBase.Name = "comDataBase";
            this.comDataBase.Size = new System.Drawing.Size(142, 20);
            this.comDataBase.TabIndex = 3;
            this.comDataBase.SelectedIndexChanged += new System.EventHandler(this.comDataBase_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据库：";
            // 
            // btnLogin
            // 
            this.btnLogin.Enabled = false;
            this.btnLogin.Location = new System.Drawing.Point(32, 164);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(169, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lolMessage
            // 
            this.lolMessage.AutoSize = true;
            this.lolMessage.Location = new System.Drawing.Point(42, 147);
            this.lolMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lolMessage.Name = "lolMessage";
            this.lolMessage.Size = new System.Drawing.Size(0, 12);
            this.lolMessage.TabIndex = 5;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 214);
            this.Controls.Add(this.lolMessage);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comDataBase);
            this.Controls.Add(this.comLoginPwd);
            this.Controls.Add(this.comLoginName);
            this.Controls.Add(this.comService);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Sunisoft.IrisSkin.SkinEngine skinStyle;
        private System.Windows.Forms.ComboBox comService;
        private System.Windows.Forms.ComboBox comLoginName;
        private System.Windows.Forms.ComboBox comLoginPwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comDataBase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lolMessage;
    }
}