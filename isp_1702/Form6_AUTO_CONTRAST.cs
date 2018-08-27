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
    public partial class Form6_AUTO_CONTRAST : Form
    {
        string xml_FilePath = ""; //xml路径
        public Form1 f1 = new Form1(); //调用f1中的方法

        public Form6_AUTO_CONTRAST()
        {
            InitializeComponent();
           

        }

        private void Form6_AUTO_CONTRAST_Load(object sender, EventArgs e)
        {
            
        }

        #region 选择模式，加载不同数据（斜率地址）
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "彩色")
            {
                dataGridView1.Columns[0].HeaderText = "P(彩色)";
                //加载XML
               
                XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
                xml_FilePath = "Data/PIS1702/AUTO_CONTRAST_color.xml";
                xmlDocument.Load(xml_FilePath);//载入路径  

                try
                {

                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["name"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["X"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["Y"].InnerText; 
                        dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address_X"].InnerText;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address_Y"].InnerText;
                    }
                    dataGridView1.Rows[0].Cells[2].ReadOnly = true;
                    dataGridView1.Rows[2].Cells[2].ReadOnly = true;
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }

                chart1.Series.Clear();
                Series series = new Series("Auto Contrast");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.ShadowOffset = 1;
                series.Color = Color.Red;
  
                series.Points.AddXY(0, 0); //原点

                // for (int i = 0; i < 58; i += 2)
                // {
                //      series.Points.AddY(dataGridView1.Rows[i].Cells[2].Value);
                //  }
                // series.Points.AddY(0); //原点
                // Add series into the chart's series collection    

                int p1_x =  Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                int p1_y = Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value.ToString());
                int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());
                int p3_y = Convert.ToInt32(dataGridView1.Rows[2].Cells[2].Value.ToString()); 

                series.Points.AddXY(p1_x, p1_y);  //p1
                series.Points.AddXY(p2_x, p2_y); //p2
                series.Points.AddXY(p3_x, p3_y);  //p3
                series.Points.AddXY(255, 255);
                chart1.Series.Add(series);

                chart1.Series[0].Points[1].Label = "P1";
                chart1.Series[0].Points[2].Label = "P2";
                chart1.Series[0].Points[3].Label = "P3";
               // chart1.Series[0].Points[2].ToolTip = "bbb";
                chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;
               
            }
            else
            {

                //加载XML
                dataGridView1.Columns[0].HeaderText = "P(黑白)";
                XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
                xml_FilePath = "Data/PIS1702/AUTO_CONTRAST_bw.xml";
                xmlDocument.Load(xml_FilePath);//载入路径  

                try
                {

                    var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                    dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                    foreach (XmlNode xmlNode in node) //遍历所有子节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["name"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["X"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["Y"].InnerText;
                        dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address_X"].InnerText;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address_Y"].InnerText;
                    }
                    dataGridView1.Rows[0].Cells[2].ReadOnly = true;
                    dataGridView1.Rows[2].Cells[2].ReadOnly = true;
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }

                chart1.Series.Clear();
                Series series = new Series("Auto Contrast");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.ShadowOffset = 1;
                series.Color = Color.Blue;
                series.Points.AddXY(0, 0); //原点

                // for (int i = 0; i < 58; i += 2)
                // {
                //      series.Points.AddY(dataGridView1.Rows[i].Cells[2].Value);
                //  }
                // series.Points.AddY(0); //原点
                // Add series into the chart's series collection    
                int p1_x = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                int p1_y = Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value.ToString());
                int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());
                int p3_y = Convert.ToInt32(dataGridView1.Rows[2].Cells[2].Value.ToString());

                series.Points.AddXY(p1_x, p1_y);  //p1
                series.Points.AddXY(p2_x, p2_y); //p2
                series.Points.AddXY(p3_x, p3_y);  //p3
                series.Points.AddXY(255, 255);
                chart1.Series.Add(series);
                chart1.Series[0].Points[1].Label = "P1";
                chart1.Series[0].Points[2].Label = "P2";
                chart1.Series[0].Points[3].Label = "P3";
                
                chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;
            }

        }
        #endregion

       
        #region 修改坐标
        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if (dataGridView1.Columns[0].HeaderText == "P(彩色)")
            {
               

                chart1.Series.Clear();
                Series series = new Series("Auto Contrast");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.ShadowOffset = 1;
                series.Color = Color.Red;
                series.Points.AddXY(0, 0); //原点
                try
                {
                    if(textBox1.Text != "")
                    {
                        Decimal T = Convert.ToDecimal(textBox1.Text.ToString());   //发送时*128   斜率
                        int p1_x = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                        int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                        int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                        int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());

                        int p1_y = (int)(T * p1_x + p2_y - p2_x * T);
                        int p3_y = (int)(T * p3_x + p2_y - p2_x * T);

                        if (p1_y > 255)
                        {
                            p1_y = 255;
                        }
                        if (p3_y > 255)
                        {
                            p3_y = 255;
                        }
                        if (p1_y < 0)
                        {
                            p1_y = 0;
                        }
                        if (p3_y < 0)
                        {
                            p3_y = 0;
                        }

                        series.Points.AddXY(p1_x, p1_y);  //p1
                        series.Points.AddXY(p2_x, p2_y); //p2
                        series.Points.AddXY(p3_x, p3_y);  //p3
                        series.Points.AddXY(255, 255);
                        chart1.Series.Add(series);

                        chart1.Series[0].Points[1].Label = "P1";
                        chart1.Series[0].Points[2].Label = "P2";
                        chart1.Series[0].Points[3].Label = "P3";
                        // chart1.Series[0].Points[2].ToolTip = "bbb";
                        chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                        chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                        chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                        chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;

                        //数据发送
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码

                        byte[] sendData = null;
                        int row_index = dataGridView1.CurrentRow.Index;
                        if (row_index == 0)
                        {
                            string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString()), 16);  //X
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        else if (row_index == 1)
                        {
                            if (dataGridView1.CurrentCell.ColumnIndex == 1)  //X
                            {
                                string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                                string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()), 16);
                                string send_text = op1 + slave2 + addr3 + data4;
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                                for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                                {
                                    if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                    {
                                        f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                        break;
                                    }
                                }
                            }
                            if (dataGridView1.CurrentCell.ColumnIndex == 2)  //Y
                            {
                                string addr3 = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                                string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()), 16);
                                string send_text = op1 + slave2 + addr3 + data4;
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                                for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                                {
                                    if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                    {
                                        f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                        break;
                                    }
                                }
                            }

                        }
                        else if (row_index == 2)
                        {
                            string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString()), 16);  //X
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }
                        }
                    }
                    


                }
                catch
                {
                    MessageBox.Show("非法字符");
                }
                  

            }
            if (dataGridView1.Columns[0].HeaderText == "P(黑白)")
            {
                
                chart1.Series.Clear();
                Series series = new Series("Auto Contrast");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.ShadowOffset = 1;
                series.Color = Color.Blue;
                series.Points.AddXY(0, 0); //原点

                try
                {
                    if(textBox1.Text != "")
                    {
                        Decimal T = Convert.ToDecimal(textBox1.Text.ToString());   //发送时*128   斜率
                        int p1_x = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                        int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                        int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                        int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());

                        int p1_y = (int)(T * p1_x + p2_y - p2_x * T);
                        int p3_y = (int)(T * p3_x + p2_y - p2_x * T);

                        if (p1_y > 255)
                        {
                            p1_y = 255;
                        }
                        if (p3_y > 255)
                        {
                            p3_y = 255;
                        }
                        if (p1_y < 0)
                        {
                            p1_y = 0;
                        }
                        if (p3_y < 0)
                        {
                            p3_y = 0;
                        }

                        series.Points.AddXY(p1_x, p1_y);  //p1
                        series.Points.AddXY(p2_x, p2_y); //p2
                        series.Points.AddXY(p3_x, p3_y);  //p3
                        series.Points.AddXY(255, 255);
                        chart1.Series.Add(series);

                        chart1.Series[0].Points[1].Label = "P1";
                        chart1.Series[0].Points[2].Label = "P2";
                        chart1.Series[0].Points[3].Label = "P3";
                        // chart1.Series[0].Points[2].ToolTip = "bbb";
                        chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                        chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                        chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                        chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;

                        //数据发送
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码

                        byte[] sendData = null;
                        int row_index = dataGridView1.CurrentRow.Index;
                        if (row_index == 0)
                        {
                            string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString()), 16);  //X
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }
                        }
                        else if (row_index == 1)
                        {
                            if (dataGridView1.CurrentCell.ColumnIndex == 1)  //X
                            {
                                string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                                string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()), 16);
                                string send_text = op1 + slave2 + addr3 + data4;
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                                for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                                {
                                    if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                    {
                                        f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                        break;
                                    }
                                }
                            }
                            if (dataGridView1.CurrentCell.ColumnIndex == 2)  //Y
                            {
                                string addr3 = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                                string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentCell.Value.ToString()), 16);
                                string send_text = op1 + slave2 + addr3 + data4;
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                                for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                                {
                                    if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                    {
                                        f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                        break;
                                    }
                                }
                            }

                        }
                        else if (row_index == 2)
                        {
                            string addr3 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            string data4 = Convert.ToString(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString()), 16);  //X
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }
                        }
                    }
                   
                }
                catch
                {
                    MessageBox.Show("非法字符");
                }
            }

        }

        #endregion

        #region 斜率
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                f1 = (Form1)this.Owner; //与主窗口数据联动的
                if (dataGridView1.Columns[0].HeaderText == "P(彩色)")
                {

                    chart1.Series.Clear();
                    Series series = new Series("Auto Contrast");
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 3;
                    series.ShadowOffset = 1;
                    series.Color = Color.Red;
                    series.Points.AddXY(0, 0); //原点
                    try
                    {
                        if(textBox1.Text != "")
                        {
                            Decimal T = Convert.ToDecimal(textBox1.Text.ToString());   //发送时*128   斜率
                            int p1_x = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                            int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                            int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                            int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());

                            int p1_y = (int)(T * p1_x + p2_y - p2_x * T);
                            int p3_y = (int)(T * p3_x + p2_y - p2_x * T);
                            
                            if(p1_y > 255)
                            {
                                p1_y = 255;
                            }
                            if (p3_y > 255)
                            {
                                p3_y = 255;
                            }
                            if (p1_y < 0)
                            {
                                p1_y = 0;
                            }
                            if (p3_y < 0)
                            {
                                p3_y = 0;
                            }

                            dataGridView1.Rows[0].Cells[2].Value = p1_y;   //修改斜率刷新dgv显示
                            dataGridView1.Rows[2].Cells[2].Value = p3_y;

                            series.Points.AddXY(p1_x, p1_y);  //p1
                            series.Points.AddXY(p2_x, p2_y); //p2
                            series.Points.AddXY(p3_x, p3_y);  //p3
                            series.Points.AddXY(255, 255);
                            chart1.Series.Add(series);

                            chart1.Series[0].Points[1].Label = "P1";
                            chart1.Series[0].Points[2].Label = "P2";
                            chart1.Series[0].Points[3].Label = "P3";
                            // chart1.Series[0].Points[2].ToolTip = "bbb";
                            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;

                            //数据发送
                            string op1 = "10";        //操作码
                            string slave2 = "60";     //设备码
                            string addr3 = "4903";
                            byte[] sendData = null;
                            string data4 = Convert.ToString((int)(T * 128), 16);
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }

                    }
                    catch
                    {
                        MessageBox.Show("非法字符或串口未打开，发送失败");
                    }


                }
                if (dataGridView1.Columns[0].HeaderText == "P(黑白)")
                {

                    chart1.Series.Clear();
                    Series series = new Series("Auto Contrast");
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 3;
                    series.ShadowOffset = 1;
                    series.Color = Color.Blue;
                    series.Points.AddXY(0, 0); //原点

                    try
                    {
                        if(textBox1.Text != "")
                        {
                            Decimal T = Convert.ToDecimal(textBox1.Text.ToString());   //发送时*128   斜率
                            int p1_x = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value.ToString());
                            int p2_x = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value.ToString());
                            int p2_y = Convert.ToInt32(dataGridView1.Rows[1].Cells[2].Value.ToString());
                            int p3_x = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value.ToString());

                            int p1_y = (int)(T * p1_x + p2_y - p2_x * T);
                            int p3_y = (int)(T * p3_x + p2_y - p2_x * T);

                            if (p1_y > 255)
                            {
                                p1_y = 255;
                            }
                            if (p3_y > 255)
                            {
                                p3_y = 255;
                            }
                            if (p1_y < 0)
                            {
                                p1_y = 0;
                            }
                            if (p3_y < 0)
                            {
                                p3_y = 0;
                            }

                            dataGridView1.Rows[0].Cells[2].Value = p1_y;   //修改斜率刷新dgv显示
                            dataGridView1.Rows[2].Cells[2].Value = p3_y;

                            series.Points.AddXY(p1_x, p1_y);  //p1
                            series.Points.AddXY(p2_x, p2_y); //p2
                            series.Points.AddXY(p3_x, p3_y);  //p3
                            series.Points.AddXY(255, 255);
                            chart1.Series.Add(series);

                            chart1.Series[0].Points[1].Label = "P1";
                            chart1.Series[0].Points[2].Label = "P2";
                            chart1.Series[0].Points[3].Label = "P3";
                            // chart1.Series[0].Points[2].ToolTip = "bbb";
                            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
                            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 10;

                            //数据发送
                            string op1 = "10";        //操作码
                            string slave2 = "60";     //设备码
                            string addr3 = "4904";
                            byte[] sendData = null;
                            string data4 = Convert.ToString((int)(T * 128), 16);
                            string send_text = op1 + slave2 + addr3 + data4;
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3)
                                {
                                    f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                       
                    }
                    catch
                    {
                        MessageBox.Show("非法字符,斜率不能为空");
                    }
                }

            }
            
        }
        #endregion

        
    
        
       
    }

    
}
