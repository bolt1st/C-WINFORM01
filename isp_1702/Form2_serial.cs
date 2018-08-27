using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace isp_1702
{
    public partial class Form2_serial : Form
    {

        int sum_length = 0;  //控制数据接收判断
        public Form2_serial()
        {
            InitializeComponent();
            
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;


        }

        public delegate void returnvalue(string msg);
        public returnvalue  ReturnValue;
        public static SerialPort sp = new SerialPort();
        public delegate void EventHandler(string rectxt);
        public EventHandler TxtChangeEvent;
 

        #region 窗口加载初始化串口相关设置
        private void Form2_serial_Load(object sender, EventArgs e)
        {
            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口","Error");
                return;
            }
            sp.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);//添加事件处理程序
            //添加串口项目
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            { //获取有多少个COM口
                cb_serial.Items.Add(s);
            }

            //串口设置默认选项
          //  cb_serial.SelectedIndex = 0;
            cbBaudRate.SelectedIndex = 7;
            cbDtataBits.SelectedIndex = 3;
            cbStop.SelectedIndex = 0;
            cbParity.SelectedIndex = 0;
    
        }
        #endregion

        #region 数据显示相关
        public void AddData(byte[] data)
        {
            if(radioButton3.Checked)
            {
                int old_rec_len = txt_receive.Text.Length;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                txt_receive.Clear();  //每次发送前清空接收区数据，为配合form1的单行显示
                

                txt_receive.AppendText(sb.ToString().ToUpper());
            
            }
            else
            {
                AddContent(new ASCIIEncoding().GetString(data));
            }
        }

        private void AddContent(string content)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                if (txt_receive.Text.Length > 0)
                {
                    txt_receive.AppendText("\r\n");
                }
                txt_receive.AppendText(content);
            }));
        }
        #endregion

        #region 数据接收
        public void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Eof)  //串口通信中，0x1A会处理两次 ：1、当做EOF处理 2、数据本身 （屏蔽第一次操作）
            {
                return;
            }
         
            byte[] ReDatas = new byte[sp.BytesToRead];
            sp.Read(ReDatas, 0, ReDatas.Length);
            this.AddData(ReDatas);//输出数据

        }
        #endregion

        #region 打开串口按钮
        public void button1_Click(object sender, EventArgs e)
        {
            if (!sp.IsOpen)
            {
                try
                {
                    //设置串口号
                    string serialName = cb_serial.SelectedItem.ToString();
                    sp.PortName = serialName;

                    //设置各"串口设置"
                    string strBaudrate = cbBaudRate.Text;
                    string strDateBits = cbDtataBits.Text;
                    string strStopBits = cbStop.Text;
                    Int32 iBaudRate = Convert.ToInt32(strBaudrate);
                    Int32 iDateBits = Convert.ToInt32(strDateBits);

                    sp.BaudRate = iBaudRate;   //波特率
                    sp.DataBits = iDateBits;   //数据位
                    switch (cbStop.Text)        //停止位
                    {
                        case "1":
                            sp.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            sp.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            sp.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error:参数不正确！", "Error");
                            break;
                    }

                    switch (cbParity.Text)      //校验位
                    {
                        case "无":
                            sp.Parity = Parity.None;
                            break;
                        case "奇校验":
                            sp.Parity = Parity.Odd;
                            break;
                        case "偶校验":
                            sp.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error:参数不正确！", "Error");
                            break;
                    }

                    if (sp.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        sp.Close();
                    }

                    //设置必要控件不可用
                    cb_serial.Enabled = false;
                    cbBaudRate.Enabled = false;
                    cbDtataBits.Enabled = false;
                    cbStop.Enabled = false;
                    cbParity.Enabled = false;

                    button1.Text = "开启中";
                    button1.Enabled = false;
                    
                    sp.Open();  //打开串口

                    button1.Text = "关闭串口";

                    ReturnValue("已连接芯片:PIS1702"); //改变主页面左下角信息状态

                    

                    DialogResult dr = MessageBox.Show("串口已打开！");
                    if(dr == DialogResult.OK)
                    {
                        button1.Enabled = true;
                        this.WindowState = FormWindowState.Minimized; //点击确定后串口窗口自动最小化
                    }
                   

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    return;
                }
              }
            else
            {
                //恢复控件功能
                cb_serial.Enabled = true;
                cbBaudRate.Enabled = true;
                cbDtataBits.Enabled = true;
                cbStop.Enabled = true;
                cbParity.Enabled = true;
                sp.Close();         //关闭串口
                button1.Text = "打开串口";

                ReturnValue("未连接");
               

            }


         }
        #endregion

        #region 字符串转换16进制字节数组
        private byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
        #endregion

        #region 发送按钮
        private void btn_send_Click(object sender, EventArgs e)
        {
           // txt_receive.Clear(); //清空接收区
            byte[] sendData = null;
            if (sp.IsOpen)
            {
                if(txtSend.Text !="")
                {
                    
                    if (radioButton1.Checked)
                    {
                        sendData = strToHexByte(txtSend.Text.Trim());   
                    }
                    
                    else
                    {
                        sendData = Encoding.ASCII.GetBytes(txtSend.Text.Trim());
                    }
                    sp.Write(sendData, 0, sendData.Length);
                }
                else
                {
                    MessageBox.Show("发送数据不能为空", "Error");
                }
            }
            else
            {
                MessageBox.Show("请先打开串口", "Error");
            }
            
            
        }
        #endregion

        #region 清空按钮
        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_receive.Text = "";

        }
        #endregion

        #region 数据收发区域配合 form1的委托事件将发送接收数据同步显示在form1中
        private void txt_receive_TextChanged(object sender, EventArgs e)
        {
            TxtChangeEvent(txt_receive.Text);
          

        }
        #endregion

        #region 关闭窗口提示信息  （关闭窗口后串口自动关闭，回报异常）
        private void Form2_serial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("退出会断开串口连接，确定退出？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
               e.Cancel = true;
            }
            else
            {
                sp.Close();
                Dispose(); //清除资源
                ReturnValue("未连接");
            }
           
        }
        #endregion
    }
}

