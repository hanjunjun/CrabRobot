using HBNiuBi.Native;
using HBNiuBi.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.From.Impl
{
    public class HBNiuBiBaseForm : Form
    {
        public HBNiuBiBaseForm()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private Panel panel = new Panel();
        private PictureBox loading = new PictureBox();
        protected void StartLoading()
        {
            Action action = () =>
            {
                panel.Controls.Add(loading);
                panel.BackColor = Color.White;
                panel.Location = new Point(0, 0);
                panel.Name = "panel1";
                panel.Size = new Size(Width, Height);
                panel.TabIndex = 4;
                Controls.Add(panel);
                panel.Dock = DockStyle.Fill;

                //loading
                //Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "loading.gif");
                var img = Resources.loading;
                loading.Image = img;
                ((System.ComponentModel.ISupportInitialize)loading).BeginInit();
                loading.Name = "loading";
                loading.TabStop = true;
                loading.BackColor = Color.White;
                loading.Width = loading.Width;
                loading.Height = loading.Height;
                //loading.Location = new System.Drawing.Point((this.Size.Width - loading.Size.Width) / 2, (this.Size.Height - loading.Size.Height) / 2);
                //loading.Location = new System.Drawing.Point((this.Size.Width / 2 - loading.Size.Width / 2), (this.Size.Height / 2 - loading.Size.Height / 2));
                loading.SizeMode = PictureBoxSizeMode.CenterImage;
                loading.Dock = DockStyle.Fill;
                Controls.Add(loading);
                ((System.ComponentModel.ISupportInitialize)loading).EndInit();
                //loading.BringToFront();
                //显示在顶层
                panel.BringToFront();
                loading.BringToFront();
            };
            Invoke(action);
        }

        protected void EndLoading()
        {
            Action action = () =>
            {
                panel.Visible = false;
                loading.Visible = false;
                panel.Controls.Remove(loading);
                Controls.Remove(panel);
                //this.Validate();
            };
            Invoke(action);
        }

        
    }
}
