using HBNiuBi.From;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dataGridView1.ForeColor = Color.Blue;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            // 禁止用户改变列头的高度   
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //加按钮
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //btn.Name = "RetryCheck";//添加按钮的名字
            //btn.HeaderText = "操作";//添加按钮列的列名称
            //btn.DefaultCellStyle.NullValue = "重新检查";//添加按钮显示的名字
            //dataGridView1.Columns.Add(btn);//在dataGridView2的最后一列添加按钮
            //列颜色
            dataGridView1.EnableHeadersVisualStyles = false;//需要
            dataGridView1.Columns[0].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[1].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[2].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[3].HeaderCell.Style.ForeColor = Color.Blue;
            dataGridView1.Columns[4].HeaderCell.Style.ForeColor = Color.Blue;
            //添加测试内容
            foreach(var item in new string[] { "张三","李四"})
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = item;
                this.dataGridView1.Rows[index].Cells[1].Value = item;
                this.dataGridView1.Rows[index].Cells[2].Value = item;
                this.dataGridView1.Rows[index].Cells[3].Value = item;
                this.dataGridView1.Rows[index].Cells[4].Value = item;
                dataGridView1.MultiSelect = false;
                // dgvRecView.Rows[dgvRecView.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
                dataGridView1.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
            }
            //单元格内容居中
            //foreach (DataGridViewColumn item in this.dataGridView1.Columns)
            //{
            //    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    //列标题右边有预留一个排序小箭头的位置，所以整个列标题就向左边多一点，而当把SortMode属性设置为NotSortable时，不使用排序，也就没有那个预留的位置，所有完全居中了

            //}
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(
                     dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(
                        System.Globalization.CultureInfo.CurrentCulture),
                        dataGridView1.DefaultCellStyle.Font
                        , b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm();
            scriptAddOrEditForm.ShowDialog();
        }
    }
}
