using HBNiuBi.From;
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
using static HBNiuBi.Controls.ScriptTaskDataTable;

namespace HBNiuBi
{
    public partial class MainForm : HBNiuBiBaseForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleClick += DataTableDoubleClickHandler;
            //dataGridView1.ForeColor = Color.Blue;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            // 禁止用户改变列头的高度   
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //列颜色
            dataGridView1.EnableHeadersVisualStyles = false;//需要
            dataGridView1.Columns[0].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[1].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[2].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[3].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[4].HeaderCell.Style.ForeColor = Color.Blue;
            //添加测试内容
            //this.dataGridView1.Rows.Clear();
            //foreach (var item in new string[] { "张三", "李四" })
            //{
            //    int index = this.dataGridView1.Rows.Add();
            //    var taskId = Guid.NewGuid().ToString();
            //    this.dataGridView1.Rows[index].Tag = taskId;
            //    this.dataGridView1.Rows[index].Cells[0].Value = item;
            //    this.dataGridView1.Rows[index].Cells[1].Value = item;
            //    this.dataGridView1.Rows[index].Cells[2].Value = item;
            //    this.dataGridView1.Rows[index].Cells[3].Value = item;
            //    this.dataGridView1.Rows[index].Cells[4].Value = item;
            //    //ScriptContainer.ScriptTaskContainer.TryAdd(taskId,new ScriptModel(new ScriptItemModel(),new DMSecret()));
            //    //dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
            //}
            //单元格内容居中
            foreach (DataGridViewColumn item in this.dataGridView1.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
                //列标题右边有预留一个排序小箭头的位置，所以整个列标题就向左边多一点，
                //而当把SortMode属性设置为NotSortable时，不使用排序，也就没有那个预留的位置，所有完全居中了
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dMSecret = new DMSecret();
            var scriptItemModel = new ScriptItemModel();
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm(false, scriptItemModel, dMSecret);
            if (scriptAddOrEditForm.ShowDialog() == DialogResult.OK)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Tag = scriptItemModel.ScriptId;
                this.dataGridView1.Rows[index].Cells[0].Value = scriptItemModel.ScriptName;
                this.dataGridView1.Rows[index].Cells[1].Value = scriptItemModel.Account;
                this.dataGridView1.Rows[index].Cells[2].Value = scriptItemModel.Password;
                this.dataGridView1.Rows[index].Cells[3].Value = scriptItemModel.PlayerName;
                this.dataGridView1.Rows[index].Cells[4].Value = scriptItemModel.Status;
                ScriptContainer.ScriptTaskContainer.TryAdd(scriptItemModel.ScriptId, new ScriptModel(scriptItemModel, dMSecret));
            }
        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            DataGridViewRow dr = this.dataGridView1.CurrentRow;
            if (dr != null)
            {
                var checkContent = dr.Cells[0].Value.ToString();
                var checkResultContent = dr.Cells[1].Value.ToString();

                ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm();
                scriptAddOrEditForm.ShowDialog();
            }
        }

        private void DataTableDoubleClickHandler(object source, DoubleClickEventArgs args, DataGridViewCellEventArgs e)
        {
            var dMSecret = args.ScriptModel.dMSecret;
            var scriptItemModel = args.ScriptModel.scriptItemModel;
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm(true, scriptItemModel, dMSecret);
            if (scriptAddOrEditForm.ShowDialog() == DialogResult.OK)
            {
                int index = e.RowIndex;
                this.dataGridView1.Rows[index].Tag = scriptItemModel.ScriptId;
                this.dataGridView1.Rows[index].Cells[0].Value = scriptItemModel.ScriptName;
                this.dataGridView1.Rows[index].Cells[1].Value = scriptItemModel.Account;
                this.dataGridView1.Rows[index].Cells[2].Value = scriptItemModel.Password;
                this.dataGridView1.Rows[index].Cells[3].Value = scriptItemModel.PlayerName;
                this.dataGridView1.Rows[index].Cells[4].Value = scriptItemModel.Status;
                if (!ScriptContainer.ScriptTaskContainer.TryGetValue(scriptItemModel.ScriptId, out var outValue))
                {
                    throw new Exception($"未能从字典中获取{scriptItemModel.ScriptName}的值！");
                }
                if (!ScriptContainer.ScriptTaskContainer.TryUpdate(scriptItemModel.ScriptId, new ScriptModel(scriptItemModel, dMSecret), outValue))
                {
                    throw new Exception($"更新{scriptItemModel.ScriptName}的字典值失败！");
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                var taskId = dataGridView1.SelectedRows[i].Tag.ToString();
                ScriptContainer.ScriptTaskContainer[taskId].Start();
                ScriptContainer.ScriptTaskContainer[taskId].SetScriptAction(() =>
                {
                    //执行脚本
                });

            }
        }
    }
}
