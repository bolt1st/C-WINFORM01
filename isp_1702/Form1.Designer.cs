

namespace isp_1702
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("总控制");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("SC");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("EXP_GAIN");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("WINDOW");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("PLL");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("VIDEO_FORMAT");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("TVENC");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("TEMP SENSOR");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("TEST TIMING");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("TEST ANA");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("sensor控制", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("TPG");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("黑电平校正(BLC)+DOFF");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("行噪声校正(RNC)");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("镜头阴影校正(LSC)");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("坏点校正(DPC)");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("RAW域去噪(RAW_DENOISE)");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("自动曝光(AE)");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("IR-CUT");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("数字增益(DIGITAL GAIN)");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("去马赛克(DEMOSIC)");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("自动白平衡(AWB)+AWB GAIN");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("颜色校正(CCM)");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("GAMMA");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("色调映射(ToneMapping)");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("自动饱和度(Auto SAT)");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("RAW", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("自动工频干扰校正(AFD)");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("YUV域去噪(YUV_DENOISE)");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("边缘增强(EDGE)");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("自动对比度(Auto Contrast)");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("倒车线(PARKING GUIDE)");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("窗口裁剪(WIN_CLICP)");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("YUV", new System.Windows.Forms.TreeNode[] {
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33});
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("ISP控制", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode34});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.固件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数写入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本地导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据读取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到本地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eEPROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.写入EEPROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensor操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gAMMA曲线调试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cCMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aWBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bt_sensor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRec = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.固件ToolStripMenuItem,
            this.工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1095, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 串口设置ToolStripMenuItem
            // 
            this.串口设置ToolStripMenuItem.Name = "串口设置ToolStripMenuItem";
            this.串口设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.串口设置ToolStripMenuItem.Text = "串口设置";
            this.串口设置ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 固件ToolStripMenuItem
            // 
            this.固件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数写入ToolStripMenuItem,
            this.数据读取ToolStripMenuItem,
            this.eEPROMToolStripMenuItem});
            this.固件ToolStripMenuItem.Name = "固件ToolStripMenuItem";
            this.固件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.固件ToolStripMenuItem.Text = "固件";
            // 
            // 参数写入ToolStripMenuItem
            // 
            this.参数写入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.本地导入ToolStripMenuItem});
            this.参数写入ToolStripMenuItem.Name = "参数写入ToolStripMenuItem";
            this.参数写入ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.参数写入ToolStripMenuItem.Text = "数据导入";
            // 
            // 本地导入ToolStripMenuItem
            // 
            this.本地导入ToolStripMenuItem.Name = "本地导入ToolStripMenuItem";
            this.本地导入ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.本地导入ToolStripMenuItem.Text = "本地配置文件导入";
            this.本地导入ToolStripMenuItem.Click += new System.EventHandler(this.本地导入ToolStripMenuItem_Click);
            // 
            // 数据读取ToolStripMenuItem
            // 
            this.数据读取ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出到本地ToolStripMenuItem});
            this.数据读取ToolStripMenuItem.Name = "数据读取ToolStripMenuItem";
            this.数据读取ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.数据读取ToolStripMenuItem.Text = "数据导出";
            // 
            // 导出到本地ToolStripMenuItem
            // 
            this.导出到本地ToolStripMenuItem.Name = "导出到本地ToolStripMenuItem";
            this.导出到本地ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.导出到本地ToolStripMenuItem.Text = "保存到本地";
            this.导出到本地ToolStripMenuItem.Click += new System.EventHandler(this.导出到本地ToolStripMenuItem_Click);
            // 
            // eEPROMToolStripMenuItem
            // 
            this.eEPROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.写入EEPROMToolStripMenuItem,
            this.保存配置文件ToolStripMenuItem});
            this.eEPROMToolStripMenuItem.Name = "eEPROMToolStripMenuItem";
            this.eEPROMToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.eEPROMToolStripMenuItem.Text = "EEPROM";
            // 
            // 写入EEPROMToolStripMenuItem
            // 
            this.写入EEPROMToolStripMenuItem.Name = "写入EEPROMToolStripMenuItem";
            this.写入EEPROMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.写入EEPROMToolStripMenuItem.Text = "写入EEPROM";
            this.写入EEPROMToolStripMenuItem.Click += new System.EventHandler(this.写入EEPROMToolStripMenuItem_Click);
            // 
            // 保存配置文件ToolStripMenuItem
            // 
            this.保存配置文件ToolStripMenuItem.Name = "保存配置文件ToolStripMenuItem";
            this.保存配置文件ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.保存配置文件ToolStripMenuItem.Text = "保存配置文件";
            this.保存配置文件ToolStripMenuItem.Click += new System.EventHandler(this.保存配置文件ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sensor操作ToolStripMenuItem,
            this.gAMMA曲线调试ToolStripMenuItem,
            this.cCMToolStripMenuItem,
            this.aWBToolStripMenuItem,
            this.autoContrastToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // sensor操作ToolStripMenuItem
            // 
            this.sensor操作ToolStripMenuItem.Name = "sensor操作ToolStripMenuItem";
            this.sensor操作ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.sensor操作ToolStripMenuItem.Text = "Sensor操作";
            this.sensor操作ToolStripMenuItem.Click += new System.EventHandler(this.sensor操作ToolStripMenuItem_Click);
            // 
            // gAMMA曲线调试ToolStripMenuItem
            // 
            this.gAMMA曲线调试ToolStripMenuItem.Name = "gAMMA曲线调试ToolStripMenuItem";
            this.gAMMA曲线调试ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.gAMMA曲线调试ToolStripMenuItem.Text = "GAMMA曲线调试";
            this.gAMMA曲线调试ToolStripMenuItem.Click += new System.EventHandler(this.gAMMA曲线调试ToolStripMenuItem_Click);
            // 
            // cCMToolStripMenuItem
            // 
            this.cCMToolStripMenuItem.Name = "cCMToolStripMenuItem";
            this.cCMToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cCMToolStripMenuItem.Text = "CCM";
            this.cCMToolStripMenuItem.Click += new System.EventHandler(this.cCMToolStripMenuItem_Click);
            // 
            // aWBToolStripMenuItem
            // 
            this.aWBToolStripMenuItem.Name = "aWBToolStripMenuItem";
            this.aWBToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aWBToolStripMenuItem.Text = "AWB";
            this.aWBToolStripMenuItem.Click += new System.EventHandler(this.aWBToolStripMenuItem_Click);
            // 
            // autoContrastToolStripMenuItem
            // 
            this.autoContrastToolStripMenuItem.Name = "autoContrastToolStripMenuItem";
            this.autoContrastToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.autoContrastToolStripMenuItem.Text = "Auto Contrast";
            this.autoContrastToolStripMenuItem.Click += new System.EventHandler(this.autoContrastToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.bt_sensor,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1095, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // bt_sensor
            // 
            this.bt_sensor.Image = global::isp_1702.Properties.Resources.r;
            this.bt_sensor.Name = "bt_sensor";
            this.bt_sensor.Size = new System.Drawing.Size(60, 17);
            this.bt_sensor.Text = "未连接";
            this.bt_sensor.TextChanged += new System.EventHandler(this.bt_sensor_TextChanged);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 641);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.treeView1.Location = new System.Drawing.Point(12, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "tree_main";
            treeNode1.Text = "总控制";
            treeNode2.Name = "节点2";
            treeNode2.Text = "SC";
            treeNode3.Name = "节点3";
            treeNode3.Text = "EXP_GAIN";
            treeNode4.Name = "节点4";
            treeNode4.Text = "WINDOW";
            treeNode5.Name = "节点5";
            treeNode5.Text = "PLL";
            treeNode6.Name = "节点8";
            treeNode6.Text = "VIDEO_FORMAT";
            treeNode7.Name = "节点9";
            treeNode7.Text = "TVENC";
            treeNode8.Name = "节点10";
            treeNode8.Text = "TEMP SENSOR";
            treeNode9.Name = "节点11";
            treeNode9.Text = "TEST TIMING";
            treeNode10.Name = "节点12";
            treeNode10.Text = "TEST ANA";
            treeNode11.Name = "sensor_main";
            treeNode11.Text = "sensor控制";
            treeNode12.Name = "节点0";
            treeNode12.Text = "TPG";
            treeNode13.Name = "节点1";
            treeNode13.Text = "黑电平校正(BLC)+DOFF";
            treeNode13.ToolTipText = "黑电平校正";
            treeNode14.Name = "节点2";
            treeNode14.Text = "行噪声校正(RNC)";
            treeNode15.Name = "节点3";
            treeNode15.Text = "镜头阴影校正(LSC)";
            treeNode16.Name = "节点4";
            treeNode16.Text = "坏点校正(DPC)";
            treeNode17.Name = "节点5";
            treeNode17.Text = "RAW域去噪(RAW_DENOISE)";
            treeNode18.Name = "节点6";
            treeNode18.Text = "自动曝光(AE)";
            treeNode19.Name = "节点7";
            treeNode19.Text = "IR-CUT";
            treeNode20.Name = "节点8";
            treeNode20.Text = "数字增益(DIGITAL GAIN)";
            treeNode21.Name = "节点9";
            treeNode21.Text = "去马赛克(DEMOSIC)";
            treeNode22.Name = "节点10";
            treeNode22.Text = "自动白平衡(AWB)+AWB GAIN";
            treeNode23.Name = "节点11";
            treeNode23.Text = "颜色校正(CCM)";
            treeNode24.Name = "节点0";
            treeNode24.Text = "GAMMA";
            treeNode25.Name = "节点15";
            treeNode25.Text = "色调映射(ToneMapping)";
            treeNode26.Name = "节点16";
            treeNode26.Text = "自动饱和度(Auto SAT)";
            treeNode27.Name = "节点1";
            treeNode27.Text = "RAW";
            treeNode28.Name = "节点21";
            treeNode28.Text = "自动工频干扰校正(AFD)";
            treeNode29.Name = "节点22";
            treeNode29.Text = "YUV域去噪(YUV_DENOISE)";
            treeNode30.Name = "节点23";
            treeNode30.Text = "边缘增强(EDGE)";
            treeNode31.Name = "节点24";
            treeNode31.Text = "自动对比度(Auto Contrast)";
            treeNode32.Name = "节点26";
            treeNode32.Text = "倒车线(PARKING GUIDE)";
            treeNode33.Name = "节点7";
            treeNode33.Text = "窗口裁剪(WIN_CLICP)";
            treeNode34.Name = "节点2";
            treeNode34.Text = "YUV";
            treeNode35.Name = "isp_main";
            treeNode35.Text = "ISP控制";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode11,
            treeNode35});
            this.treeView1.Size = new System.Drawing.Size(250, 632);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtRec);
            this.groupBox2.Controls.Add(this.txtSend);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(532, 463);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 175);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息区";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "通讯状态:";
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.progressBar1.Location = new System.Drawing.Point(95, 88);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(174, 30);
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Note：Data(BIN)为 * 时，数据为16进制可调，\r\n      否则为二进制可调。";
            // 
            // txtRec
            // 
            this.txtRec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRec.HideSelection = false;
            this.txtRec.Location = new System.Drawing.Point(95, 62);
            this.txtRec.Name = "txtRec";
            this.txtRec.ReadOnly = true;
            this.txtRec.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRec.Size = new System.Drawing.Size(174, 14);
            this.txtRec.TabIndex = 11;
            this.txtRec.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // txtSend
            // 
            this.txtSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSend.Location = new System.Drawing.Point(95, 27);
            this.txtSend.Name = "txtSend";
            this.txtSend.ReadOnly = true;
            this.txtSend.Size = new System.Drawing.Size(174, 14);
            this.txtSend.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "接收数据:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "发送数据:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(13, 463);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 175);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "调整区";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(206, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 29);
            this.button5.TabIndex = 14;
            this.button5.Text = "批量读取";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(70, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 29);
            this.button4.TabIndex = 13;
            this.button4.Text = "批量写入";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(206, 125);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 29);
            this.button3.TabIndex = 12;
            this.button3.Text = "单次读取";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(341, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "重置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "单次写入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBar1.Location = new System.Drawing.Point(250, 20);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(232, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "寄存器参数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(13, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "寄存器参数";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(13, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(803, 401);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 170;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Address";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Data(BIN)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Data(HEX)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 62;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Bit";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Description";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 267;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "RW/RO";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "CID";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 80;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1095, 688);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "芯片调试工具(Image Design) V1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 固件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripStatusLabel bt_sensor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem 参数写入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据读取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本地导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出到本地ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.ToolStripMenuItem gAMMA曲线调试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensor操作ToolStripMenuItem;
        public System.Windows.Forms.TextBox txtRec;
        private System.Windows.Forms.ToolStripMenuItem cCMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aWBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoContrastToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem eEPROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 写入EEPROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存配置文件ToolStripMenuItem;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}

