namespace HBNiuBi.From
{
    partial class ToolSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolSettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbDebug = new System.Windows.Forms.CheckBox();
            this.txtDmVer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDmCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSaveDayLine = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.button5 = new System.Windows.Forms.Button();
            this.ckbEnableVideo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVideSavePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSendMail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMailServerPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTestReceiveMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtMailServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMailPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbDebug);
            this.groupBox1.Controls.Add(this.txtDmVer);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDmCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 137);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DM组件设置";
            // 
            // ckbDebug
            // 
            this.ckbDebug.AutoSize = true;
            this.ckbDebug.Location = new System.Drawing.Point(140, 96);
            this.ckbDebug.Name = "ckbDebug";
            this.ckbDebug.Size = new System.Drawing.Size(123, 21);
            this.ckbDebug.TabIndex = 14;
            this.ckbDebug.Text = "是否启用调试模式";
            this.ckbDebug.UseVisualStyleBackColor = true;
            // 
            // txtDmVer
            // 
            this.txtDmVer.Location = new System.Drawing.Point(139, 51);
            this.txtDmVer.Name = "txtDmVer";
            this.txtDmVer.Size = new System.Drawing.Size(263, 23);
            this.txtDmVer.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "DmVer：";
            // 
            // txtDmCode
            // 
            this.txtDmCode.Location = new System.Drawing.Point(139, 22);
            this.txtDmCode.Name = "txtDmCode";
            this.txtDmCode.Size = new System.Drawing.Size(263, 23);
            this.txtDmCode.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "DmCode：";
            // 
            // button1
            // 
            this.button1.Image = global::HBNiuBi.Properties.Resources.保存;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(140, 605);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 38);
            this.button1.TabIndex = 8;
            this.button1.Text = "保存";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::HBNiuBi.Properties.Resources.取消;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(284, 605);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 38);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmbSaveDayLine);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dtpEnd);
            this.groupBox2.Controls.Add(this.dtpStart);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.ckbEnableVideo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtVideSavePath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 208);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "录像设置";
            // 
            // button4
            // 
            this.button4.Image = global::HBNiuBi.Properties.Resources.清理缓存;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(139, 153);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 38);
            this.button4.TabIndex = 21;
            this.button4.Text = "清理录像";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(202, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 17);
            this.label12.TabIndex = 20;
            this.label12.Text = "天以内的录像";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(89, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 19;
            this.label11.Text = "只保存";
            // 
            // cmbSaveDayLine
            // 
            this.cmbSaveDayLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaveDayLine.FormattingEnabled = true;
            this.cmbSaveDayLine.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cmbSaveDayLine.Location = new System.Drawing.Point(139, 95);
            this.cmbSaveDayLine.Name = "cmbSaveDayLine";
            this.cmbSaveDayLine.Size = new System.Drawing.Size(57, 25);
            this.cmbSaveDayLine.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(202, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 17);
            this.label10.TabIndex = 17;
            this.label10.Text = "----";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "HH";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(236, 66);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(57, 23);
            this.dtpEnd.TabIndex = 16;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "HH";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(139, 66);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(57, 23);
            this.dtpStart.TabIndex = 15;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button5.Image = global::HBNiuBi.Properties.Resources.打开文件夹x16;
            this.button5.Location = new System.Drawing.Point(408, 35);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(38, 23);
            this.button5.TabIndex = 14;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // ckbEnableVideo
            // 
            this.ckbEnableVideo.AutoSize = true;
            this.ckbEnableVideo.Location = new System.Drawing.Point(139, 126);
            this.ckbEnableVideo.Name = "ckbEnableVideo";
            this.ckbEnableVideo.Size = new System.Drawing.Size(123, 21);
            this.ckbEnableVideo.TabIndex = 13;
            this.ckbEnableVideo.Text = "是否启用屏幕录像";
            this.ckbEnableVideo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "时间段：";
            // 
            // txtVideSavePath
            // 
            this.txtVideSavePath.Location = new System.Drawing.Point(139, 35);
            this.txtVideSavePath.Name = "txtVideSavePath";
            this.txtVideSavePath.Size = new System.Drawing.Size(263, 23);
            this.txtVideSavePath.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "录像存储位置：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSendMail);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtMailServerPort);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtTestReceiveMail);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.txtMailServer);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtMailPassword);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(13, 369);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(501, 218);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "邮件服务器设置";
            // 
            // txtSendMail
            // 
            this.txtSendMail.Location = new System.Drawing.Point(139, 80);
            this.txtSendMail.Name = "txtSendMail";
            this.txtSendMail.Size = new System.Drawing.Size(263, 23);
            this.txtSendMail.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "发件人邮箱：";
            // 
            // txtMailServerPort
            // 
            this.txtMailServerPort.Location = new System.Drawing.Point(139, 51);
            this.txtMailServerPort.Name = "txtMailServerPort";
            this.txtMailServerPort.Size = new System.Drawing.Size(263, 23);
            this.txtMailServerPort.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "服务器端口：";
            // 
            // txtTestReceiveMail
            // 
            this.txtTestReceiveMail.Location = new System.Drawing.Point(139, 138);
            this.txtTestReceiveMail.Name = "txtTestReceiveMail";
            this.txtTestReceiveMail.Size = new System.Drawing.Size(263, 23);
            this.txtTestReceiveMail.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "测试收件人邮箱：";
            // 
            // button3
            // 
            this.button3.Image = global::HBNiuBi.Properties.Resources.发送邮件模板;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(139, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 38);
            this.button3.TabIndex = 12;
            this.button3.Text = "发送邮件测试";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtMailServer
            // 
            this.txtMailServer.Location = new System.Drawing.Point(139, 22);
            this.txtMailServer.Name = "txtMailServer";
            this.txtMailServer.Size = new System.Drawing.Size(263, 23);
            this.txtMailServer.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "发件服务器：";
            // 
            // txtMailPassword
            // 
            this.txtMailPassword.Location = new System.Drawing.Point(139, 109);
            this.txtMailPassword.Name = "txtMailPassword";
            this.txtMailPassword.Size = new System.Drawing.Size(263, 23);
            this.txtMailPassword.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "密码：";
            // 
            // ToolSettingsForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(526, 668);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToolSettingsForm";
            this.Text = "DM配置-重启生效";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtDmVer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDmCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVideSavePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtMailServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMailPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMailServerPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTestReceiveMail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSendMail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbEnableVideo;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSaveDayLine;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox ckbDebug;
    }
}