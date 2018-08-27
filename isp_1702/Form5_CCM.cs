using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace isp_1702
{
    public partial class Form5_CCM : Form
    {
        string xml_FilePath = ""; //xml路径
        XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
        public Form1 f1 = new Form1(); //调用f1中的方法
        public Form5_CCM()
        {
            InitializeComponent();
        }

        #region 加载
        public void Form5_CCM_Load(object sender, EventArgs e)
        {
            // XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/CCM_null.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  

            try
            {

                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }
        }
        #endregion

        #region 合并单元格
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // if((dataGridView1.Rows[1].Cells[0].Value != dataGridView1.Rows[2].Cells[0].Value) || (dataGridView1.Rows[3].Cells[0].Value != dataGridView1.Rows[4].Cells[0].Value) || )
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
                        //   如果下一行和当前行的数据不同，或者行索引为1、3时，则在当前的单元格画一条底边线
                        if ((e.RowIndex < dataGridView1.Rows.Count - 1 && dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString()) || e.RowIndex == 1 || e.RowIndex == 3)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //画最后一条记录的底线 
                        if (e.RowIndex == dataGridView1.Rows.Count - 1)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left + 2, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //画右边线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                        // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                        if (e.Value != null)
                        {
                            if (e.RowIndex > 0 && dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString() && e.RowIndex != 2 && e.RowIndex != 4)
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

        #region 修改值直接发送
        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            /*
            int row_index = dataGridView1.CurrentRow.Index;
            int column_index = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.Rows[row_index + 1].Cells[column_index].Value = dataGridView1.CurrentCell.Value;   //保持合并的单元格内值相同。
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                if (dataGridView1.CurrentCell.Value.ToString() != "")
                {
                    //修改数据后发送到寄存器
                    try
                    {
                        int i = dataGridView1.CurrentCell.RowIndex;
                        int j;
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[i].Cells[column_index + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[i + 1].Cells[column_index + 3].Value.ToString();  //低位寄存器地址

                        string data_str = dataGridView1.Rows[i].Cells[column_index].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(data_num * 1024);
                        int data_h = data_1024 / 256;
                        string data_16h = Convert.ToString(data_h, 16);  //高八位
                        int data_l = data_1024 - 256 * data_h;
                        string data_16l = Convert.ToString(data_l, 16);  //低八位


                        string data4_up = data_16h;//高位寄存器数据
                        string data4_down = data_16l;  //低位寄存器数据
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };
                        string[] data4_same = new string[2] { data4_up, data4_down };

                        for (j = 0; j < 2; j++)
                        {
                            string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
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
            */

        

    } //end  dataGridView1_CellValidated

        #endregion

        #region 单选框切换
        //CCM0
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            // XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/CCM0.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  
            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }


            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址
                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if (data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位
                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }

        }
        //CCM1
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            // XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/CCM1.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  
            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }

            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址
                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if (data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位
                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }
        }
        //CCM2
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            // XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/CCM2.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  
            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }

            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址
                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if (data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位
                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }
        }
        //CCM3
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            // XmlDocument xmlDocument = new XmlDocument();//新建一个xml编辑器(实例化)
            xml_FilePath = "Data/PIS1702/CCM3.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  
            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }

            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址
                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if (data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位
                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }

        }



        #endregion

        #region 还原键
        private void button3_Click(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            xml_FilePath = "Data/PIS1702/CCM_org.xml";
            xmlDocument.Load(xml_FilePath);//载入路径  
            try
            {
                var node = xmlDocument.SelectSingleNode("chip/Module"); //定位
                dataGridView1.Rows.Clear();//清空datagridview1，防止和上次的处理数据混乱
                foreach (XmlNode xmlNode in node) //遍历所有子节点
                {
                    XmlElement xmlElement = (XmlElement)xmlNode; //将子节点的类型转换为XmlElement
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                    dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                    dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                    dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                    dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                    dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;

                }
            }
            catch
            {
                MessageBox.Show("XML格式不对");
            }

            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址

                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(data_num * 1024);
                        int data_h = data_1024 / 256;
                        string data_16h = Convert.ToString(data_h, 16);  //高八位
                        int data_l = data_1024 - 256 * data_h;
                        string data_16l = Convert.ToString(data_l, 16);  //低八位

                        string data4_up = data_16h;//高位寄存器数据
                        string data4_down = data_16l;  //低位寄存器数据
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };
                        string[] data4_same = new string[2] { data4_up, data4_down };

                        for (int j = 0; j < 2; j++)
                        {
                            string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);

                        }

                        for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                        {
                            if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                            {
                                f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                break;
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }
        }


        #endregion

        #region 发送
        private void button1_Click(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            int row_index = dataGridView1.CurrentRow.Index;
            int column_index = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.Rows[row_index + 1].Cells[column_index].Value = dataGridView1.CurrentCell.Value;   //保持合并的单元格内值相同。
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                if (dataGridView1.CurrentCell.Value.ToString() != "")
                {
                    //修改数据后发送到寄存器
                    try
                    {
                        int i = dataGridView1.CurrentCell.RowIndex;
                        int j;
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[i].Cells[column_index + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[i + 1].Cells[column_index + 3].Value.ToString();  //低位寄存器地址

                        string data_str = dataGridView1.Rows[i].Cells[column_index].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if(data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);

                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }

                            for (int k = 0; k < f1.dataGridView1.Rows.Count; k++)  //实时更新主页中的对应寄存器地址
                            {
                                if (f1.dataGridView1.Rows[k].Cells[1].Value.ToString() == "0x" + addr3_up)
                                {
                                    f1.dataGridView1.Rows[k].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                    f1.dataGridView1.Rows[k + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                    break;
                                }
                            }

                        }
                        
                    }
                    catch
                    {
                        MessageBox.Show("发送失败");
                    }
                }

            }



        }
        #endregion

        #region 禁用回车键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && this.dataGridView1.CurrentCell.IsInEditMode)
            {
                var curcell = dataGridView1.CurrentCell;
                //判断当前单元格不是最后一列
                if (curcell.ColumnIndex < dataGridView1.ColumnCount - 1)
                {
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 保存配置表
        private void button4_Click(object sender, EventArgs e)
        {
            XmlDeclaration Declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            if (xml_FilePath != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了  
            {
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_chip = xmlDocument.SelectSingleNode("chip");//找到<chip>作为根节点 
                XmlElement xmlElement_module = xmlDocument.CreateElement("Module");//创建一个<Module>节点  
                xmlElement_chip.RemoveAll();//删除旗下所有节点  
                int row = dataGridView1.Rows.Count;//得到总行数      
                int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数      
                for (int i = 0; i < row; i++)//遍历这个dataGridView  
                {

                    XmlElement xmlElement_reg = xmlDocument.CreateElement("reg");//创建<name>节点  
                    XmlAttribute Address00 = xmlDocument.CreateAttribute("Address00"); //创建节点属性 Address
                    Address00.InnerText = dataGridView1.Rows[i].Cells[3].Value.ToString();//其文本就是第4个单元格的内容

                    XmlAttribute Address01 = xmlDocument.CreateAttribute("Address01"); //创建节点属性 Address
                    Address01.InnerText = dataGridView1.Rows[i].Cells[4].Value.ToString();//其文本就是第5个单元格的内容

                    XmlAttribute Address02 = xmlDocument.CreateAttribute("Address02"); //创建节点属性 Address
                    Address02.InnerText = dataGridView1.Rows[i].Cells[5].Value.ToString();//其文本就是第6个单元格的内容

                    XmlAttribute hex_00 = xmlDocument.CreateAttribute("hex_00"); //创建节点属性 hex_00
                    hex_00.InnerText = dataGridView1.Rows[i].Cells[0].Value.ToString();//其文本就是第1个单元格的内容

                    XmlAttribute hex_01 = xmlDocument.CreateAttribute("hex_01"); //创建节点属性 hex_01
                    hex_01.InnerText = dataGridView1.Rows[i].Cells[1].Value.ToString();//其文本就是第2个单元格的内容

                    XmlAttribute hex_02 = xmlDocument.CreateAttribute("hex_02"); //创建节点属性 hex_02
                    hex_02.InnerText = dataGridView1.Rows[i].Cells[2].Value.ToString();//其文本就是第3个单元格的内容



                    xmlElement_reg.Attributes.Append(Address00);
                    xmlElement_reg.Attributes.Append(Address01);
                    xmlElement_reg.Attributes.Append(Address02);
                    xmlElement_reg.Attributes.Append(hex_00);
                    xmlElement_reg.Attributes.Append(hex_01);
                    xmlElement_reg.Attributes.Append(hex_02);


                    xmlElement_module.AppendChild(xmlElement_reg);//将这个<reg>节点放到<module>下方 

                }
                xmlElement_chip.AppendChild(xmlElement_module);//将这个<module>节点放到<chip>下方 
                                                               
                xmlDocument.AppendChild(xmlElement_chip);//将这个<chip>附到总文件头，而且设置为根结点

                SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框  
                saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名  
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径  
                {
                    // xmlDocument.Save(xml_FilePath);//保存这个xml  
                    xmlDocument.Save(saveFileDialog1.FileName);//保存这个xml文件  
                }
            }
        }
        #endregion

        #region 读取配置表
        private void button2_Click(object sender, EventArgs e)
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
                        dataGridView1.Rows[index].Cells[0].Value = xmlNode.Attributes["hex_00"].InnerText;
                        dataGridView1.Rows[index].Cells[1].Value = xmlNode.Attributes["hex_01"].InnerText;
                        dataGridView1.Rows[index].Cells[2].Value = xmlNode.Attributes["hex_02"].InnerText;
                        dataGridView1.Rows[index].Cells[3].Value = xmlNode.Attributes["Address00"].InnerText;
                        dataGridView1.Rows[index].Cells[4].Value = xmlNode.Attributes["Address01"].InnerText;
                        dataGridView1.Rows[index].Cells[5].Value = xmlNode.Attributes["Address02"].InnerText;
                    }
                    MessageBox.Show("导入成功");
                }
                catch
                {
                    MessageBox.Show("XML格式不对");
                }
            }
            else
            {
                MessageBox.Show("请打开XML文件");
            }

            for (int m = 0; m < 6; m += 2)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        string op1 = "10";        //操作码
                        string slave2 = "60";     //设备码
                        byte[] sendData = null;
                        string addr3_up = dataGridView1.Rows[m].Cells[n + 3].Value.ToString();//高位寄存器地址
                        string addr3_down = dataGridView1.Rows[m + 1].Cells[n + 3].Value.ToString();  //低位寄存器地址
                        string data_str = dataGridView1.Rows[m].Cells[n].Value.ToString();
                        decimal data_num = Convert.ToDecimal(data_str);
                        int data_1024 = (int)(Math.Round(data_num * (decimal)1024));
                        string data_bin = Convert.ToString(data_1024, 2);

                        if (data_bin.Length < 15)
                        {
                            string data_bin_12 = data_bin.Substring(0, data_bin.Length);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位
                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }
                        }
                        else
                        {
                            //保留后15位
                            string data_bin_12 = data_bin.Substring(data_bin.Length - 15, 15);
                            int data_int = Convert.ToInt32(data_bin_12, 2);
                            int data_h = data_int / 256;
                            string data_16h = Convert.ToString(data_h, 16);  //高八位
                            int data_l = data_int - 256 * data_h;
                            string data_16l = Convert.ToString(data_l, 16);  //低八位

                            string data4_up = data_16h;//高位寄存器数据
                            string data4_down = data_16l;  //低位寄存器数据
                            string[] addr3_same = new string[2] { addr3_up, addr3_down };
                            string[] data4_same = new string[2] { data4_up, data4_down };

                            for (int j = 0; j < 2; j++)
                            {
                                string send_text = op1 + slave2 + addr3_same[j] + data4_same[j];
                                sendData = f1.strToHexByte(send_text);
                                Form2_serial.sp.Write(sendData, 0, sendData.Length);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口未打开");
                    }
                }
            }

        }
        #endregion



    }

}
