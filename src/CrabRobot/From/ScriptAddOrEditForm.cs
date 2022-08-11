using CrabRobot.Config;
using CrabRobot.From.Impl;
using CrabRobot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrabRobot.From
{
    public partial class ScriptAddOrEditForm : NoMinMaxButtonTopForm
    {
        private ScriptConfig scriptItemModel;
        //private DMSecret dMSecret;
        private ScriptAddOrEditForm()
        {
            InitializeComponent();
        }
        public ScriptAddOrEditForm(bool isEdit = false, ScriptConfig script = null) : this()
        {
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.scriptItemModel = script;
            if (isEdit)
            {
                this.Name = $"{this.scriptItemModel.ScriptName}-{this.scriptItemModel.PlayerName}-{(isEdit ? "编辑脚本" : "新增脚本")}";
                this.txtScriptName.Text = this.scriptItemModel.ScriptName;
                this.txtPlayerName.Text = this.scriptItemModel.PlayerName;
                this.txtAccount.Text = this.scriptItemModel.Account;
                this.txtPassword.Text = this.scriptItemModel.Password;
                this.txtSerialNumber.Text = this.scriptItemModel.SerialNumber;
                this.txtRestoreCode.Text = this.scriptItemModel.RestoreCode;
                this.txtGamePath.Text = this.scriptItemModel.GamePath;
                this.txtX.Text = this.scriptItemModel.X.ToString();
                this.txtY.Text = this.scriptItemModel.Y.ToString();
                this.txtWidth.Text = this.scriptItemModel.Width.ToString();
                this.txtHeight.Text = this.scriptItemModel.Height.ToString();
                this.checkBox1.Checked = this.scriptItemModel.NvidiaShow;
                this.cmbChildNumber.SelectedItem = this.scriptItemModel.SubAccount;
                this.txtGameDownLine.Text = this.scriptItemModel.GameDownLine.ToString();
                this.txtYaosaiOutLine.Text = this.scriptItemModel.YaosaiOutLine.ToString();
                this.txtMaxRetryLoginCount.Text = this.scriptItemModel.MaxRetryLoginCount.ToString();
                this.txtWaitTimeMinute.Text = this.scriptItemModel.WaitTimeMinute.ToString();
                this.dtpStartTime.Value=this.scriptItemModel.StartTime;
            }
            else
            {
                this.Name = $"新增脚本";
                this.scriptItemModel.ScriptId = Guid.NewGuid().ToString();
                this.txtScriptName.Text = this.scriptItemModel.ScriptName;
                this.txtPlayerName.Text = this.scriptItemModel.PlayerName;
                this.txtAccount.Text = this.scriptItemModel.Account;
                this.txtPassword.Text = this.scriptItemModel.Password;
                this.txtSerialNumber.Text = this.scriptItemModel.SerialNumber;
                this.txtRestoreCode.Text = this.scriptItemModel.RestoreCode;
                this.txtGamePath.Text = this.scriptItemModel.GamePath;
                this.txtX.Text = this.scriptItemModel.X.ToString();
                this.txtY.Text = this.scriptItemModel.Y.ToString();
                this.txtWidth.Text = this.scriptItemModel.Width.ToString();
                this.txtHeight.Text = this.scriptItemModel.Height.ToString();
                this.checkBox1.Checked = this.scriptItemModel.NvidiaShow;
                this.cmbChildNumber.SelectedItem = this.scriptItemModel.SubAccount;
                this.txtGameDownLine.Text = this.scriptItemModel.GameDownLine.ToString();
                this.txtYaosaiOutLine.Text = this.scriptItemModel.YaosaiOutLine.ToString();
                this.txtMaxRetryLoginCount.Text = this.scriptItemModel.MaxRetryLoginCount.ToString();
                this.txtWaitTimeMinute.Text = this.scriptItemModel.WaitTimeMinute.ToString();
                this.dtpStartTime.Value = this.scriptItemModel.StartTime;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.scriptItemModel.ScriptName = txtScriptName.Text;
            this.scriptItemModel.PlayerName = txtPlayerName.Text;
            this.scriptItemModel.Account = txtAccount.Text;
            this.scriptItemModel.Password = txtPassword.Text;
            this.scriptItemModel.SerialNumber = this.txtSerialNumber.Text;
            this.scriptItemModel.RestoreCode = this.txtRestoreCode.Text;
            this.scriptItemModel.GamePath = this.txtGamePath.Text;
            this.scriptItemModel.X = int.Parse(this.txtX.Text);
            this.scriptItemModel.Y = int.Parse(this.txtY.Text);
            this.scriptItemModel.Width = int.Parse(this.txtWidth.Text);
            this.scriptItemModel.Height = int.Parse(this.txtHeight.Text);
            this.scriptItemModel.NvidiaShow = this.checkBox1.Checked;
            this.scriptItemModel.SubAccount = this.cmbChildNumber.SelectedItem.ToString();
            this.scriptItemModel.GameDownLine = Convert.ToInt32(this.txtGameDownLine.Text);
            this.scriptItemModel.YaosaiOutLine = Convert.ToInt32(this.txtYaosaiOutLine.Text);
            this.scriptItemModel.MaxRetryLoginCount = Convert.ToInt32(this.txtMaxRetryLoginCount.Text);
            this.scriptItemModel.WaitTimeMinute = Convert.ToInt32(this.txtWaitTimeMinute.Text);
            this.scriptItemModel.StartTime = this.dtpStartTime.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
