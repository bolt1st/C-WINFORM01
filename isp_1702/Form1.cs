using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace isp_1702
{
    public partial class Form1 : Form
    {
        public delegate void change_form3_rec(string str);
        public change_form3_rec change_f3_rec;
        public static Form2_serial form_2;

        string xml_FilePath = "";
        Form3_sensor f3;
        Form0 f0;

        DateTime dt;    //进度条记时
        private delegate void SetPos(int ipos);

        public Form1()
        {
            InitializeComponent();

           

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        #region 连接串口后，显示配置文件
        private void bt_sensor_TextChanged(object sender, EventArgs e)
        {
            if (bt_sensor.Text != "未连接")
            {
                XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
               // xml_FilePath = "Data/PIS1702/1702_read.xml"; //xml路径
               xml_FilePath = "Data/PIS1702/base_1702.xml"; //xml路径
                xmlDocument.Load(xml_FilePath);//载入路径   

                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["reg_name"].InnerText;//各个单元格分别添加
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["Address"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["data_b"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["data_h"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Bit"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Description"].InnerText;
                    dataGridView1.Rows[index].Cells[6].Value = xmlNode.Attributes["Mem_Type"].InnerText;
                    dataGridView1.Rows[index].Cells[7].Value = xmlNode.Attributes["CID"].InnerText;
                }
                

            }
        }
        #endregion

        #region 串口设置
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form_2 == null) //如果没有打开过
            {
                form_2 = new Form2_serial();
                form_2.ReturnValue = new Form2_serial.returnvalue(bt_sensor_text);
                form_2.TxtChangeEvent += new Form2_serial.EventHandler(GetRectext);
                form_2.Show();
            }
            else
            {
                form_2.Activate(); //如果已经打开过就让其获得焦点  
                form_2.WindowState = FormWindowState.Normal;//使Form恢复正常窗体大小
            }
           
        }
        #endregion

        #region 主窗口芯片连接状态
        private void bt_sensor_text(string msg)
        {
            bt_sensor.Text = msg.ToString();
               if(bt_sensor.Text != "未连接")
               {
                bt_sensor.Image = global::isp_1702.Properties.Resources.g;
              }
               else
                bt_sensor.Image = global::isp_1702.Properties.Resources.r;

        }
        #endregion

        #region 工具-->sensor操作
        public void sensor操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
              f3 = new Form3_sensor(this);
              change_f3_rec = new change_form3_rec(f3.showrec_value);   //FORM3 寄存器的值
              f3.TxtChangeEvent = new Form3_sensor.EventHandler(Getform3_send);
              f3.Show();
           
            
        }
        #endregion

        #region 读取发送数据定义函数
        private void GetRectext(string rectxt)
        {
            txtRec.Text = rectxt;
        }
        #endregion

        #region f3发送数据实时更新到f1
        private void Getform3_send(string f3_sendtxt)
        {
            this.textBox1.Text = f3_sendtxt.ToString();
        }
        #endregion

        #region f4GAMMA发送数据实时更新到f1
        private void Getform4_send(string f4_sendtxt1, string f4_sendtxt2)
        {
            this.textBox1.Text = f4_sendtxt1.PadLeft(2,'0') + f4_sendtxt2.PadLeft(2, '0');
        }
        #endregion

        #region 滑动条
        private void trackBar1_Scroll(object sender, EventArgs e)   
        {

                dataGridView1.CurrentRow.Cells[3].Value ="0x" + Convert.ToString(trackBar1.Value,16) ; //滑动条10进制转成16进制
                textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                byte[] sendData = null;
                //修改的数据直发送到芯片还是本地也保存？
                if (Form2_serial.sp.IsOpen)
                {
                    if (textBox1.Text != "")
                    {
                        /*
                                     操作码(普通模式，写操作)：0(addr:8 data:8)，
                                                               8(addr:8 data:16)，
                                                               10(addr:16 data:8)，
                                                               18(addr:16 data:16)
                                     设备码: 60
                                     地址码(8/16)：
                                     DATA(8/16)：
                         */
                        int num_row = this.dataGridView1.CurrentCell.RowIndex;
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        string addr3 = dataGridView1.CurrentRow.Cells[1].Value.ToString();  //寄存器地址
                        string data4 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                        string addr3_0x = addr3.Replace("0x", "");  //去掉“0x”
                        string data4_0x = data4.Replace("0x", "");
                    
                   
                    if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "01") //同一个参数，存入两个寄存器
                    {
                        int i;
                        string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                        string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                        string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                        //string data4_down = textBox1.Text;
                        string data4_up = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                        string[] addr3_same = new string[2] { addr3_down, addr3_up };
                        string[] data4_same = new string[2] { data4_down, data4_up };

                        for (i = 0; i < 2; i++)
                        {
                            string addr3_same_0x = addr3_same[i].Replace("0x", "");
                            string data4_same_0x = data4_same[i].Replace("0x", "");
                            string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                    }
                    else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "") //同一个参数，存入两个寄存器
                    {
                        int i;
                        string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                        string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                        string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                                                                                          // string data4_down = textBox1.Text;
                        string data4_up = dataGridView1.Rows[num_row - 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };
                        string[] data4_same = new string[2] { data4_up, data4_down };

                        for (i = 0; i < 2; i++)
                        {
                            string addr3_same_0x = addr3_same[i].Replace("0x", "");
                            string data4_same_0x = data4_same[i].Replace("0x", "");
                            string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                    }
                    else  //一个寄存器，多个参数
                    {
                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "1") //单地址，单数据
                        {
                            string send_text = op1 + slave2 + addr3_0x + data4_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                        else
                        {
                            int cid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
                            string data_2, data_16;
                            string sum_data_2 = null;

                            if (dataGridView1.CurrentRow == dataGridView1.Rows[0]) //当前选中行是整个数据表的第一行(通过)
                            {
                                for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                {
                                    data_2 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                    sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                }
                                data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                string send_text = op1 + slave2 + addr3_0x + data_16;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;

                                for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                {
                                    dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                }
                            }
                            else if (dataGridView1.CurrentRow == dataGridView1.Rows[dataGridView1.RowCount - 1])//当前选中行是数据表的最后一行（通过）
                            {
                                for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                {
                                    data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                    sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                }
                                data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                string send_text = op1 + slave2 + addr3_0x + data_16;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;

                                for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                {
                                    dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                }
                            }
                            else //当前选中行非数据表首行与末行
                            {

                                if ((dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                   (dataGridView1.CurrentRow.Cells[1].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的第一行（通过）
                                {
                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[num_row + i].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;

                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[num_row + j].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else if ((dataGridView1.CurrentRow.Cells[1].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                         (dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的最后一行（通过）
                                {
                                    for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;

                                    for (int j = cid; j > 0; j--)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[num_row - (j - 1)].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else //中间部分（单独数据表+连续数据表）        需要解决单独数据块发送有问题！第二行数据发送无反应
                                {

                                    int row_start;
                                    int row_select;  //防溢出，行索引检测变量

                                    for (int k = 1; k < cid; k++)   //遍历中间的部分
                                    {

                                        if (num_row < cid)  //防止所选区域位于整个数据表的顶部区域溢出（所选数据表位于顶端情况）
                                        {
                                            row_select = num_row - k;
                                            if (row_select == 0)
                                            {
                                                for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                {
                                                    data_2 = dataGridView1.Rows[row_select + i].Cells[2].Value.ToString();
                                                    sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                }
                                                data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                string send_text = op1 + slave2 + addr3_0x + data_16;
                                                sendData = strToHexByte(send_text);
                                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                txtSend.Text = send_text;

                                                for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                {
                                                    dataGridView1.Rows[row_select + j].Cells[3].Value = "0x" + data_16;
                                                }

                                                break;

                                            }
                                        }
                                        else //（连续数据表情况）通过
                                        {

                                            if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row - k].Cells[1].Value.ToString())
                                            {
                                                row_start = num_row - k + 1;  //行索引的起始位置

                                                for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                {
                                                    data_2 = dataGridView1.Rows[row_start + i].Cells[2].Value.ToString();
                                                    sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                }
                                                data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                string send_text = op1 + slave2 + addr3_0x + data_16;
                                                sendData = strToHexByte(send_text);
                                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                txtSend.Text = send_text;

                                                for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                {
                                                    dataGridView1.Rows[row_start + j].Cells[3].Value = "0x" + data_16;
                                                }
                                                break;  //找到数据连接的起点，循环后跳出。

                                            }
                                        }

                                    }
                                }

                            }

                        }
                    } //end 单地址，多寄存器
                 


                } //end if data box
                else
                {
                    MessageBox.Show("发送数据不能为空", "Error");
                }
            } //end if serial open
            else
            {
                MessageBox.Show("串口未打开", "Error");
            }
 
        }
        #endregion

        #region 树菜单
        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string tree_text = treeView1.SelectedNode.Text;  //获得当前选中的值
            //dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
           
            switch (tree_text)
            {
                ////////////////////////////////////////////////////////////////
                ///////////////////////raw域处理参数///////////////////////////
                //////////////////////////////////////////////////////////////
                case "TPG":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "TPG_GRAYDIRECT")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "黑电平校正(BLC)+DOFF":
                    try
                    {
                        for(int i=0;i<dataGridView1.Rows.Count - 1;i++)
                        {
                            int scroll_index;
                            if(dataGridView1.Rows[i].Cells[0].Value.ToString() == "BLC_TRIGGER")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "行噪声校正(RNC)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "RNC_MODE")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "镜头阴影校正(LSC)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "LSC_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "坏点校正(DPC)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "DPC_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "RAW域去噪(RAW_DENOISE)":
                    
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "RAW_DENOISE_EDIG")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;
                    
                case "自动曝光(AE)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "AEC_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "IR-CUT":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "BWCTRL_MODE")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "数字增益(DIGITAL GAIN)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "DGAIN_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "去马赛克(DEMOSIC)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "DEMOSAIC_COLOR_PAT")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "自动白平衡(AWB)+AWB GAIN":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "AWB_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "颜色校正(CCM)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "CCM_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "GAMMA":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "GAMMA_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break; 

                case "色调映射(ToneMapping)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "TMP_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "自动饱和度(Auto SAT)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "ATS_GAIN_LN_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;
               //////////////////////////////////////////////////////////
               ///////////////// //YUV域/////////////////////////////////
               /////////////////////////////////////////////////////////
                case "自动工频干扰校正(AFD)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "AFD_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "YUV域去噪(YUV_DENOISE)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "Y_DENOISE_LN_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "边缘增强(EDGE)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "EDGE_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "自动对比度(Auto Contrast)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "BRIGHT_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "倒车线(PARKING GUIDE)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "PG_LN_EN")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "窗口裁剪(WIN_CLICP)":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "WIN_WST")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;
                /////////////////////////////////////////////////////////////////////////
                ////////////////////芯片配置/////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////
                case "SC":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "DEVICE_ID")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "EXP_GAIN":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "AEC_EXPT")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "WINDOW":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "FRAME_H")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "PLL":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "PLL_NRSET")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "VIDEO_FORMAT":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "TV_MODE")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "TVENC":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "EN_DAC")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "TEMP SENSOR":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "EN_TEMP")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "TEST TIMING":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "EN_RAW")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

                case "TEST ANA":
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            int scroll_index;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "PD_ADC")
                            {
                                scroll_index = dataGridView1.Rows[i].Index;
                                //滑动条焦点选为导航栏的标记点
                                dataGridView1.FirstDisplayedScrollingRowIndex = scroll_index;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("XML格式不对");
                    }
                    break;

            }
        }
        #endregion

        #region 点击DGV单元格，寄存器参数栏显示
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1) //判断是否为标题行，防止超出索引
                {
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "*")
                    {
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(); //固定16进制一列
                    }
                    else
                    {
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(); //固定2进制一列
                    }
                }

                if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "RO")
                {
                    textBox1.Enabled = false;
                    trackBar1.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
                else
                {
                    textBox1.Enabled = true;
                    trackBar1.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                }

                if (dataGridView1.CurrentRow.Cells[2].Value.ToString() != "*")  //滑动条
                {
                    trackBar1.Enabled = false;

                }
                else
                {
                    trackBar1.Enabled = true;
                    string trackbar_value = dataGridView1.CurrentRow.Cells[3].Value.ToString().Replace("0x", "");
                    if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[1:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[9:8]")
                    {
                        trackBar1.Maximum = 3;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[2:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[10:8]")
                    {
                        trackBar1.Maximum = 7;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[3:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[11:8]")
                    {
                        trackBar1.Maximum = 15;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[4:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[12:8]")
                    {
                        trackBar1.Maximum = 31;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[5:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[13:8]")
                    {
                        trackBar1.Maximum = 63;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[6:0]" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[14:8]")
                    {
                        trackBar1.Maximum = 127;
                    }
                    else if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "[0]")
                    {
                        trackBar1.Enabled = false;
                    }
                    else
                    {
                        trackBar1.Maximum = 255;
                    }

                    if (trackbar_value == "00")
                    {
                        trackBar1.Value = (Int32.Parse(trackbar_value, System.Globalization.NumberStyles.HexNumber) + 1);  //滑动条最小值不能为0
                    }
                    else
                    {
                        trackBar1.Value = Int32.Parse(trackbar_value, System.Globalization.NumberStyles.HexNumber);
                    }

                   

                }
            }
            catch
            {
                MessageBox.Show("所选16进制数据格式错误");
            }
            
          
        }
        #endregion

        #region 寄存器参数栏值改变刷新gdv
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "*")
            {
                dataGridView1.CurrentRow.Cells[3].Value = textBox1.Text;
            }
            else
            {
                dataGridView1.CurrentRow.Cells[2].Value = textBox1.Text;
            }
        }
        #endregion
        
        #region 数据重置按钮
        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            //数据还原（重新加载配置文件）
            xml_FilePath = "Data/PIS1702/1702_default.xml";
            xmlDocument.Load(xml_FilePath);//载入路径

            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["reg_name"].InnerText;//各个单元格分别添加
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["Address"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["data_b"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["data_h"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Bit"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Description"].InnerText;
                    dataGridView1.Rows[index].Cells[6].Value = xmlNode.Attributes["Mem_Type"].InnerText;
                    dataGridView1.Rows[index].Cells[7].Value = xmlNode.Attributes["CID"].InnerText;
                }

                MessageBox.Show("重置成功");

            }
            catch
            {
                MessageBox.Show("重置失败");
            }


        }
        #endregion

        #region  写入模块
        //字符串转16进制数组
        public byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int str_i = 0; str_i < returnBytes.Length; str_i++)
                returnBytes[str_i] = Convert.ToByte(hexString.Substring(str_i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
        
        //写入键
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] sendData = null;
            //修改的数据直发送到芯片还是本地也保存？
            if (Form2_serial.sp.IsOpen)
            {
                if (textBox1.Text != "")
                {
                    /*
                                 操作码(普通模式，写操作)：0(addr:8 data:8)，
                                                           8(addr:8 data:16)，
                                                           10(addr:16 data:8)，
                                                           18(addr:16 data:16)
                                 设备码: 60
                                 地址码(8/16)：
                                 DATA(8/16)：
                     */
                    int num_row = this.dataGridView1.CurrentCell.RowIndex;
                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    string addr3 = dataGridView1.CurrentRow.Cells[1].Value.ToString();  //寄存器地址
                    string data4 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    string addr3_0x = addr3.Replace("0x", "");  //去掉“0x”
                    string data4_0x = data4.Replace("0x", "");

                    #region 方案一
                    /*
                    if (dataGridView1.CurrentRow == dataGridView1.Rows[dataGridView1.RowCount - 1])  //最后一行，特殊处理，只能与上一行做比较
                        {
                            if(dataGridView1.CurrentRow.Cells[7].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[7].Value.ToString())  //对比相邻两行的数据
                            {
                                //操作码+设备码+地址码+数据
                                //  string test_1 = "10" + "60" + "4802" + "2C";
                                string send_text = op1 + slave2 + addr3_0x + data4_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            
                            }
                            else
                            {
                                int i;
                                string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                                string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                                //string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                string data4_down = textBox1.Text;
                                string data4_up = dataGridView1.Rows[num_row - 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                                string[] addr3_same = new string[2]{ addr3_up, addr3_down };
                                string[] data4_same = new string[2]{ data4_up, data4_down };

                                for(i=0;i<2;i++)
                               {
                                    string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                    string data4_same_0x = data4_same[i].Replace("0x", "");
                                    string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                }
                                
                                //MessageBox.Show("最后一行！,我们一样");//连续发送
                                
                            }
                         
                        }
                        else if(dataGridView1.CurrentRow == dataGridView1.Rows[0]) //第一行，只能与下面的一行做比较
                        {
                            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[7].Value.ToString())  //对比下行的数据
                            {
                                string send_text = op1 + slave2 + addr3_0x + data4_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;

                            }
                            else
                            {
                                int i;
                                string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                                string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                               // string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                string data4_down = textBox1.Text;
                                string data4_up = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                                string[] addr3_same = new string[2]{ addr3_down, addr3_up };
                                string[] data4_same = new string[2]{ data4_down, data4_up };

                                for(i=0;i<2;i++)
                               {
                                    string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                    string data4_same_0x = data4_same[i].Replace("0x", "");
                                    string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                }
                            
                            // MessageBox.Show("第一行！,我们一样");//连续发送
                            }
                        } //END else if
                        else    //除去第一行与最后一行的位置,分两种情况处理
                        { 
                            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() ==dataGridView1.Rows[num_row+1].Cells[7].Value.ToString())  //(非首尾行)当前选中行与下一行相同
                            {
                                int i;
                                string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                                string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                               // string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                string data4_down = textBox1.Text;
                                string data4_up = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                                string[] addr3_same = new string[2]{ addr3_down, addr3_up };
                                string[] data4_same = new string[2]{ data4_down, data4_up };

                                for(i=0;i<2;i++)
                               {
                                    string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                    string data4_same_0x = data4_same[i].Replace("0x", "");
                                    string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                }
                             }
                            else if(dataGridView1.CurrentRow.Cells[7].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[7].Value.ToString()) //(非首尾行)当前选中行与上一行相同
                        {
                                int i;
                                string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                                string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                               // string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                string data4_down = textBox1.Text;
                                string data4_up = dataGridView1.Rows[num_row - 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                                string[] addr3_same = new string[2]{ addr3_up, addr3_down };
                                string[] data4_same = new string[2]{ data4_up, data4_down };

                                for(i=0;i<2;i++)
                               {
                                    string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                    string data4_same_0x = data4_same[i].Replace("0x", "");
                                    string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                }
                            }
                            else
                            {
                                string send_text = op1 + slave2 + addr3_0x + data4_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            }
                        }
                        */
                    #endregion

                    try
                    {
                        #region 方案二


                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "01") //同一个参数，存入两个寄存器
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                                                                                              //string data4_down = textBox1.Text;
                            string data4_up = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                            string[] addr3_same = new string[2] { addr3_down, addr3_up };
                            string[] data4_same = new string[2] { data4_down, data4_up };

                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string data4_same_0x = data4_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                                System.Threading.Thread.Sleep(10);
                            }
                        }
                        else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "") //同一个参数，存入两个寄存器
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string data4_down = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //相同name的低位寄存器数据
                                                                                                              // string data4_down = textBox1.Text;
                            string data4_up = dataGridView1.Rows[num_row - 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string data4_same_0x = data4_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                                System.Threading.Thread.Sleep(10);
                            }
                        }
                        else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "03")
                        {
                            string data_16, add16;
                            string data16_0x, add16_0x;
                            if ((dataGridView1.CurrentRow.Cells[0].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[0].Value.ToString()) &&
                                      (dataGridView1.CurrentRow.Cells[0].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[0].Value.ToString())) //选中行为同name数据的第一行（通过）
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    data_16 = dataGridView1.Rows[num_row + i].Cells[3].Value.ToString();
                                    data16_0x = data_16.Replace("0x", "");
                                    add16 = dataGridView1.Rows[num_row + i].Cells[1].Value.ToString();
                                    add16_0x = add16.Replace("0x", "");
                                    string send_text = op1 + slave2 + add16_0x + data16_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }

                            }
                            else if ((dataGridView1.CurrentRow.Cells[0].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[0].Value.ToString()) &&
                                     (dataGridView1.CurrentRow.Cells[0].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[0].Value.ToString())) //选中行为同name数据的最后一行（通过）
                            {
                                for (int i = 4; i > 0; i--)
                                {
                                    data_16 = dataGridView1.Rows[num_row - (i - 1)].Cells[3].Value.ToString();
                                    data16_0x = data_16.Replace("0x", "");
                                    add16 = dataGridView1.Rows[num_row - (i - 1)].Cells[1].Value.ToString();
                                    add16_0x = add16.Replace("0x", "");
                                    string send_text = op1 + slave2 + add16_0x + data16_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }
                            }
                            else //中间部分（单独数据表+连续数据表）        需要解决单独数据块发送有问题！第二行数据发送无反应
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    data_16 = dataGridView1.Rows[num_row - 1 + i].Cells[3].Value.ToString();
                                    data16_0x = data_16.Replace("0x", "");
                                    add16 = dataGridView1.Rows[num_row - 1 + i].Cells[1].Value.ToString();
                                    add16_0x = add16.Replace("0x", "");
                                    string send_text = op1 + slave2 + add16_0x + data16_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }
                            }
                        }
                        else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "04")
                        {
                            string data_16, add16;
                            string data16_0x, add16_0x;
                            if ((dataGridView1.CurrentRow.Cells[0].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[0].Value.ToString()) &&
                                      (dataGridView1.CurrentRow.Cells[0].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[0].Value.ToString())) //选中行为同name数据的第一行（通过）
                            {
                                for (int i = 0; i < 4; i++)  //负责把各个单独的2进制数据组成完整的数据
                                {
                                    data_16 = dataGridView1.Rows[num_row + i].Cells[3].Value.ToString();
                                    data16_0x = data_16.Replace("0x", "");
                                    add16 = dataGridView1.Rows[num_row + i].Cells[1].Value.ToString();
                                    add16_0x = add16.Replace("0x", "");
                                    string send_text = op1 + slave2 + add16_0x + data16_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }

                            }
                            else if ((dataGridView1.CurrentRow.Cells[0].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[0].Value.ToString()) &&
                                     (dataGridView1.CurrentRow.Cells[0].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[0].Value.ToString())) //选中行为同name数据的最后一行（通过）
                            {
                                for (int i = 4; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                {
                                    data_16 = dataGridView1.Rows[num_row - (i - 1)].Cells[3].Value.ToString();
                                    data16_0x = data_16.Replace("0x", "");
                                    add16 = dataGridView1.Rows[num_row - (i - 1)].Cells[1].Value.ToString();
                                    add16_0x = add16.Replace("0x", "");
                                    string send_text = op1 + slave2 + add16_0x + data16_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }
                            }
                            else //中间部分（单独数据表+连续数据表）        需要解决单独数据块发送有问题！第二行数据发送无反应
                            {

                                int row_start;
                                for (int k = 1; k < 4; k++)   //遍历中间的部分
                                {
                                    if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != dataGridView1.Rows[num_row - k].Cells[0].Value.ToString())
                                    {
                                        row_start = num_row - k + 1;  //行索引的起始位置

                                        for (int i = 0; i < 4; i++)  //发送四个数据
                                        {
                                            data_16 = dataGridView1.Rows[row_start + i].Cells[3].Value.ToString();
                                            data16_0x = data_16.Replace("0x", "");
                                            add16 = dataGridView1.Rows[row_start + i].Cells[1].Value.ToString();
                                            add16_0x = add16.Replace("0x", "");
                                            string send_text = op1 + slave2 + add16_0x + data16_0x;
                                            sendData = strToHexByte(send_text);
                                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                            txtSend.Text = send_text;
                                            System.Threading.Thread.Sleep(10);
                                        }
                                        break;
                                    }
                                }


                            }//end 04
                        }
                        else  //一个寄存器，多个参数
                        {
                            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "1") //单地址，单数据
                            {
                                if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "*")  //两种情况
                                {
                                    string send_text = op1 + slave2 + addr3_0x + data4_0x;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                }
                                else
                                {
                                    string data_2 = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                                    string data_16 = Convert.ToString(Convert.ToInt32(data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    dataGridView1.CurrentRow.Cells[3].Value = "0x" + data_16; //更新16进制数的显示
                                    System.Threading.Thread.Sleep(10);

                                }

                            }
                            else
                            {
                                int cid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
                                string data_2, data_16;
                                string sum_data_2 = null;

                                if (dataGridView1.CurrentRow == dataGridView1.Rows[0]) //当前选中行是整个数据表的第一行(通过)
                                {
                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);

                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else if (dataGridView1.CurrentRow == dataGridView1.Rows[dataGridView1.RowCount - 1])//当前选中行是数据表的最后一行（通过）
                                {
                                    for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);

                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else //当前选中行非数据表首行与末行
                                {

                                    if ((dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                       (dataGridView1.CurrentRow.Cells[1].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的第一行（通过）
                                    {
                                        for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                        {
                                            data_2 = dataGridView1.Rows[num_row + i].Cells[2].Value.ToString();
                                            sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                        }
                                        data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                        string send_text = op1 + slave2 + addr3_0x + data_16;
                                        sendData = strToHexByte(send_text);
                                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                        txtSend.Text = send_text;
                                        System.Threading.Thread.Sleep(10);

                                        for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                        {
                                            dataGridView1.Rows[num_row + j].Cells[3].Value = "0x" + data_16;
                                        }
                                    }
                                    else if ((dataGridView1.CurrentRow.Cells[1].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                             (dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的最后一行（通过）
                                    {
                                        for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                        {
                                            data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                            sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                        }
                                        data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                        string send_text = op1 + slave2 + addr3_0x + data_16;
                                        sendData = strToHexByte(send_text);
                                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                        txtSend.Text = send_text;
                                        System.Threading.Thread.Sleep(10);

                                        for (int j = cid; j > 0; j--)  //更新16进制数的显示
                                        {
                                            dataGridView1.Rows[num_row - (j - 1)].Cells[3].Value = "0x" + data_16;
                                        }
                                    }
                                    else //中间部分（单独数据表+连续数据表）        需要解决单独数据块发送有问题！第二行数据发送无反应
                                    {

                                        int row_start;
                                        int row_select;  //防溢出，行索引检测变量

                                        for (int k = 1; k < cid; k++)   //遍历中间的部分
                                        {

                                            if (num_row < cid)  //防止所选区域位于整个数据表的顶部区域溢出（所选数据表位于顶端情况）
                                            {
                                                row_select = num_row - k;
                                                if (row_select == 0)
                                                {
                                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                    {
                                                        data_2 = dataGridView1.Rows[row_select + i].Cells[2].Value.ToString();
                                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                    }
                                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                                    sendData = strToHexByte(send_text);
                                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                    txtSend.Text = send_text;
                                                    System.Threading.Thread.Sleep(10);

                                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                    {
                                                        dataGridView1.Rows[row_select + j].Cells[3].Value = "0x" + data_16;
                                                    }

                                                    break;

                                                }
                                            }
                                            else //（连续数据表情况）通过
                                            {

                                                if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != dataGridView1.Rows[num_row - k].Cells[1].Value.ToString())
                                                {
                                                    row_start = num_row - k + 1;  //行索引的起始位置

                                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                    {
                                                        data_2 = dataGridView1.Rows[row_start + i].Cells[2].Value.ToString();
                                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                    }
                                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                                    sendData = strToHexByte(send_text);
                                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                    txtSend.Text = send_text;
                                                    System.Threading.Thread.Sleep(10);

                                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                    {
                                                        dataGridView1.Rows[row_start + j].Cells[3].Value = "0x" + data_16;
                                                    }
                                                    break;  //找到数据连接的起点，循环后跳出。

                                                }
                                            }

                                        }
                                    }//

                                }

                            }
                        } //end 单地址，多寄存器
                        #endregion
                    }
                    catch
                    {
                        MessageBox.Show("发送失败，请检查发送参数是否正确！\n参数是否超出范围或是否输入非法字符");
                    }
                } //end if data box
                else
                {
                    MessageBox.Show("发送数据不能为空", "Error");
                }
            } //end if serial open
            else
            {
                MessageBox.Show("串口未打开", "Error");
            }
        }
        #endregion

        #region 导出
        private void 导出到本地ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            XmlDeclaration Declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.Load(xml_FilePath);
            XmlNode xmlElement_chip = xmlDocument.SelectSingleNode("chip");//找到<chip>作为根节点 
            XmlElement xmlElement_module = xmlDocument.CreateElement("Module");//创建一个<Module>节点  
            xmlElement_chip.RemoveAll();//删除旗下所有节点 

            int row = dataGridView1.Rows.Count;//得到总行数      
            int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数      
            for (int i = 0; i < row; i++)//遍历这个dataGridView  
            {

                XmlElement xmlElement_reg = xmlDocument.CreateElement("reg");//创建<name>节点  
                XmlAttribute reg_name = xmlDocument.CreateAttribute("reg_name"); //创建节点属性 reg_name
                reg_name.InnerText = dataGridView1.Rows[i].Cells[0].Value.ToString();//其文本就是第0个单元格的内容

                XmlAttribute Address = xmlDocument.CreateAttribute("Address"); //创建节点属性 Address
                Address.InnerText = dataGridView1.Rows[i].Cells[1].Value.ToString();//其文本就是第1个单元格的内容

                XmlAttribute data_b = xmlDocument.CreateAttribute("data_b"); //创建节点属性 data_b
                data_b.InnerText = dataGridView1.Rows[i].Cells[2].Value.ToString();//其文本就是第2个单元格的内容

                XmlAttribute data_h = xmlDocument.CreateAttribute("data_h"); //创建节点属性 data_h
                data_h.InnerText = dataGridView1.Rows[i].Cells[3].Value.ToString();//其文本就是第3个单元格的内容

                XmlAttribute Bit = xmlDocument.CreateAttribute("Bit"); //创建节点属性 Bit
                Bit.InnerText = dataGridView1.Rows[i].Cells[4].Value.ToString();//其文本就是第4个单元格的内容

                XmlAttribute Description = xmlDocument.CreateAttribute("Description"); //创建节点属性 Description
                Description.InnerText = dataGridView1.Rows[i].Cells[5].Value.ToString();//其文本就是第5个单元格的内容

                XmlAttribute Mem_Type = xmlDocument.CreateAttribute("Mem_Type"); //创建节点属性 Mem_Type
                Mem_Type.InnerText = dataGridView1.Rows[i].Cells[6].Value.ToString();//其文本就是第6个单元格的内容

                XmlAttribute CID = xmlDocument.CreateAttribute("CID"); //创建节点属性 CID
                CID.InnerText = dataGridView1.Rows[i].Cells[7].Value.ToString();//其文本就是第7个单元格的内容

                xmlElement_reg.Attributes.Append(reg_name);
                xmlElement_reg.Attributes.Append(Address);
                xmlElement_reg.Attributes.Append(data_b);
                xmlElement_reg.Attributes.Append(data_h);
                xmlElement_reg.Attributes.Append(Bit);
                xmlElement_reg.Attributes.Append(Description);
                xmlElement_reg.Attributes.Append(Mem_Type);
                xmlElement_reg.Attributes.Append(CID);

                xmlElement_module.AppendChild(xmlElement_reg);//将这个<reg>节点放到<module>下方 

            }
            xmlElement_chip.AppendChild(xmlElement_module);//将这个<module>节点放到<chip>下方 
                                                           // xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", ""));//编写文件头
            xmlDocument.AppendChild(xmlElement_chip);//将这个<chip>附到总文件头，而且设置为根结点

            SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框  
            saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径  
            {
                // xmlDocument.Save(xml_FilePath);//保存这个xml  
                xmlDocument.Save(saveFileDialog1.FileName);//保存这个xml文件 
                MessageBox.Show("保存成功");
            }
        }
        #endregion

        #region 读取发送的数据
        private void button3_Click(object sender, EventArgs e)
        { 
            byte[] sendData = null;
            if (Form2_serial.sp.IsOpen)
            {
                if (textBox1.Text != "")
                {
                    int num_row = this.dataGridView1.CurrentCell.RowIndex;
                    string op1 = "30";        //操作码
                    string slave2 = "60";     //设备码
                    string addr3 = dataGridView1.CurrentRow.Cells[1].Value.ToString();  //寄存器地址
                    string addr3_0x = addr3.Replace("0x", "");  //去掉“0x”

                    #region 方案一(连续寄存器发送)
                    /*
                    if (dataGridView1.CurrentRow == dataGridView1.Rows[dataGridView1.RowCount - 1])  //最后一行，特殊处理，只能与上一行做比较
                    {
                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[7].Value.ToString())  //对比相邻两行的数据
                        {
                            //操作码+设备码+地址码
                            //  string test_1 = "30" + "60" + "4802";
                            string send_text = op1 + slave2 + addr3_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;

                        }
                        else
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x ;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            }

                        }

                    }
                    else if (dataGridView1.CurrentRow == dataGridView1.Rows[0]) //第一行，只能与下面的一行做比较
                    {
                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[7].Value.ToString())  //对比下行的数据
                        {
                            string send_text = op1 + slave2 + addr3_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;

                        }
                        else
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string[] addr3_same = new string[2] { addr3_down, addr3_up };

                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            }
                        }
                    } //END else if
                    else    //除去第一行与最后一行的位置,分两种情况处理
                    {
                        if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[7].Value.ToString())  //(非首尾行)当前选中行与下一行相同
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string[] addr3_same = new string[2] { addr3_down, addr3_up };
                                    
                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            }
                        }
                        else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[7].Value.ToString()) //(非首尾行)当前选中行与上一行相同
                        {
                            int i;
                            string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };

                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x ;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                            }
                        }
                        else
                        {
                            string send_text = op1 + slave2 + addr3_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                    }*/
                    #endregion 


                    #region 方案二
                    if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "01") //同一个参数，存入两个寄存器
                    {
                        int i;
                        string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                        string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                        string[] addr3_same = new string[2] { addr3_down, addr3_up };

                        for (i = 0; i < 2; i++)
                        {
                            string addr3_same_0x = addr3_same[i].Replace("0x", "");
                            string send_text = op1 + slave2 + addr3_same_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                    }
                    else if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "") //同一个参数，存入两个寄存器
                    {
                        int i;
                        string addr3_down = dataGridView1.CurrentRow.Cells[1].Value.ToString();           //相同name的低位寄存器地址
                        string addr3_up = dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };

                        for (i = 0; i < 2; i++)
                        {
                            string addr3_same_0x = addr3_same[i].Replace("0x", "");
                            string send_text = op1 + slave2 + addr3_same_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                        }
                    }
                    else  //一个寄存器，多个参数
                    {
                            string send_text = op1 + slave2 + addr3_0x;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                    }
                       
                    #endregion

                } //end if data box
                else
                {
                    MessageBox.Show("发送数据不能为空", "Error");
                }
            } //end if serial open
            else
            {
                MessageBox.Show("串口未打开", "Error");
            }

        } 
        
        #endregion

        #region sensor读写窗口功能相关方法
        private void textBox6_TextChanged(object sender, EventArgs e) //来配合委托显示在form3中
        {
           
            if (f3 != null)
            {
                change_f3_rec(txtRec.Text);
            }
            
        }
        #endregion

        #region DGV合并单元格
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 ) && e.RowIndex != -1)   //第一列相同内容的单元格合并
            {
                using( Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
                       backColorBrush = new SolidBrush(e.CellStyle.BackColor) )
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // 清除单元格
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        // 画 Grid 边线（仅画单元格的底边线和右边线）
                        //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                        if (e.RowIndex < dataGridView1.Rows.Count - 1 &&dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() !=e.Value.ToString())
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,e.CellBounds.Bottom - 1);
                        //画最后一条记录的底线 
                        if (e.RowIndex == dataGridView1.Rows.Count - 1)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left + 2, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //画右边线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,e.CellBounds.Top, e.CellBounds.Right - 1,e.CellBounds.Bottom);

                        // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                        if (e.Value != null)
                        {
                            if (e.RowIndex > 0 && dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() ==e.Value.ToString())
                            {

                            }
                            else
                            {
                                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                Brushes.Black, e.CellBounds.X + 2,
                                e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                        }
                        e.Handled = true;
                    }
                }
            
             }
        }
        #endregion

        #region GAMMA窗口
        private void gAMMA曲线调试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4_GAMMA f4 = new Form4_GAMMA();
            f4.TxtChangeEvent = new Form4_GAMMA.EventHandler(Getform4_send);
            f4.Owner = this;
            f4.Show();
        }

        #endregion

        #region GAMMA窗口联动主窗口显示函数

        private void f4tof1()
        {
            for (int i = 0;i< dataGridView1.Rows.Count;i++)
            {
                
                {

                }
            }
        }

        #endregion

        #region CCM窗口
        private void cCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5_CCM f5 = new Form5_CCM();
            f5.Owner = this;  //与主窗口数据联动的
            f5.Show();
        }
        #endregion

        #region AUTO_CONTRAST 窗口
        private void autoContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6_AUTO_CONTRAST f6 = new Form6_AUTO_CONTRAST();
            f6.Owner = this;  //与主窗口数据联动的 
            f6.Show();
        }
        #endregion

        #region AWB窗口
        private void aWBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7_AWB f7 = new Form7_AWB();
            f7.Owner = this;  //与主窗口数据联动的
            f7.Show();
        }


        #endregion

        #region 从本地导入
        private void 本地导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名  
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件    
            {
                xml_FilePath = openFileDialog1.FileName;//记录用户选择的文件路径  
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”  
                xmlDocument.Load(xml_FilePath);//载入路径这个xml  
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["reg_name"].InnerText;//各个单元格分别添加
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["data_b"].InnerText;
                        dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["data_h"].InnerText;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Bit"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Description"].InnerText;
                        dataGridView1.Rows[index].Cells[6].Value = xmlNode.Attributes["Mem_Type"].InnerText;
                        dataGridView1.Rows[index].Cells[7].Value = xmlNode.Attributes["CID"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("配置文件加载错误！请重新加载");
                }

                MessageBox.Show("导入成功");

            }
        }
        #endregion

        #region 批量写入
        private void button4_Click(object sender, EventArgs e)
        {
            if (Form2_serial.sp.IsOpen)
            {
                progressBar1.Visible = true;
                Thread fThread = new Thread(new ThreadStart(SleepT2));//开辟一个新的线程
                fThread.Start();
                dt = DateTime.Now;  //开始记录当前时间

                string op1 = "10";        //操作码
                string slave2 = "60";     //设备码
                byte[] sendData = null;

                for (int m = 0; m < dataGridView1.Rows.Count; m++)
                {
                    if (dataGridView1.Rows[m].Cells[6].Value.ToString() != "RO")
                    {
                        string addr3 = dataGridView1.Rows[m].Cells[1].Value.ToString();
                        string data4 = dataGridView1.Rows[m].Cells[3].Value.ToString();
                        string addr3_0x = addr3.Replace("0x", "");  //去掉“0x”
                        string data4_0x = data4.Replace("0x", "");
                        int num_row = this.dataGridView1.Rows[m].Index;
                        #region 发送
                        if (dataGridView1.Rows[m].Cells[7].Value.ToString() == "01") //同一个参数，存入两个寄存器
                        {
                            int i;
                            string addr3_down = dataGridView1.Rows[m].Cells[1].Value.ToString();           //相同name的低位寄存器地址
                            string addr3_up = dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString();  //相同name的高位寄存器地址
                            string data4_down = dataGridView1.Rows[m].Cells[3].Value.ToString();           //相同name的低位寄存器数据
                            //string data4_down = textBox1.Text;
                            string data4_up = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //相同name的高位寄存器数据
                            string[] addr3_same = new string[2] { addr3_down, addr3_up };
                            string[] data4_same = new string[2] { data4_down, data4_up };

                            for (i = 0; i < 2; i++)
                            {
                                string addr3_same_0x = addr3_same[i].Replace("0x", "");
                                string data4_same_0x = data4_same[i].Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                                System.Threading.Thread.Sleep(10);
                            }
                        }
                        else if (dataGridView1.Rows[m].Cells[7].Value.ToString() == "") //同一个参数，存入两个寄存器
                        {

                        }

                        else  //一个寄存器，多个参数
                        {
                            if (dataGridView1.Rows[m].Cells[7].Value.ToString() == "1" || dataGridView1.Rows[m].Cells[7].Value.ToString() == "03" || dataGridView1.Rows[m].Cells[7].Value.ToString() == "04") //单地址，单数据
                            {
                                string send_text = op1 + slave2 + addr3_0x + data4_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                txtSend.Text = send_text;
                                System.Threading.Thread.Sleep(10);
                            }
                            else
                            {
                                int cid = Convert.ToInt32(dataGridView1.Rows[m].Cells[7].Value);
                                string data_2, data_16;
                                string sum_data_2 = null;

                                if (m == 0) //当前选中行是整个数据表的第一行(通过)
                                {
                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else if (dataGridView1.Rows[m] == dataGridView1.Rows[dataGridView1.RowCount - 1])//当前选中行是数据表的最后一行（通过）
                                {
                                    for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                    {
                                        data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                    }
                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                    sendData = strToHexByte(send_text);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    txtSend.Text = send_text;
                                    System.Threading.Thread.Sleep(10);
                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                    {
                                        dataGridView1.Rows[j].Cells[3].Value = "0x" + data_16;
                                    }
                                }
                                else //当前选中行非数据表首行与末行
                                {

                                    if ((dataGridView1.Rows[m].Cells[1].Value.ToString() != dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                       (dataGridView1.Rows[m].Cells[1].Value.ToString() == dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的第一行（通过）
                                    {
                                        for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                        {
                                            data_2 = dataGridView1.Rows[num_row + i].Cells[2].Value.ToString();
                                            sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                        }
                                        data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                        string send_text = op1 + slave2 + addr3_0x + data_16;
                                        sendData = strToHexByte(send_text);
                                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                        txtSend.Text = send_text;
                                        System.Threading.Thread.Sleep(10);
                                        for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                        {
                                            dataGridView1.Rows[num_row + j].Cells[3].Value = "0x" + data_16;
                                        }
                                    }
                                    else if ((dataGridView1.Rows[m].Cells[1].Value.ToString() == dataGridView1.Rows[num_row - 1].Cells[1].Value.ToString()) &&
                                             (dataGridView1.Rows[m].Cells[1].Value.ToString() != dataGridView1.Rows[num_row + 1].Cells[1].Value.ToString())) //选中行为同地址数据的最后一行（通过）
                                    {
                                        for (int i = cid; i > 0; i--)  //负责把各个单独的2进制数据组成完整的数据
                                        {
                                            data_2 = dataGridView1.Rows[num_row - (i - 1)].Cells[2].Value.ToString();
                                            sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                        }
                                        data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                        string send_text = op1 + slave2 + addr3_0x + data_16;
                                        sendData = strToHexByte(send_text);
                                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                        txtSend.Text = send_text;
                                        System.Threading.Thread.Sleep(10);
                                        for (int j = cid; j > 0; j--)  //更新16进制数的显示
                                        {
                                            dataGridView1.Rows[num_row - (j - 1)].Cells[3].Value = "0x" + data_16;
                                        }
                                    }
                                    else //中间部分（单独数据表+连续数据表）        需要解决单独数据块发送有问题！第二行数据发送无反应
                                    {

                                        int row_start;
                                        int row_select;  //防溢出，行索引检测变量

                                        for (int k = 1; k < cid; k++)   //遍历中间的部分
                                        {

                                            if (num_row < cid)  //防止所选区域位于整个数据表的顶部区域溢出（所选数据表位于顶端情况）
                                            {
                                                row_select = num_row - k;
                                                if (row_select == 0)
                                                {
                                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                    {
                                                        data_2 = dataGridView1.Rows[row_select + i].Cells[2].Value.ToString();
                                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                    }
                                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                                    sendData = strToHexByte(send_text);
                                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                    txtSend.Text = send_text;
                                                    System.Threading.Thread.Sleep(10);
                                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                    {
                                                        dataGridView1.Rows[row_select + j].Cells[3].Value = "0x" + data_16;
                                                    }

                                                    break;

                                                }
                                            }
                                            else //（连续数据表情况）通过
                                            {

                                                if (dataGridView1.Rows[m].Cells[1].Value.ToString() != dataGridView1.Rows[num_row - k].Cells[1].Value.ToString())
                                                {
                                                    row_start = num_row - k + 1;  //行索引的起始位置

                                                    for (int i = 0; i < cid; i++)  //负责把各个单独的2进制数据组成完整的数据
                                                    {
                                                        data_2 = dataGridView1.Rows[row_start + i].Cells[2].Value.ToString();
                                                        sum_data_2 = sum_data_2 + data_2;  //从首行开始向下递加，把2进制字符串组合起来。
                                                    }
                                                    data_16 = Convert.ToString(Convert.ToInt32(sum_data_2, 2), 16);  //先转10进制，再转16进制
                                                    string send_text = op1 + slave2 + addr3_0x + data_16;
                                                    sendData = strToHexByte(send_text);
                                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                                    txtSend.Text = send_text;
                                                    System.Threading.Thread.Sleep(10);
                                                    for (int j = 0; j < cid; j++)  //更新16进制数的显示
                                                    {
                                                        dataGridView1.Rows[row_start + j].Cells[3].Value = "0x" + data_16;
                                                    }
                                                    break;  //找到数据连接的起点，循环后跳出。

                                                }
                                            }

                                        }
                                    }

                                }

                            }
                        } //end 单地址，多寄存器
                        #endregion
                    }
                }

                if (MessageBox.Show("写入成功") == DialogResult.OK)
                {
                    progressBar1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("串口未打开");
            }
            

        }

        #region 进度条
        private void SetTextMessage2(int ipos)
        {
            this.progressBar1.Value = Convert.ToInt32(ipos);
        }

        public void SleepT2()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(15);//没什么意思，单纯的执行延时
                SetTextMessage2(100 * i / 500);
            }
        }

        //  MessageBox.Show(DateTime.Now.Subtract(dt).ToString());  //循环结束截止时间
        #endregion

        #endregion

        #region 批量读取
        private void button5_Click(object sender, EventArgs e)
        {
           
            string op1 = "30";        //操作码 连续模式(70-100K  78-400K) 普通模式(30-100K 38-400K)
            string slave2 = "60";     //设备码
            byte[] sendData = null;

            int data_num3 = 0;  //需转成16进制

            #region 单发模式

            if (Form2_serial.sp.IsOpen)
                        {
                progressBar1.Visible = true;
                //进度条
                Thread fThread = new Thread(new ThreadStart(SleepT3));//开辟一个新的线程
                fThread.Start();
                for (int m = 0; m < dataGridView1.Rows.Count; m++)
                            {

                                //启动计时器
                                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                                timer.Tick += new System.EventHandler(this.timer1_Tick);
                               
                                timer1.Start();
                                f0 = new Form0();
                              
                                string addr3_same_0x = dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");
                                string send_text = op1 + slave2 + addr3_same_0x;
                                sendData = strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                
                               // MessageBox.Show(f0, "读取中(" + m.ToString() + ")");
                                f0.ShowDialog();
                               
                                dataGridView1.Rows[m].Cells[3].Value = txtRec.Text;
                                if(dataGridView1.Rows[m].Cells[3].Value.ToString() == "")
                                {
                                    dataGridView1.Rows[m].Cells[3].Value = txtRec.Text;
                                    if(dataGridView1.Rows[m].Cells[3].Value.ToString().Length !=2)
                                    {
                                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                        dataGridView1.Rows[m].Cells[3].Value = txtRec.Text;
                                    }
                                }
                            }
                            MessageBox.Show("读取成功！");
                            progressBar1.Visible = false;
                            

                            }
                        else
                        {
                            MessageBox.Show("串口未打开！");
                            progressBar1.Visible = false;
                            
                            
            }
            #endregion

            #region 连续模式
            /*
            if(Form2_serial.sp.IsOpen)
            {
               int row_num = 0;
               for(int m = 0; m < dataGridView1.Rows.Count; m++)
               {
                    string addr4 = dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");

                    int addr4_10 = Convert.ToInt32(addr4,16);  //起始地址
                  
                    if (m <= dataGridView1.Rows.Count - 2) //防止遍历到最后一行溢出
                    {
                        string addr4_1 = dataGridView1.Rows[m+1].Cells[1].Value.ToString().Replace("0x", ""); //与当前地址做比较的地址，即当前地址的下一行地址
                        int addr4_1_10 = Convert.ToInt32(addr4_1,16);  //起始地址+1
                       // if(addr4_1_10 - addr4_10 == 1)  //排除同一个地址，多个数据的情况
                       // {
                            data_num3++;  //统计连续的数据发送个数
                       // }
                        
                        if (addr4_1_10 - addr4_10 > 1)
                        {
                            string data_num3_16 = Convert.ToString(data_num3,16).PadLeft(2,'0');  //保证发送个数为两位数
                            addr4 = dataGridView1.Rows[m+1-data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                            string send_text = op1 + slave2 + data_num3_16 + addr4;
                            sendData = strToHexByte(send_text);
                            
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            txtSend.Text = send_text;
                            string[] strin = txtRec.Text.Split(' ');

                            MessageBox.Show(data_num3.ToString());
                            foreach (var data_rec in strin)
                            {
                                if(addr4_1_10 - addr4_10 == 0)
                                {
                                    
                                }
                                else
                                {
                                    dataGridView1.Rows[row_num].Cells[2].Value = data_rec;
                                }
                                
                                row_num++;
                            }
                            
                            data_num3 = 0;  // 发送完一次后发送数据个数置零。
                            
                            
                        }
                        
                    }

                }
                 
                
               

            }
            else
            {
                MessageBox.Show("串口未打开！");
            } */
            #endregion
        }

        #region 进度条
        private void SetTextMessage3(int ipos)
        {
            this.progressBar1.Value = Convert.ToInt32(ipos);
        }

        public void SleepT3()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(130);//没什么意思，单纯的执行延时
                SetTextMessage3(100 * i / 500);
            }
        }

        //  MessageBox.Show(DateTime.Now.Subtract(dt).ToString());  //循环结束截止时间
        #endregion

        #endregion

        #region 定时器(数据批量读取(单次))
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 停止定时器 
            timer1.Stop();
            
            this.f0.Close();
           
            
        }
        #endregion

        #region 批量写入EEPROM
        private void 写入EEPROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            dt = DateTime.Now;  //开始记录当前时间

            //////////////////////
            int m ;
            string op1 = "10";        //操作码 连续模式(70-100K  78-400K) 普通模式(30-100K 38-400K)
            string slave2 = "A0";     //设备码
            byte[] sendData = null;
            byte[] sendData1 = null;
            byte[] sendData2 = null;
            byte[] sendData3 = null;
            byte[] sendData4 = null;
            int data_num3 = 0;  //需转成16进制
            int e2prom_data2 = 0; //需转成16进制
            int duplicate_addr = 0;  //重复地址个数
            int row_num = 0;  //eeprom协议第二个数据()
            int row_num2 = 0; //统计发送表中数据
            int eeprom_addr3_0_int = 0; //用于计算每次发送时起始的地址1
            int eeprom_addr3_1_int = 0; //用于计算每次发送时起始的地址2
            int addr4_int = 0; //用于计算每次发送时数据的地址
            int addr4_int1 = 0; //用于计算每次发送时数据的地址
            int eeprom_addr2_int = 0; //用于计算每次发送时修改寄存器个数的地址
            
            string addr4_0 = null; //进行上下地址对比，来判断
            int addr4_10 = 0;  //起始地址(10进制)  
            string addr4_1 = null; //与当前地址做比较的地址，即当前地址的下一行地址
            int addr4_1_10 = 0;  //起始地址+1(10进制)
            string data5 = null; //发送表中的数据

            string addr3;                 //eeprom存储寄存器起始地址  第三部分
            string eeprom_addr3_0 = null;
            string eeprom_addr3_1 = null;
            string[] eeprom_addr3;
            string[] data_3 = new string[2];
            string send_text3 = null;
            int[] num_data = new int[dataGridView1.Rows.Count];

            string eeprom_addr2 = null;    //统计数据存入EEPROM个数    第二部分
            string send_text2 = null;

           // string[] sum_data = new string[dataGridView1.Rows.Count];   //发送表中的寄存器值  第一部分
            string data_num3_16 = null;
            string addr4 = null;
            int addr4_end = 0;
            string send_text = null;

            if (Form2_serial.sp.IsOpen)
            {
                progressBar1.Visible = true;
                //进度条
                Thread fThread = new Thread(new ThreadStart(SleepT));//开辟一个新的线程
                fThread.Start();


                #region 连续模式
                //先发送文件头
                /*
                    string[] send_text0 = new string[4] { "10A00000A9" , "10A0000156","10A0000204", "10A0000331" };
                    byte[] sendData00 = null;
                    //string sum_data = string.Empty;
                    StringBuilder sum_data = new StringBuilder();  //连续模式字符串拼接
                    string send_text00 = null;


                    int data_4_int = 0;
                    for (int i=0;i<4;i++)
                    {
                        send_text00 = send_text0[i];
                        sendData00 = strToHexByte(send_text00);
                        Form2_serial.sp.Write(sendData00, 0, sendData00.Length);
                        System.Threading.Thread.Sleep(30);
                    }

                    for (m = 0; m < dataGridView1.Rows.Count; m++)
                    {

                        data5 = dataGridView1.Rows[m].Cells[3].Value.ToString().Replace("0x", "");
                        addr4_0 = dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");
                        addr4_10 = Convert.ToInt32(addr4_0, 16);  //起始地址(10进制)

                        if (m <= dataGridView1.Rows.Count - 2) //防止遍历到最后一行溢出(测试最后一行数据是否被遍历到)！！！！！！！！！！！！！！！！！！！！
                        {
                            
                            addr4_1 = dataGridView1.Rows[m + 1].Cells[1].Value.ToString().Replace("0x", ""); //与当前地址做比较的地址，即当前地址的下一行地址
                            addr4_1_10 = Convert.ToInt32(addr4_1, 16);  //起始地址+1
                            data_num3++;      //同一个地址段的连续地址寄存器个数（未排除重复的地址）

                            if (addr4_1_10 - addr4_10 == 0) //去掉重复的地址
                            {
                                duplicate_addr++;
                            }
                            else if (addr4_1_10 - addr4_10 == 1)
                            {
                                sum_data.Append(data5.ToString());//连续发送的数据叠加
                            }
                            else if (addr4_1_10 - addr4_10 > 1) //一次连续地址的发送
                            {
                                sum_data.AppendLine(data5.ToString());//连续发送的数据叠加   
                                /////////////////////////////////利用连续写模式写EEPROM data部分////////////////111/////////////////////////////////////////check/////////

                                data_num3_16 = Convert.ToString((data_num3-duplicate_addr), 16).PadLeft(2, '0');  //保证发送个数为两位数（排除重复的地址）
                                num_data[row_num] = 3 + data_num3 - duplicate_addr;
                                if (row_num == 0)
                                   {
                                       addr4_int = 9;
                                       eeprom_addr2_int = 8;
                                       eeprom_addr3_0_int = 6;
                                       eeprom_addr3_1_int = 7;
                                   }
                                else
                                   {
                                       addr4_int = addr4_int + num_data[row_num-1];
                                       eeprom_addr2_int = eeprom_addr2_int + num_data[row_num - 1];
                                       eeprom_addr3_0_int = eeprom_addr3_0_int + num_data[row_num - 1];
                                       eeprom_addr3_1_int = eeprom_addr3_1_int + num_data[row_num - 1];
                                    }
                                addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                               // send_text = "50A0" + data_num3_16 + addr4 + sum_data.ToString();
                               // sendData = strToHexByte(send_text);
                               // Form2_serial.sp.Write(sendData, 0, sendData.Length);
                               // sum_data.Remove(0,sum_data.Length);
                               // System.Threading.Thread.Sleep(100);  //暂停100ms（连续发送之间间隔）                                   

                                
                                //////////////////////////////////利用普通模式发送EEPROM 每一组数据的修改寄存器个数///////222////////////////////////////check/////////
                               
                               // MessageBox.Show("写入中");
                                eeprom_addr2 = Convert.ToString(eeprom_addr2_int,16).PadLeft(4, '0');
                                send_text2 = "10A0"+ eeprom_addr2+ Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');
                                sendData = strToHexByte(send_text2);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                System.Threading.Thread.Sleep(30);  //暂停30ms
                                //////////////////////////////////利用普通模式发送EEPROM 每一组数据的开始地址/////////////333/////////////////////////check///////////
                               
                                //MessageBox.Show("写入中");
                                addr3 = dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                                eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');
                                eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');
                                eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };
                                
                                for(int j=0;j<2;j++)
                                {
                                    data_3[j] = addr3.Substring(j*2,2);
                                    send_text3 = "10A0" + eeprom_addr3[j] + data_3[j];
                                    sendData = strToHexByte(send_text3);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                   System.Threading.Thread.Sleep(30);  //暂停10ms
                                }
                                
                                //MessageBox.Show("写入中");
                                //////////////////////////////////利用普通模式发送EEPROM 05地址后所有改动的地址总个数//////444/////////////////////////////////
                                string[] eeprom_addr4 = new string[2] { "0004", "0005" };
                                string[] eeprom_data4 = new string[2];
                                data_4_int = data_4_int + 3 + data_num3 - duplicate_addr;
                                string data_4 = Convert.ToString(data_4_int, 16).PadLeft(4, '0');
                                string send_text4 = null;
                                for (int k=0;k<2;k++)
                                {
                                    eeprom_data4[k] = data_4.Substring(k * 2, 2);
                                    send_text4 = "10A0" + eeprom_addr4[k] + eeprom_data4[k];
                                    sendData = strToHexByte(send_text4);
                                    Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                    System.Threading.Thread.Sleep(10);  //暂停10ms
                                   
                                }
                               
                                data_num3 = 0;        //发送完一次，发送个数归零
                                duplicate_addr = 0;    //发送完一次，重复地址个数归零
                                row_num++;
                         
                            }
                            

                        }
                        else if(m == dataGridView1.Rows.Count - 1)
                        {
                           //单次发(普通模式发)
                        }
                        else if(m == dataGridView1.Rows.Count)
                        {
                            //单次发
                        }


                    }
                    //MessageBox.Show(row_num.ToString());


                        MessageBox.Show("写入成功");

                */
                #endregion

                #region 普通模式
                /* 
                 string[] send_text0 = new string[4] { "10A00000A9", "10A0000156", "10A0000204", "10A0000331" };
                 byte[] sendData00 = null;
                 string send_text00 = null;
                 for (int i = 0; i < 4; i++)   //发送eeprom文件头(checked)
                 {
                     send_text00 = send_text0[i];
                     sendData00 = strToHexByte(send_text00);
                     Form2_serial.sp.Write(sendData00, 0, sendData00.Length);
                     System.Threading.Thread.Sleep(10);

                 }
                 ///////////////////////////////////////////////////////////////////////////////////////////////

                 for (m = 0; m < dataGridView1.Rows.Count-2; m++)    //最后两行没有放到循环中，最后加上！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
                 {
                     System.Threading.Thread.Sleep(10);  //暂停10ms
                     data_num3++;      //同一个地址段的连续地址寄存器个数（未排除重复的地址）
                     data5 = dataGridView1.Rows[m].Cells[3].Value.ToString().Replace("0x", ""); //发送表中的数据
                     addr4_0 = dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");
                     addr4_10 = Convert.ToInt32(addr4_0, 16);  //起始地址(10进制)  
                     addr4_1 = dataGridView1.Rows[m + 1].Cells[1].Value.ToString().Replace("0x", ""); //与当前地址做比较的地址，即当前地址的下一行地址
                     addr4_1_10 = Convert.ToInt32(addr4_1, 16);  //起始地址+1

                     //--------各个信息的地址----------//
                     num_data[row_num] = 3 + data_num3 - duplicate_addr;   //EEPROM  05地址后个数统计
                     if (row_num == 0) //初始化其他几个值
                     {
                        // addr4_int = 9;
                         eeprom_addr2_int = 8;
                         eeprom_addr3_0_int = 6;
                         eeprom_addr3_1_int = 7;
                     }
                     else
                     {
                         //addr4_int = addr4_int + num_data[row_num - 1]; //数据段的起始地址
                         eeprom_addr2_int = eeprom_addr2_int + num_data[row_num - 1];
                         eeprom_addr3_0_int = eeprom_addr3_0_int + num_data[row_num - 1];
                         eeprom_addr3_1_int = eeprom_addr3_1_int + num_data[row_num - 1];
                     }

                     //---------去掉重复的地址---------//
                     if (addr4_1_10 - addr4_10 == 0) 
                     {
                         duplicate_addr++;    //重复的地址个数计数
                     }
                     if (addr4_1_10 - addr4_10 == 1) //连续的数据逐个发送
                     {
                         if(row_num2 == 0)
                         {
                             addr4_int = 9;
                         }

                         addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                         send_text = op1 + slave2 + addr4 + data5;
                         sendData = strToHexByte(send_text);
                         Form2_serial.sp.Write(sendData, 0, sendData.Length);
                         addr4_int++;   //发送一次后，地址递加一个
                         row_num2++;
                        // System.Threading.Thread.Sleep(10);  //暂停10ms
                     }
                     if (addr4_1_10 - addr4_10 > 1)   //切换到下一个连续的数据
                     {
                         addr3 = dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", "");              //发送地址为此地址段的第一个地址
                         eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');                            //eeprom存储寄存器地址的地址1
                         eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');                            //eeprom存储寄存器地址的地址2
                         eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };

                         //---------寄存器的起始地址 3000开始*******第三部分---------//
                         for (int j = 0; j < 2; j++)
                         {
                             data_3[j] = addr3.Substring(j * 2, 2);
                             send_text3 = "10A0" + eeprom_addr3[j] + data_3[j];
                             sendData3 = strToHexByte(send_text3);
                             Form2_serial.sp.Write(sendData3, 0, sendData3.Length);
                            // System.Threading.Thread.Sleep(10);  //暂停10ms

                         }

                         //---------存储data5写入到eeprom中的个数*****第二部分***************--//

                         eeprom_addr2 = Convert.ToString(eeprom_addr2_int, 16).PadLeft(4, '0');
                         send_text2 = "10A0" + eeprom_addr2 + Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');
                         sendData2 = strToHexByte(send_text2);
                         Form2_serial.sp.Write(sendData2, 0, sendData2.Length);
                       //  System.Threading.Thread.Sleep(10);

                         //-***********寄存器的数值写入到eeprom中********第一部分****************-//

                         //sum_data[row_num2] = data5; //切换到发送情景时，当前组的最后一个数据也要发送
                         addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                         send_text = op1 + slave2 + addr4 + data5;
                         sendData = strToHexByte(send_text);
                         Form2_serial.sp.Write(sendData, 0, sendData.Length);
                         addr4_int +=4;   //发送一个周期后，地址递加4
                        // System.Threading.Thread.Sleep(10);
                         //-**********计算eeprom中地址07后面的改动的地址个数********第四部分****-//



                         //-****发送完一个周期后部分值归零***-//
                         row_num++;
                         //row_num2++;
                         data_num3 = 0;
                         duplicate_addr = 0;
                         sum_data = null;
                        // System.Threading.Thread.Sleep(10);


                     }


                 }

             */

                #endregion

                #region 有效的模式

                //先发送文件头

                string[] send_text0 = new string[4] { "10A00000A9" , "10A0000156","10A0000204", "10A0000331" };
                    byte[] sendData00 = null;
                    //string sum_data = string.Empty;
                    StringBuilder sum_data = new StringBuilder();  //连续模式字符串拼接
                    string send_text00 = null;


                    int data_4_int = 0;
                    for (int i=0;i<4;i++)
                    {
                        send_text00 = send_text0[i];
                        sendData00 = strToHexByte(send_text00);
                        Form2_serial.sp.Write(sendData00, 0, sendData00.Length);
                        System.Threading.Thread.Sleep(10);
                    }

                    for (m = 0; m < dataGridView1.Rows.Count; m++)
                    {
                        data5 = dataGridView1.Rows[m].Cells[3].Value.ToString().Replace("0x", "");
                        addr4_0 = dataGridView1.Rows[m].Cells[1].Value.ToString().Replace("0x", "");
                        addr4_10 = Convert.ToInt32(addr4_0, 16);  //起始地址(10进制)
                        data_num3++;      //同一个地址段的连续地址寄存器个数（未排除重复的地址）
                     if (m <= dataGridView1.Rows.Count - 2) //防止遍历到最后一行溢出(测试最后一行数据是否被遍历到)！！！！！！！！！！！！！！！！！！！！
                     {

                        addr4_1 = dataGridView1.Rows[m + 1].Cells[1].Value.ToString().Replace("0x", ""); //与当前地址做比较的地址，即当前地址的下一行地址
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
                            send_text = "10A0" + addr4 + data5;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            System.Threading.Thread.Sleep(10);
                            addr4_int++;   //发送一次后，地址递加一个
                            row_num2++;

                        }
                        else if (addr4_1_10 - addr4_10 > 1) //一次连续地址的发送
                        {

                            /////////////////////////////////利用连续写模式写EEPROM data部分////////////////111/////////////////////////////////////////check/////////

                            data_num3_16 = Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');  //保证发送个数为两位数（排除重复的地址）
                            num_data[row_num] = 3 + data_num3 - duplicate_addr;
                            if (row_num == 0)
                            {
                                // addr4_int = 9 + data_num3 - duplicate_addr -1 ;
                                eeprom_addr2_int = 8;
                                eeprom_addr3_0_int = 6;
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
                            send_text = "10A0" + addr4 + data5;
                            sendData = strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            System.Threading.Thread.Sleep(10);  //暂停10ms
                            addr4_end = addr4_int;

                            //////////////////////////////////利用普通模式发送EEPROM 每一组数据的修改寄存器个数///////222////////////////////////////check/////////


                            eeprom_addr2 = Convert.ToString(eeprom_addr2_int, 16).PadLeft(4, '0');
                            send_text2 = "10A0" + eeprom_addr2 + Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');
                            sendData = strToHexByte(send_text2);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            System.Threading.Thread.Sleep(10);  //暂停30ms
                            //////////////////////////////////利用普通模式发送EEPROM 每一组数据的开始地址/////////////333/////////////////////////check///////////

                            addr3 = dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                            eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');
                            eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');
                            eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };

                            for (int j = 0; j < 2; j++)
                            {
                                data_3[j] = addr3.Substring(j * 2, 2);
                                send_text3 = "10A0" + eeprom_addr3[j] + data_3[j];
                                sendData = strToHexByte(send_text3);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                System.Threading.Thread.Sleep(10);  //暂停10ms
                            }


                            //////////////////////////////////利用普通模式发送EEPROM 05地址后所有改动的地址总个数//////444/////////////////////////////////
                            string[] eeprom_addr4 = new string[2] { "0004", "0005" };
                            string[] eeprom_data4 = new string[2];
                            data_4_int = data_4_int + 3 + data_num3 - duplicate_addr;
                            string data_4 = Convert.ToString(data_4_int, 16).PadLeft(4, '0');
                            string send_text4 = null;
                            for (int k = 0; k < 2; k++)
                            {
                                eeprom_data4[k] = data_4.Substring(k * 2, 2);
                                send_text4 = "10A0" + eeprom_addr4[k] + eeprom_data4[k];
                                sendData = strToHexByte(send_text4);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                                System.Threading.Thread.Sleep(10);  //暂停10ms

                            }

                            data_num3 = 0;        //发送完一次，发送个数归零
                            duplicate_addr = 0;    //发送完一次，重复地址个数归零
                            row_num++;
                            addr4_int += 4;


                        }


                    }
                    else if (m == dataGridView1.Rows.Count - 1)
                    {
                        //单次发(普通模式发)

                        //寄存器数据  checked
                        addr4 = Convert.ToString(addr4_int, 16).PadLeft(4, '0');
                        send_text = "10A0" + addr4 + data5;
                        sendData = strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);
                        // addr4_int++;

                        //开始地址    checked
                        // num_data[row_num] = 3 + data_num3 - duplicate_addr;
                        eeprom_addr3_0_int = addr4_end + 1;
                        eeprom_addr3_1_int = addr4_end + 2;
                        addr3 = dataGridView1.Rows[m + 1 - data_num3].Cells[1].Value.ToString().Replace("0x", ""); //发送地址为此地址段的第一个地址
                        eeprom_addr3_0 = Convert.ToString(eeprom_addr3_0_int, 16).PadLeft(4, '0');
                        eeprom_addr3_1 = Convert.ToString(eeprom_addr3_1_int, 16).PadLeft(4, '0');
                        eeprom_addr3 = new string[2] { eeprom_addr3_0, eeprom_addr3_1 };

                        for (int j = 0; j < 2; j++)
                        {
                            data_3[j] = addr3.Substring(j * 2, 2);
                            send_text3 = "10A0" + eeprom_addr3[j] + data_3[j];
                            sendData = strToHexByte(send_text3);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            System.Threading.Thread.Sleep(10);  //暂停10ms
                        }

                        //发送寄存器个数

                        eeprom_addr2 = Convert.ToString((eeprom_addr3_1_int + 1), 16).PadLeft(4, '0');
                        send_text2 = "10A0" + eeprom_addr2 + Convert.ToString((data_num3 - duplicate_addr), 16).PadLeft(2, '0');
                        sendData = strToHexByte(send_text2);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                        //总修改个数

                        string[] eeprom_addr4 = new string[2] { "0004", "0005" };
                        string[] eeprom_data4 = new string[2];
                        data_4_int = data_4_int + 3 + data_num3 - duplicate_addr;
                        string data_4 = Convert.ToString(data_4_int, 16).PadLeft(4, '0');
                        string send_text4 = null;
                        for (int k = 0; k < 2; k++)
                        {
                            eeprom_data4[k] = data_4.Substring(k * 2, 2);
                            send_text4 = "10A0" + eeprom_addr4[k] + eeprom_data4[k];
                            sendData = strToHexByte(send_text4);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            System.Threading.Thread.Sleep(10);  //暂停10ms

                        }


                    }

                }
                   
               // MessageBox.Show("写入成功");
                        
                if(MessageBox.Show("写入成功") == DialogResult.OK)
                {
                    progressBar1.Visible = false;
                }

                #endregion

                
            }
            else
            {
                MessageBox.Show("串口未打开！");
            }    

        }


        #region 进度条
        private void SetTextMessage(int ipos)
        {
            this.progressBar1.Value = Convert.ToInt32(ipos);
        }

        public void SleepT()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(20);//没什么意思，单纯的执行延时
                SetTextMessage(100 * i / 500);
            }
        }

        //  MessageBox.Show(DateTime.Now.Subtract(dt).ToString());  //循环结束截止时间
        #endregion



        #endregion

        #region EEPROM 配置文件
        private void 保存配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
               Form8_eeprom  f8 = new Form8_eeprom();
               f8.Owner = this;
               f8.Show();
        }
        #endregion

        

    }
}
