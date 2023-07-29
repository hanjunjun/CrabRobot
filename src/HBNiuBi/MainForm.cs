using HBNiuBi.Config;
using HBNiuBi.DM;
using HBNiuBi.From;
using HBNiuBi.From.Impl;
using HBNiuBi.Model;
using HBNiuBi.ScriptTask;
using HBNiuBi.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HBNiuBi.Controls.ScriptTaskDataTable;

namespace HBNiuBi
{
    public partial class MainForm : TopForm
    {
        private Dmsoft DM;
        public MainForm()
        {
            InitializeComponent();

        }
        private void LoadDM(DMSecret dMSecret)
        {
            DmDynamicLoad.LoadDmDll();
            DM = new Dmsoft();
            var path = AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName + ".exe";
            var result = DM.Reg(dMSecret.Code, dMSecret.Ver);
            if (result != 1)
            {
                throw new Exception("DM注册失败！");
            }
            result = DM.DmGuard(1, "memory2");
            if (result != 1)
            {
                throw new Exception("memory2 启动失败！");
            }
            result = DM.DmGuard(1, "hm 0 1");
            if (result != 1)
            {
                throw new Exception("hm 0 1 启动失败！");
            }
            var dun = @$"f2 <c:\windows\system32\calc.exe> <{path}>";
            result = DM.DmGuard(1, dun);
            //调试模式
            if (dMSecret.Debug)
            {
                DM.SetShowErrorMsg(0);
                Action action = () =>
                {
                    TestForm test = new TestForm();
                    test.Show();
                };
                this.Invoke(action);

            }
        }
        int width = 0;
        private void InitDataGrid()
        {
            dataGridView1.InvokeDataGridView(() =>
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
                //单元格内容居中
                var i = 0;
                foreach (DataGridViewColumn item in this.dataGridView1.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //列标题右边有预留一个排序小箭头的位置，所以整个列标题就向左边多一点，
                    //而当把SortMode属性设置为NotSortable时，不使用排序，也就没有那个预留的位置，所有完全居中了
                    this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                    //记录整个DataGridView的宽度
                    width += this.dataGridView1.Columns[i].Width;
                    i++;
                }
                //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
                //则将DataGridView的列自动调整模式设置为显示的列即可，
                //如果是小于原来设定的宽度，将模式改为填充。
                if (width > this.dataGridView1.Size.Width)
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                else
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                //冻结某列 从左开始 0，1，2
                dataGridView1.Columns[1].Frozen = true;
            });
        }
        private void DeleteGameCache(List<ScriptConfig> scriptConfigs)
        {
            if (Process.GetProcessesByName("Wow").Count() == 0)
            {
                foreach (var item in scriptConfigs)
                {
                    var path = Path.GetDirectoryName(item.GamePath);
                    var cachePath = path + @"\Cache";
                    if (Directory.Exists(cachePath))
                    {
                        FileUtil.DeleteDirectory(cachePath);
                    }
                    var errorPath = path + @"\Errors";
                    if (Directory.Exists(errorPath))
                    {
                        FileUtil.DeleteDirectory(errorPath);
                    }
                    var logsPath = path + @"\Logs";
                    if (Directory.Exists(logsPath))
                    {
                        FileUtil.DeleteDirectory(logsPath);
                    }
                }
            }
        }
        /// <summary>
        /// 页面加载完成回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
             {
                 StartLoading();
                 //脚本配置
                 var configManager = new JsonConfig(Const.ScriptXmlConfig.ScriptConfig);
                 var scriptConfigs = configManager.GetConfig();
                 //工具配置
                 ToolSettingsConfig toolSettingsConfig = new ToolSettingsConfig(Const.ScriptXmlConfig.ToolSettingsConfig);
                 var toolConfigs = toolSettingsConfig.GetConfig();
                 DeleteGameCache(scriptConfigs);
                 if (string.IsNullOrWhiteSpace(toolConfigs.DMSecret.Code) || string.IsNullOrWhiteSpace(toolConfigs.DMSecret.Ver))
                 {
                     MessageBox.Show(this, "请填写DM注册码！否则无法启动", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     Environment.Exit(0);
                 }
                 LoadDM(toolConfigs.DMSecret);
                 InitDataGrid();
                 //加载配置
                 scriptConfigs.Sort(delegate (ScriptConfig p1, ScriptConfig p2) { return p1.Sort.CompareTo(p2.Sort); });
                 LoadJsonConfig(scriptConfigs);
                 //Thread.Sleep(2000);
                 EndLoading();
             });
        }
        /// <summary>
        /// 首次进入加载配置
        /// </summary>
        /// <param name="scriptConfigs"></param>
        private void LoadJsonConfig(List<ScriptConfig> scriptConfigs)
        {
            foreach (var item in scriptConfigs)
            {
                this.dataGridView1.InvokeDataGridView(() =>
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Tag = item.ScriptId;
                    this.dataGridView1.Rows[index].Cells[0].Value = item.ScriptName;
                    this.dataGridView1.Rows[index].Cells[1].Value = item.Account;
                    this.dataGridView1.Rows[index].Cells[2].Value = item.Password;
                    this.dataGridView1.Rows[index].Cells[3].Value = "";
                    this.dataGridView1.Rows[index].Cells[4].Value = Const.ScriptTaskState.Not_Started_State.Msg;
                    this.dataGridView1.Rows[index].Cells[5].Value = "";
                    this.dataGridView1.Rows[index].Cells[6].Value = item.StartTime;
                    ScriptItemModel scriptItemModel = new ScriptItemModel();
                    scriptItemModel.ScriptId = item.ScriptId;
                    scriptItemModel.Sort = item.Sort;
                    scriptItemModel.ScriptName = item.ScriptName;
                    scriptItemModel.PlayerName = item.PlayerName;
                    scriptItemModel.Account = item.Account;
                    scriptItemModel.Password = item.Password;
                    scriptItemModel.SerialNumber = item.SerialNumber;
                    scriptItemModel.RestoreCode = item.RestoreCode;
                    scriptItemModel.GamePath = item.GamePath;
                    scriptItemModel.X = item.X;
                    scriptItemModel.Y = item.Y;
                    scriptItemModel.Width = item.Width;
                    scriptItemModel.Height = item.Height;
                    scriptItemModel.TabControl = tabControl1;
                    scriptItemModel.NvidiaShow = item.NvidiaShow;
                    scriptItemModel.SubAccount = item.SubAccount;
                    //添加任务
                    ScriptTaskSchedulerExecutor.GetInstance().LoadTask(scriptItemModel);
                });
            }
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var scriptItemModel = new ScriptItemModel();
            scriptItemModel.TabControl = tabControl1;
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm(false, scriptItemModel);
            if (scriptAddOrEditForm.ShowDialog() == DialogResult.OK)
            {
                //添加任务
                ScriptTaskSchedulerExecutor.GetInstance().AddTask(scriptItemModel);
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Tag = scriptItemModel.ScriptId;
                this.dataGridView1.Rows[index].Cells[0].Value = scriptItemModel.ScriptName;
                this.dataGridView1.Rows[index].Cells[1].Value = scriptItemModel.Account;
                this.dataGridView1.Rows[index].Cells[2].Value = scriptItemModel.Password;
                this.dataGridView1.Rows[index].Cells[3].Value = scriptItemModel.DynamicCode;
                this.dataGridView1.Rows[index].Cells[4].Value = scriptItemModel.Status.Msg;
                this.dataGridView1.Rows[index].Cells[5].Value = "";
                this.dataGridView1.Rows[index].Cells[6].Value = scriptItemModel.StartTime;
            }
        }
        /// <summary>
        /// 双击表格-编辑记录
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <param name="e"></param>
        private void DataTableDoubleClickHandler(object source, DoubleClickEventArgs args, DataGridViewCellEventArgs e)
        {
            var scriptItemModel = args.ScriptModel.scriptItemModel;
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm(true, scriptItemModel);
            if (scriptAddOrEditForm.ShowDialog() == DialogResult.OK)
            {
                int index = e.RowIndex;
                this.dataGridView1.Rows[index].Tag = scriptItemModel.ScriptId;
                this.dataGridView1.Rows[index].Cells[0].Value = scriptItemModel.ScriptName;
                this.dataGridView1.Rows[index].Cells[1].Value = scriptItemModel.Account;
                this.dataGridView1.Rows[index].Cells[2].Value = scriptItemModel.Password;
                this.dataGridView1.Rows[index].Cells[3].Value = scriptItemModel.DynamicCode;
                this.dataGridView1.Rows[index].Cells[4].Value = scriptItemModel.Status.Msg;
                this.dataGridView1.Rows[index].Cells[5].Value = "";
                this.dataGridView1.Rows[index].Cells[6].Value = scriptItemModel.StartTime;
                //更新任务
                ScriptTaskSchedulerExecutor.GetInstance().UpdateTask(scriptItemModel);
            }
        }
        /// <summary>
        /// 启动脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                var taskId = dataGridView1.SelectedRows[i].Tag.ToString();
                //获取脚本管理器
                var scriptModel = ScriptTaskSchedulerExecutor.GetInstance().GetScriptTaskManagerById(taskId);
                if (scriptModel.scriptItemModel.Status == Const.ScriptTaskState.Suspend_State ||
                    scriptModel.scriptItemModel.Status == Const.ScriptTaskState.Stopped_State ||
                    scriptModel.scriptItemModel.Status == Const.ScriptTaskState.Not_Started_State)
                {
                    if (scriptModel.scriptItemModel.Status == Const.ScriptTaskState.Not_Started_State)
                    {
                        //首次启动
                        //设置脚本内容
                        scriptModel.SetScriptAction((dm, Log) =>
                        {
                            //执行脚本
                            var list = new List<string>();
                            list.Add("f11");
                            list.Add("f12");
                            list = list.RandomSort();
                            foreach (var item in list)
                            {
                                var result = dm.KeyPressChar(item);
                                Thread.Sleep(RandomUtil.RandomInt(1, 6) * 100);
                                Log($"按下了{item}", Color.Blue);
                            }

                            //延迟间隔
                            var value = RandomUtil.RandomInt(1, 2);
                            Log($"延迟{value}秒", Color.Blue);
                            Thread.Sleep(value * 600);
                            var rdm = RandomUtil.RandomInt(1, 100);
                            if (rdm >= 93)
                            {
                                dm.KeyPressChar("space");
                                Thread.Sleep(value * 1000);
                                Log($"延迟{value}秒", Color.Blue);
                            }
                            Thread.Sleep(400);
                            dm.MoveTo(677, 146);
                            Thread.Sleep(400);
                            dm.LeftClick();
                            Thread.Sleep(200);
                            //Thread.Sleep(2 * 900);
                            //dm.KeyPressChar("esc");
                            //Thread.Sleep(1 * 200);
                        });
                        //scriptModel.SetScriptAction((dm, Log) =>
                        //{
                        //    //执行脚本
                        //    var move = DM.MoveTo(600, 352);
                        //    var click = DM.LeftDoubleClick();
                        //});
                        //启动脚本
                        ScriptTaskSchedulerExecutor.GetInstance().OperateOn(taskId, Const.ScriptTaskState.Not_Started_State.FunctionName);
                    }
                    //继续脚本
                    scriptModel.Continue();
                }

            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                return;
            }
            var taskId = dataGridView1.SelectedRows[0].Tag.ToString();
            var scriptModel = ScriptTaskSchedulerExecutor.GetInstance().GetScriptTaskManagerById(taskId);
            var scriptItemModel = scriptModel.scriptItemModel;
            ScriptAddOrEditForm scriptAddOrEditForm = new ScriptAddOrEditForm(true, scriptItemModel);
            if (scriptAddOrEditForm.ShowDialog() == DialogResult.OK)
            {
                ScriptTaskSchedulerExecutor.GetInstance().UpdateTask(scriptItemModel);
                dataGridView1.SelectedRows[0].Tag = scriptItemModel.ScriptId;
                this.dataGridView1.Rows[0].Cells[0].Value = scriptItemModel.ScriptName;
                this.dataGridView1.Rows[0].Cells[1].Value = scriptItemModel.Account;
                this.dataGridView1.Rows[0].Cells[2].Value = scriptItemModel.Password;
                this.dataGridView1.Rows[0].Cells[3].Value = scriptItemModel.DynamicCode;
                this.dataGridView1.Rows[0].Cells[4].Value = scriptItemModel.Status.Msg;
                this.dataGridView1.Rows[0].Cells[5].Value = "";
                this.dataGridView1.Rows[0].Cells[6].Value = scriptItemModel.StartTime;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            var deleteItem = "";
            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                var taskId = dataGridView1.SelectedRows[i].Tag.ToString();
                var scriptModel = ScriptTaskSchedulerExecutor.GetInstance().GetScriptTaskManagerById(taskId);
                var scriptItemModel = scriptModel.scriptItemModel;
                deleteItem += scriptItemModel.ScriptName + "-" + scriptItemModel.PlayerName + " ";
            }
            deleteItem = $"【{deleteItem}】";
            var dialog = MessageBox.Show(this, $"你确定要删除{deleteItem}吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialog == DialogResult.OK)
            {
                for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    var taskId = dataGridView1.SelectedRows[i].Tag.ToString();
                    ScriptTaskSchedulerExecutor.GetInstance().DeleteTask(taskId);
                    //删除页面数据
                    var rowIndex = dataGridView1.SelectedRows[i].Index;
                    dataGridView1.Rows.RemoveAt(rowIndex);
                }
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ToolSettingsForm toolSettingsForm = new ToolSettingsForm();
            toolSettingsForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DM.SetDict(0, AppDomain.CurrentDomain.BaseDirectory + @"Resources\ziku.txt");
            var hwnd = DM.FindWindowByProcessId(11472, "", "魔兽世界");
            var dmbind = DM.BindWindowEx(hwnd, "dx.graphic.3d.10plus", "dx.mouse.position.lock.api", "dx.keypad.raw.input", "", 0);
            //var sss = DM.Capture(1160, 4, 1190, 19, @$"{AppDomain.CurrentDomain.BaseDirectory}1.bmp");
            var sss = DM.Capture(1159, 3, 1188, 17, @$"{AppDomain.CurrentDomain.BaseDirectory}1.bmp");
            //var zzz=  DM.FindStr(0, 0, 2000, 2000, "要塞", "f1c600-937703", 0.8, out var x, out var y);
            var s = DM.Ocr(0, 0, 2000, 2000, "fed000-937703", 0.7);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                var taskId = dataGridView1.SelectedRows[i].Tag.ToString();
                //获取脚本管理器
                var scriptModel = ScriptTaskSchedulerExecutor.GetInstance().GetScriptTaskManagerById(taskId);
                scriptModel.Pause();
            }
        }
    }
}
