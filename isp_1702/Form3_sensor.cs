using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace isp_1702
{
    public partial class Form3_sensor : Form
    {
        public Form1 f1 = new Form1();

        public Form3_sensor(Form1 f1)
        //public Form3_sensor()
        {
            this.f1 = f1;
            
            InitializeComponent();   
        }

        public delegate void EventHandler(string form3_sendtxt);
        public EventHandler TxtChangeEvent;

        #region 读取按钮
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] sendData = null;
            string send_text;
            string op1 = "30";  //读取操作码
            string slave2;      //设备码
            string add3 = textBox1.Text; //地址码
          
            
            if (Form2_serial.sp.IsOpen)
            {
                slave2 = deviceID.Text.ToString();
                send_text = op1 + slave2 + add3;
                sendData = f1.strToHexByte(send_text);
                Form2_serial.sp.Write(sendData, 0, sendData.Length);
            }
            else
            {
                MessageBox.Show("请打开串口");
            }
        }
        #endregion

        #region 配合委托窗口间传值相关
        public void showrec_value(string str)
        {
            textbox2.Text = str;
            
        }
        #endregion

        #region 发送按钮
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] sendData = null;
            string send_text;
            string op1 = "10";  //写入操作码
            string slave2;      //设备码
            string add3 = textBox1.Text; //地址码
            string data4 = textBox4.Text; //数据


            if (Form2_serial.sp.IsOpen)
            {
                slave2 = deviceID.Text.ToString();
                send_text = op1 + slave2 + add3 + data4;
                sendData = f1.strToHexByte(send_text);
                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                TxtChangeEvent("0x"+ textBox4.Text);
            }
            else
            {
                MessageBox.Show("请打开串口");
            }
        }
        #endregion

        #region 清空按钮
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textbox2.Text = "";
            textBox4.Text = "";
        }
        #endregion
    }
}

