using HBNiuBi.Model;
using HBNiuBi.ScriptTask;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.Controls
{
    public class ScriptTaskDataTable : DataGridView
    {
        private bool _CellColorOnchange = false;
        private Color cell_color = Color.Yellow;
        private bool shifouhuasanjiao = true;
        private Color color_grid = Color.FromArgb(236, 233, 216);
        bool click = false;
        public ScriptTaskDataTable()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        protected override void OnCreateControl()
        {
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(236, 233, 216);
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            //this.ColumnHeadersHeight = 20;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.RowHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(236, 233, 216);
            this.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
            this.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //this.GridColor = Color.Silver;//表格点击后颜色  表格线颜色
            this.BackgroundColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AllowUserToOrderColumns = true;
            this.AutoGenerateColumns = true;
            base.OnCreateControl();
        }
        Color defaultcolor;
        //移到单元格时的颜色
        protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseMove(e);
            try
            {
                if (_CellColorOnchange)
                    Rows[e.RowIndex].DefaultCellStyle.BackColor = cell_color;
            }
            catch (Exception)
            {
            }
        }
        //进入单元格时保存当前的颜色
        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseEnter(e);
            try
            {
                if (_CellColorOnchange)
                    defaultcolor = Rows[e.RowIndex].DefaultCellStyle.BackColor;
            }
            catch (Exception)
            {
            }
        }
        //离开时还原颜色
        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseLeave(e);
            try
            {
                if (_CellColorOnchange)
                    Rows[e.RowIndex].DefaultCellStyle.BackColor = defaultcolor;
            }
            catch (Exception)
            {
            }
        }
        public bool CellColorOnchange
        {
            get
            {
                return _CellColorOnchange;
            }
            set
            {
                _CellColorOnchange = value;
            }
        }
        public Color DefaultcolorSet
        {
            get
            {
                return cell_color;
            }
            set
            {
                cell_color = value;
            }
        }
        public bool Shifouhua_Sanjiao
        {
            get
            {
                return shifouhuasanjiao;
            }
            set
            {
                shifouhuasanjiao = value;
            }
        }
        public Color Content_Grid_color
        {
            get
            {
                return color_grid;
            }
            set
            {
                color_grid = value;
            }
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            //this.RowTemplate.Height = 17;

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
        //RowPostPaint
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(
                     this.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(
                        System.Globalization.CultureInfo.CurrentCulture),
                        this.DefaultCellStyle.Font
                        , b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
            base.OnRowPostPaint(e);
        }


        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            if (shifouhuasanjiao)
            {
                using (SolidBrush b = new SolidBrush(Color.Black))
                {
                    Image image = global::HBNiuBi.Properties.Resources.全选;
                    //e.Graphics.DrawString("►", e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
                    //e.Graphics.DrawImageUnscaled(image, e.RowBounds.Location.X + 1, e.RowBounds.Location.Y + 2, 8, 13);
                }
            }
            base.OnRowPrePaint(e);
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && this.CurrentRow.Index == e.RowIndex)
            {
                click = true;
            }
            base.OnCellClick(e);
        }
        public class DoubleClickEventArgs : EventArgs
        {
            public ScriptTaskManager ScriptModel;

            /// <summary>
            /// Default constructor
            /// </summary>
            public DoubleClickEventArgs(ScriptTaskManager scriptModel)
                : base()
            {
                this.ScriptModel = scriptModel;
            }
        }
        public new event DataTableDoubleClickHandler DoubleClick;
        public delegate void DataTableDoubleClickHandler(object source, DoubleClickEventArgs args, DataGridViewCellEventArgs e);
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);
            var scriptTaskManager = ScriptTaskSchedulerExecutor.GetInstance().GetScriptTaskManagerById(this.CurrentRow.Tag?.ToString());
            DoubleClick(this, new DoubleClickEventArgs(scriptTaskManager), e);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            SolidBrush b = new SolidBrush(Color.FromArgb(236, 233, 216));
            Pen whitePen = new Pen(color_grid, 1);
            if (e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.Gray,
                    Color.Gray, LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(b, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.RowIndex == -1)
            {
                //标题行
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.Silver,
                    Color.Silver, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(b, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Silver, border);
                    //e.Graphics.DrawRectangle(Pens.Black, border.X + 1, border.Y + 1, border.Width - 1, border.Height - 1);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.ColumnIndex == -1)
            {
                //标题列
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.Silver,
                    Color.Silver, LinearGradientMode.Horizontal))
                {

                    e.Graphics.FillRectangle(b, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Silver, border);
                    //e.Graphics.DrawRectangle(Pens.Black, border.X+1,border.Y+1,border.Width-1,border.Height-1);
                    e.Graphics.DrawString("△", Font, b, e.CellBounds.X, e.CellBounds.Y);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else
            {
                //Color.FromArgb(193, 193, 193)
                Rectangle border = e.CellBounds;
                border.Offset(new Point(-1, -1));

                e.Graphics.DrawRectangle(whitePen, border);
            }
        }
    }
}
