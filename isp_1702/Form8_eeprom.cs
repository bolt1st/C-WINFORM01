using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace isp_1702
{
    public partial class Form8_eeprom : Form
    {
        string xml_FilePath = ""; //xml路径
        public Form1 f1 = new Form1(); //调用f1中的方法
        public delegate void EventHandler(string form3_sendtxt);
        public EventHandler TxtChangeEvent;
        public delegate void UpdateForm_dl(int msg);
        Form0 f0;
        public Form8_eeprom()
        {
            InitializeComponent();
            
        }

        #region 加载窗口时自动生成地址dgv
        private void Form8_eeprom_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            f1 = (Form1)this.Owner; //与主窗口数据联动的

            for (int i = 0; i < 980; i++)//创建空的表  
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i,16).PadLeft(4,'0');
                //dataGridView1.Rows[i].Cells[1].Value = "";

            }
            if (Form2_serial.sp.IsOpen)
            {

                ////将主页中的datagridview数据显示到此页面
                dataGridView1.Rows[0].Cells[1].Value = "A9";
                dataGridView1.Rows[1].Cells[1].Value = "56";
                dataGridView1.Rows[2].Cells[1].Value = "04";
                dataGridView1.Rows[3].Cells[1].Value = "31";
                dataGridView1.Rows[4].Cells[1].Value = "03";
                dataGridView1.Rows[5].Cells[1].Value = "CE";

                string addr1 = null;
                string num2 = null;
                int data_num3 = 0;
                string addr4_0 = null; //进行上下地址对比，来判断
                int addr4_10 = 0;  //起始地址(10进制)  
                string addr4_1 = null; //与当前地址做比较的地址，即当前地址的下一行地址
                int addr4_1_10 = 0;  //起始地址+1(10进制)
                int duplicate_addr = 0;
                int row_num2 = 0;
                string addr4 = null;
                string data5 = null;
                int addr4_int = 0;
                int eeprom_addr2_int = 0;
                int eeprom_addr3_0_int = 0;
                int eeprom_addr3_1_int = 0;
                int row_num = 0;
                string data_num3_16 = null;
                int[] num_data = new int[f1.dataGridView1.Rows.Count];
                int addr4_end = 0;
                string eeprom_addr2 = null;
                string addr3 = null;
                string eeprom_addr3_0 = null;
                string eeprom_addr3_1 = null;
                string[] eeprom_addr3;
                string[] data_3 = new string[2];
                int num_ee = 6;

                for (int m = 0; m < f1.dataGridView1.Rows.Count; m++)
                {
                    data5 = f1.dataGridView1.Rows[m].Cells[3].Value.ToString().Replace("0x", "");
                    addr4_0 = f1.dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");
                    addr4_10 = Convert.ToInt32(addr4_0, 16);  //起始地址(10进制)
                    data_num3++;      //同一个地址段的连续地址寄存器个数（未排除重复的地址）
                    if (m <= f1.dataGridView1.Rows.Count - 2) //防止遍历到最后一行溢出(测试最后一行数据是否被遍历到)！！！！！！！！！！！！！！！！！！！！
                    {
                        addr4_1 = f1.dataGridView1.Rows[m + 1].Cells[1].Value.ToString().Replace("0x", ""); //与当前地址做比较的地址，即当前地址的下一行地址
                        addr4_1_10 = Convert.ToInt32(addr4_1, 16);  //起始地址+1

                        if (addr4_1_10 - addr4_10 == 0) //去掉重复的地址
                        {
                            duplicate_addr++;
                        }
                        else if (addr4_1_10 - addr4_10 == 1)
                        {
                            if (row_num2 == 0)
                            {
                                addr4_int = 9;
                            }

                            addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                            for (int j = num_ee; j < 980; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[0].Value.ToString() == addr4)
                                {
                                    dataGridView1.Rows[j].Cells[1].Value = data5;
                                    num_ee++;
                                }
                            }


                            addr4_int++;   //发送一次后，地址递加一个
                            row_num2++;



                        }
                        else if (addr4_1_10 - addr4_10 > 1) //一次连续地址的发送
                        {
                            data_num3_16 = Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');  //保证发送个数为两位数（排除重复的地址）
                            num_data[row_num] = 3 + data_num3 - duplicate_addr;
                            if (row_num == 0)
                            {

                                eeprom_addr2_int = 8;    //每段寄存器地址修改的寄存器个数
                                eeprom_addr3_0_int = 6;  //每段连续寄存器地址的第一个地址高位
                                eeprom_addr3_1_int = 7;
                            }
                            else
                            {
                                // addr4_int = addr4_int + 1;
                                eeprom_addr2_int = eeprom_addr2_int + num_data[row_num - 1];
                                eeprom_addr3_0_int = eeprom_addr3_0_int + num_data[row_num - 1];
                                eeprom_addr3_1_int = eeprom_addr3_1_int + num_data[row_num - 1];
                            }
                            addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                            for (int j = 6; j < 980; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[0].Value.ToString() == addr4)
                                {
                                    dataGridView1.Rows[j].Cells[1].Value = data5;
                                    // num_ee++;
                                }
                            }

                            addr4_end = addr4_int;

                            //////////////////////////////////利用普通模式发送EEPROM 每一组数据的修改寄存器个数///////222////////////////////////////check/////////


                            eeprom_addr2 = Convert.ToString(eeprom_addr2_int, 16).PadLeft(4, '0');
                            for (int j = 6; j < 980; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[0].Value.ToString() == eeprom_addr2)
                                {
                                    dataGridView1.Rows[j].Cells[1].Value = Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');
                                    // num_ee++;
                                }
                            }


                            //////////////////////////////////利用普通模式发送EEPROM 每一组数据的开始地址/////////////333/////////////////////////check///////////

                            addr3 = f1.dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                            eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');
                            eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');
                            eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };

                            for (int n = 0; n < 2; n++)
                            {
                                data_3[n] = addr3.Substring(n * 2, 2);
                                for (int j = 6; j < 980; j++)
                                {
                                    if (dataGridView1.Rows[j].Cells[0].Value.ToString() == eeprom_addr3[n])
                                    {
                                        dataGridView1.Rows[j].Cells[1].Value = data_3[n];
                                        // num_ee++;
                                    }
                                }


                            }

                            data_num3 = 0;        //发送完一次，发送个数归零
                            duplicate_addr = 0;    //发送完一次，重复地址个数归零
                            row_num++;
                            addr4_int += 4;


                        }


                    }
                    else if (m == f1.dataGridView1.Rows.Count - 1)
                    {
                        addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                        for (int j = 6; j < 980; j++)
                        {
                            if (dataGridView1.Rows[j].Cells[0].Value.ToString() == addr4)
                            {
                                dataGridView1.Rows[j].Cells[1].Value = data5;

                            }
                        }

                        //开始地址    checked
                        // num_data[row_num] = 3 + data_num3 - duplicate_addr;
                        eeprom_addr3_0_int = addr4_end + 1;
                        eeprom_addr3_1_int = addr4_end + 2;
                        addr3 = f1.dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                        eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');
                        eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');
                        eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };

                        for (int j1 = 0; j1 < 2; j1++)
                        {
                            data_3[j1] = addr3.Substring(j1 * 2, 2);

                            for (int j = 6; j < 980; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[0].Value.ToString() == eeprom_addr3[j1])
                                {
                                    dataGridView1.Rows[j].Cells[1].Value = data_3[j1];

                                }
                            }
                        }

                        //发送寄存器个数

                        eeprom_addr2 = Convert.ToString((eeprom_addr3_1_int + 1), 16).PadLeft(4, '0');
                        // send_text2 = "10A0" + eeprom_addr2 + Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');

                        for (int j = 6; j < 980; j++)
                        {
                            if (dataGridView1.Rows[j].Cells[0].Value.ToString() == eeprom_addr2)
                            {
                                dataGridView1.Rows[j].Cells[1].Value = Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');

                            }
                        }


                    }
                }






            }
            else
            {
                MessageBox.Show("串口未打开！");
            }

        }
        #endregion

        #region 读取EEPROM数据
        private void button1_Click(object sender, EventArgs e)
        {
            

            byte[] sendData = null;
            string send_text;
            string op1 = "30";  //读取操作码
            string slave2 = "A0";      //设备码
            string add3; //地址码

           
            //读取EEPROM中的数据显示在表格中
            if (Form2_serial.sp.IsOpen)
            {
                progressBar1.Visible = true;
                Thread fThread = new Thread(new ThreadStart(SleepT2));//开辟一个新的线程
                fThread.Start();
                 for (int i = 0; i < 980; i++)  
                {
                    //定时器自动关闭弹出的对话框
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Tick += new System.EventHandler(this.timer1_Tick);
                    timer1.Start();
                    f0 = new Form0();

                    add3 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    send_text = op1 + slave2 + add3;
                    sendData = f1.strToHexByte(send_text);
                    Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    // MessageBox.Show(f0, "读取中(" + i.ToString() + ")");
                    // MessageBox.Show(f0, "读取中(" + (979-i).ToString() + ")");
                    f0.ShowDialog();
                    if (f1.txtRec.Text != "")
                    {
                        dataGridView1.Rows[i].Cells[1].Value = f1.txtRec.Text.Substring(0,2);

                    }
                }
                

            }
            else
            {
                MessageBox.Show("请打开串口");
                
            }
            MessageBox.Show("读取成功！");

            button2.Enabled = true;
            progressBar1.Visible = false;


        }
        #endregion

        #region 保存EEPROM数据
        private void button2_Click(object sender, EventArgs e)
        {
           
            FileStream fs;
            BinaryWriter bw;
            int row = dataGridView1.Rows.Count;//得到总行数      
            
            SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框  
            saveFileDialog1.Filter = "bin文件(*.bin)|*.bin";//设置允许打开的扩展名  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径  
            {
                try
                {
                    fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    bw = new BinaryWriter(fs);
                    
                    for (int i = 0; i < row; i++)//遍历这个dataGridView  
                      {
                          bw.Write(Convert.ToSByte(dataGridView1.Rows[i].Cells[1].Value.ToString(),16));
                      }
                   
                    MessageBox.Show("保存成功");
                    bw.Close();
                }
                catch
                {
                    MessageBox.Show("保存失败，有空白数据，请检查并重新读取");
                }
              
            }
        }
        #endregion

        #region 定时器(中断读取过程)
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 停止定时器 
            timer1.Stop();
            // this.f0.Close();
            
            this.f0.Close();
        }

        #endregion

        #region 进度条
        private void SetTextMessage2(int ipos)
        {
            this.progressBar1.Value = Convert.ToInt32(ipos);
        }

        public void SleepT2()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(125);//没什么意思，单纯的执行延时
                SetTextMessage2(100 * i / 500);
            }
        }



        #endregion

       
    }
}
