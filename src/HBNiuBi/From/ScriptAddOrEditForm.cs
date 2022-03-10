using HBNiuBi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.From
{
    public partial class ScriptAddOrEditForm : HBNiuBiBaseForm
    {
        private ScriptItemModel scriptItemModel;
        private DMSecret dMSecret;
        private ScriptAddOrEditForm()
        {
            InitializeComponent();
        }
        public ScriptAddOrEditForm(bool isEdit = false, ScriptItemModel script = null, DMSecret dMSecret = null) : this()
        {
            this.scriptItemModel = script;
            this.dMSecret = dMSecret;
            if (isEdit)
            {
                this.Name = $"{this.scriptItemModel.ScriptName}-{this.scriptItemModel.PlayerName}-{(isEdit ? "编辑脚本" : "新增脚本")}";
                this.txtScriptName.Text = this.scriptItemModel.ScriptName;
                this.txtPlayerName.Text = this.scriptItemModel.PlayerName;
                this.txtAccount.Text = this.scriptItemModel.Account;
                this.txtPassword.Text = this.scriptItemModel.Password;
                this.txtSerialNumber.Text = this.scriptItemModel.SerialNumber;
                this.txtRestoreCode.Text = this.scriptItemModel.RestoreCode;
            }
            else
            {
                this.Name = $"新增脚本";
                this.scriptItemModel.ScriptId = Guid.NewGuid().ToString();
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
            this.dMSecret.Code = txtDmCode.Text;
            this.dMSecret.Ver = txtDmVer.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
