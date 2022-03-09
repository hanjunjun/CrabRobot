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
        private ScriptAddOrEditForm()
        {
            InitializeComponent();
        }
        public ScriptAddOrEditForm(bool isEdit = false, ScriptItemModel script = null) : this()
        {
            if (isEdit)
            {
                this.Name = $"{script.ScriptName}-{script.PlayerName}-{(isEdit ? "编辑脚本" : "新增脚本")}";
                this.txtScriptName.Text = script.ScriptName;
                this.txtPlayerName.Text = script.PlayerName;
                this.txtAccount.Text = script.Account;
                this.txtPassword.Text = script.Password;
            }
            else
            {
                this.Name = $"新增脚本";
            }
            
            InitializeComponent();
        }
    }
}
