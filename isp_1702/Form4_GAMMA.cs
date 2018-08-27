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
using System.Windows.Forms.DataVisualization.Charting;

namespace isp_1702
{
    public partial class Form4_GAMMA : Form
    {
        string xml_FilePath = ""; //xml路径
        public Form1 f1 = new Form1(); //调用f1中的方法
        

        public delegate void EventHandler(string form4_sendtxt1, string form4_sendtxt2);
        public EventHandler TxtChangeEvent;
        public Form4_GAMMA()
        {
            InitializeComponent();
        }

        #region 加载窗口时加载gdv配置文件,加载曲线图
        private void Form4_GAMMA_Load(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/GAMMA.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  

            try
            {
                //var node = xmlDocument.SelectSingleNode("chip/Module[@Name='GAMMA_X']"); //定位
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }

            #region 加载曲线图


            chart1.Series.Clear();
            
            Series series = new Series("Gamma");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;

            this.chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;


            // Populate new series with data   
             series.Points.AddXY(0, 0); //原点

            for (int i = 0; i < 58; i += 2)
            {
                series.Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
                
                
            }
            // series.Points.AddY(0); //原点
            // Add series into the chart's series collection    
            // chart1.ChartAreas[0].AxisX.Maximum = 4096;
           // chart1.ChartAreas[0].AxisY.Maximum = 4096;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.Series.Add(series);
           // chart1.ChartAreas[0].AxisX.Maximum = 4000;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 100;
            
            





            #endregion

        }
        #endregion

        #region 合并单元格
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex != -1)
            {
                using (Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
                       backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // 清除单元格
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        // 画 Grid 边线（仅画单元格的底边线和右边线）
                        //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                        if (e.RowIndex < dataGridView1.Rows.Count - 1 && dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //画最后一条记录的底线 
                        if (e.RowIndex == dataGridView1.Rows.Count - 1)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left + 2, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //画右边线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                        // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                        if (e.Value != null)
                        {
                            if (e.RowIndex > 0 && dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
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

        #region 滑动条
 
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner;
            //滑动条控制Y10进制，然后将十进制数存到两个8bit寄存器内。
            // (1)Y右移八位(/256) --> Yh(高8位)  Yh转16进制--> Yh_16 存到 pid ！=“”的单元格内
            // (2)Y-(256*Yh)-->Yl(低8位), Yl转16进制-->Yl_16  存到 pid =“”的单元格内
            int num_row = this.dataGridView1.CurrentCell.RowIndex;
            int Y = trackBar1.Value;
            int Yh, Yl;
            string Yh_16, Yl_16;

            string Y_str = Convert.ToString(Y);
            
            //dataGridView1.CurrentRow.Cells[2].Value = Y_str;

            Yh = Y / 256;   //测试不能被256整除的情况**********
            Yh_16 = Convert.ToString(Yh, 16);  //高八位
            Yl = Y - 256 * Yh;
            Yl_16 = Convert.ToString(Yl, 16);  //低八位

            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() != "") //当前选中行(高八位)
            { 
                dataGridView1.Rows[num_row].Cells[3].Value = Yh_16;
                dataGridView1.Rows[num_row].Cells[2].Value = Y_str;   //合并单元格需要string格式
                dataGridView1.Rows[num_row + 1].Cells[3].Value = Yl_16;
                dataGridView1.Rows[num_row + 1].Cells[2].Value = Y_str;

                try
                {
                    //发送数据到寄存器
                    int i;
                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = dataGridView1.CurrentRow.Cells[4].Value.ToString();           //高位寄存器地址
                    string addr3_down = dataGridView1.Rows[num_row + 1].Cells[4].Value.ToString();  //低位寄存器地址
                    string data4_up = dataGridView1.CurrentRow.Cells[3].Value.ToString();           //高位寄存器数据
                    string data4_down = dataGridView1.Rows[num_row + 1].Cells[3].Value.ToString();  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (i = 0; i < 2; i++)
                    {
                        string addr3_same_0x = addr3_same[i].Replace("0x", "");
                        string data4_same_0x = data4_same[i].Replace("0x", "");
                        string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);


                    }
                   // TxtChangeEvent(data4_up, data4_down);
                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if(f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2,'0');
                            f1.dataGridView1.Rows[j+1].Cells[3].Value = "0x" + data4_down.PadLeft(2,'0');
                            break;
                        }
                        
                    }



                }
               catch
                {
                    MessageBox.Show("串口未打开");
                }

            }
            else  //当前选中行(两个bit数值的第二个，低八位)
            {
                dataGridView1.Rows[num_row].Cells[3].Value = Yl_16;
                dataGridView1.Rows[num_row].Cells[2].Value = Y_str;
                dataGridView1.Rows[num_row - 1].Cells[3].Value = Yh_16;
                dataGridView1.Rows[num_row - 1].Cells[2].Value = Y_str;

                try
                {
                    //发送数据到寄存器
                    int i;
                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = dataGridView1.Rows[num_row - 1].Cells[4].Value.ToString();//高位寄存器地址
                    string addr3_down = dataGridView1.Rows[num_row].Cells[4].Value.ToString();  //低位寄存器地址
                    string data4_up = dataGridView1.Rows[num_row - 1].Cells[3].Value.ToString();//高位寄存器数据
                    string data4_down = dataGridView1.Rows[num_row].Cells[3].Value.ToString();  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (i = 0; i < 2; i++)
                    {
                        string addr3_same_0x = addr3_same[i].Replace("0x", "");
                        string data4_same_0x = data4_same[i].Replace("0x", "");
                        string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }
                    //TxtChangeEvent(data4_up, data4_down);
                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("串口未打开");
                }
            }

            //发送数据





            #region 加载曲线图


            chart1.Series.Clear();
            Series series = new Series("Gamma");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;
            // Populate new series with data 
            series.Points.AddXY(0, 0); //原点

            for (int i = 0; i < 58; i += 2)
            {
                series.Points.AddY(dataGridView1.Rows[i].Cells[2].Value);
            }

            // Add series into the chart's series collection             
            chart1.Series.Add(series);
            #endregion

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            /*
            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() != "") //根据寄存器所占位数限制滑动条范围以及初始位置
            {
                trackBar1.Maximum = 16; //4位，2^4
                trackBar1.Value = Int32.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString(), System.Globalization.NumberStyles.HexNumber);  //现将字符串转成10进制数字
                
            }
            else
            {
                trackBar1.Maximum = 255; //8位，2^8
                trackBar1.Value = Int32.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString(), System.Globalization.NumberStyles.HexNumber);  //现将字符串转成10进制数字
                
            }
            */
            
            trackBar1.Maximum = 4096; //2的11次方
            trackBar1.Value = Int32.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString(), System.Globalization.NumberStyles.Number);  //现将字符串转成10进制数字;
        }

        #endregion

        #region 重置
        private void button2_Click_1(object sender, EventArgs e)
        {
            #region 重新加载XML

            XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/GAMMA.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  

            try
            {
                //var node = xmlDocument.SelectSingleNode("chip/Module[@Name='GAMMA_X']"); //定位
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }
            #endregion

            #region 重新加载曲线图

            chart1.Series.Clear();
            Series series = new Series("Gamma");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;
            // Populate new series with data   
            series.Points.AddXY(0, 0); //原点

            for (int i = 0; i < 58; i += 2)
            {
                series.Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
            }

            // Add series into the chart's series collection             
            chart1.Series.Add(series);
            #endregion

        }
        #endregion

        #region 读取按钮
        private void button4_Click_1(object sender, EventArgs e)
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
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }

                #region 加载曲线图


                chart1.Series.Clear();
                    Series series = new Series("Gamma");
                    series.ChartType = SeriesChartType.Spline;
                    series.BorderWidth = 3;
                    series.ShadowOffset = 2;
                    // Populate new series with data   
                    series.Points.AddXY(0, 0); //原点

                    for (int i = 0; i < 58; i += 2)
                    {
                    series.Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
                     }
                    // series.Points.AddY(0); //原点
                    // Add series into the chart's series collection             
                    chart1.Series.Add(series);
                #endregion


                MessageBox.Show("导入成功");
            }
 
            else
            {
                MessageBox.Show("请打开XML文件");
            }
        }
        #endregion

        #region  保存按钮
        private void button3_Click(object sender, EventArgs e)
        {
            
            XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”  
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>  
            XmlDeclaration Declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
          //  if (xml_FilePath != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了  
          //  {
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_chip = xmlDocument.SelectSingleNode("chip");//找到<chip>作为根节点 
                XmlElement xmlElement_module = xmlDocument.CreateElement("Module");//创建一个<Module>节点  
                xmlElement_chip.RemoveAll();//删除旗下所有节点  
                int row = dataGridView1.Rows.Count;//得到总行数      
                int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数      
                for (int i = 0; i < row; i++)//遍历这个dataGridView  
                {
                    
                    XmlElement xmlElement_reg = xmlDocument.CreateElement("reg");//创建<name>节点  
                    XmlAttribute Address = xmlDocument.CreateAttribute("Address"); //创建节点属性 Address
                    Address.InnerText = dataGridView1.Rows[i].Cells[4].Value.ToString();//其文本就是第4个单元格的内容

                    XmlAttribute X = xmlDocument.CreateAttribute("x"); //创建节点属性 x
                    X.InnerText = dataGridView1.Rows[i].Cells[1].Value.ToString();//其文本就是第1个单元格的内容

                    XmlAttribute Y = xmlDocument.CreateAttribute("y"); //创建节点属性 y
                    Y.InnerText = dataGridView1.Rows[i].Cells[2].Value.ToString();//其文本就是第2个单元格的内容

                    XmlAttribute pid = xmlDocument.CreateAttribute("pid"); //创建节点属性 pid
                    pid.InnerText = dataGridView1.Rows[i].Cells[0].Value.ToString();//其文本就是第0个单元格的内容

                    XmlAttribute pid2 = xmlDocument.CreateAttribute("pid2"); //创建节点属性 pid2
                    pid2.InnerText = dataGridView1.Rows[i].Cells[5].Value.ToString();//其文本就是第5个单元格的内容

                    xmlElement_reg.Attributes.Append(Address);
                    xmlElement_reg.Attributes.Append(X);
                    xmlElement_reg.Attributes.Append(Y);
                    xmlElement_reg.Attributes.Append(pid);
                    xmlElement_reg.Attributes.Append(pid2);

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
                }
          //  }
           
        }

        #endregion

        #region  整个配置表发送到寄存器
        private void button1_Click(object sender, EventArgs e)
        {
            //将dgv中的数据发送到寄存器
            int row = dataGridView1.Rows.Count; //得到总行数
            string Y_str;
            int Y,Yh, Yl;
            string Yh_16, Yl_16;

            for (int i = 0; i < row - 1; i+=2)//遍历这个dataGridView  
            {
                Y_str = dataGridView1.Rows[i].Cells[2].Value.ToString();
                Y = Convert.ToInt32(Y_str);
               
                Yh = (Y/256);   //测试不能被256整除的情况**********
                Yh_16 = Convert.ToString(Yh, 16);  //高八位
                Yl = Y - 256 * Yh;
                Yl_16 = Convert.ToString(Yl, 16);  //低八位

                dataGridView1.Rows[i].Cells[3].Value = Yh_16;
                dataGridView1.Rows[i+1].Cells[3].Value = Yl_16;
                try
                {
                    //发送数据到寄存器
                    int j;
                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = dataGridView1.Rows[i].Cells[4].Value.ToString();//高位寄存器地址
                    string addr3_down = dataGridView1.Rows[i+1].Cells[4].Value.ToString();  //低位寄存器地址
                    string data4_up = dataGridView1.Rows[i].Cells[3].Value.ToString();//高位寄存器数据
                    string data4_down = dataGridView1.Rows[i+1].Cells[3].Value.ToString();  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (j = 0; j < 2; j++)
                    {
                        string addr3_same_0x = addr3_same[j].Replace("0x", "");
                        string data4_same_0x = data4_same[j].Replace("0x", "");
                        string send_text = op1 + slave2 + addr3_same_0x + data4_same_0x;
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }
                }
                catch
                {
                    MessageBox.Show("串口未打开");
                }
            }

        }
        #endregion

        #region gamma 选择项
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            if (comboBox1.SelectedItem.ToString() == "0.67")
            {
                xml_FilePath = "Data/PIS1702/GAMMA_0.67.xml";
                xmlDocument.Load(xml_FilePath);//载入路径  
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }

                
            }
            else if(comboBox1.SelectedItem.ToString() == "0.4")
            {
                xml_FilePath = "Data/PIS1702/GAMMA_0.4.xml";
                xmlDocument.Load(xml_FilePath);//载入路径
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "0.2")
            {
                xml_FilePath = "Data/PIS1702/GAMMA_0.2.xml";
                xmlDocument.Load(xml_FilePath);//载入路径
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "0.1")
            {
                xml_FilePath = "Data/PIS1702/GAMMA_0.1.xml";
                xmlDocument.Load(xml_FilePath);//载入路径
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }
            }
            else
            {
                xml_FilePath = "Data/PIS1702/GAMMA_0.04.xml";
                xmlDocument.Load(xml_FilePath);//载入路径
                try
                {
                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["pid"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["x"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["y"].InnerText; ;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["pid2"].InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }
            }

            #region 加载曲线图

            chart1.Series.Clear();
            Series series = new Series("Gamma");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.ShadowOffset = 1;
            series.Points.AddY(0);
            this.chart1.ChartAreas[0].AxisX.IsMarginVisible = false;

            for (int i = 0; i < 58; i += 2)
            {
                series.Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
            }
            chart1.Series.Add(series);

            #endregion

        }
        #endregion
    }
}
