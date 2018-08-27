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
    public partial class Form7_AWB : Form
    {
        public Form1 f1 = new Form1(); //调用f1中的方法

        public Form7_AWB()
        {
            InitializeComponent();
           
        }

        private void Form7_AWB_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            Series series1 = new Series("白点范围");
            Series series2 = new Series("色温点");
            series1.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Point;
            series1.BorderWidth = 3;
            series1.ShadowOffset = 1;
            series1.Color = Color.Red;

            int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
            int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

            int p1_x = p0_x;
            int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

            int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
            int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

            int p2_y = p3_y;
            int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

            int p4_x = p3_x;
            int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

            int p5_y = p0_y;
            int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


            series1.Points.AddXY(p0_x,p0_y);     //P0
            series1.Points.AddXY(p1_x,p1_y);     //P1
            series1.Points.AddXY(p2_x,p2_y);     //P2
            series1.Points.AddXY(p3_x,p3_y);     //P3
            series1.Points.AddXY(p4_x,p4_y);     //P4
            series1.Points.AddXY(p5_x,p5_y);     //P5
            series1.Points.AddXY(p0_x, p0_y);
            chart1.Series.Add(series1);

            series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
            series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
            series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
            series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
            series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
            series1.Points[5].Label = "P5" + "(#VALX,#VAL)";

            
            chart1.ChartAreas[0].AxisX.Maximum = 4096;
            chart1.ChartAreas[0].AxisY.Maximum = 4096;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

            /*
            int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
            int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

            int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
            int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

            int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
            int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

            int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
            int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


            series2.Points.AddXY(CT0_X, CT0_Y);
            series2.Points.AddXY(CT1_X, CT1_Y);
            series2.Points.AddXY(CT2_X, CT2_Y);
            series2.Points.AddXY(CT3_X, CT3_Y);
            chart1.Series.Add(series2);
             
            series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
            series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
            series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
            series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

             */



        }


        #region CT点输入

            #region CT0
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox5.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }
                
            }
            

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox5.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
            #endregion

            #region CT1
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (textBox6.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }
                
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (textBox6.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
            #endregion

            #region CT2

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((textBox3.Text != "") && (textBox7.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }
                
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if ((textBox3.Text != "") && (textBox7.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
            #endregion

            #region CT3

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((textBox4.Text != "") && (textBox8.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));

                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";

                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }
                
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if ((textBox4.Text != "") && (textBox8.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));

                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";

                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }

        #endregion

        #endregion

        #region P0
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox9.Text != "") && (textBox10.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F0D";    //高位寄存器地址
                    string addr3_down = "3F0E";  //地位寄存器地址

                    string data_str = textBox9.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j+1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion


                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox9.Text != "") && (textBox10.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F0F";    //高位寄存器地址
                    string addr3_down = "3F10";  //地位寄存器地址

                    string data_str = textBox10.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion

                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
        #endregion

        #region P3
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox11.Text != "") && (textBox12.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F11";    //高位寄存器地址
                    string addr3_down = "3F12";  //地位寄存器地址

                    string data_str = textBox11.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion

                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox11.Text != "") && (textBox12.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F13";    //高位寄存器地址
                    string addr3_down = "3F14";  //地位寄存器地址

                    string data_str = textBox12.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion
                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }


        #endregion

        #region P12 斜率截距
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox13.Text != "") && (textBox14.Text != "") && (textBox13.Text != "-") && (textBox13.Text != "-0") && (textBox13.Text != "-0."))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";



                    /////////////////////////////////////////////
                    ///////////////发送斜率//////////////////////
                    #region 发送数据 P12

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F15";    //高位寄存器地址
                    string addr3_down = "3F16";  //地位寄存器地址

                    string data_str = textBox13.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int_10 = (int)((data_num * (decimal)1024) - (decimal)0.5);
                    string data_bin = Convert.ToString(data_int_10, 2);

                    if(data_bin.Length < 12)
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

                        for (int i = 0; i < 2; i++)
                        {
                            string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                        }

                        for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                        {
                            if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                            {
                                f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                break;
                            }
                        }

                    }
                    else
                    {
                        //保留后12位
                        string data_bin_12 = data_bin.Substring(data_bin.Length - 12, 12);
                        int data_int = Convert.ToInt32(data_bin_12, 2);
                        int data_h = data_int / 256;
                        string data_16h = Convert.ToString(data_h, 16);  //高八位
                        int data_l = data_int - 256 * data_h;
                        string data_16l = Convert.ToString(data_l, 16);  //低八位

                        string data4_up = data_16h;//高位寄存器数据
                        string data4_down = data_16l;  //低位寄存器数据
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };
                        string[] data4_same = new string[2] { data4_up, data4_down };

                        for (int i = 0; i < 2; i++)
                        {
                            string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                        }

                        for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                        {
                            if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                            {
                                f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                break;
                            }
                        }

                    }
                    
                            #endregion

                        }
                        catch
                        {
                            MessageBox.Show("非法字符，请重新输入");
                        }

              }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox13.Text != "") && (textBox14.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F17";    //高位寄存器地址
                    string addr3_down = "3F18";  //地位寄存器地址

                    string data_str = textBox14.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion

                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }

        #endregion

        #region P45 斜率截距
      
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox15.Text != "") && (textBox16.Text != "") && (textBox15.Text != "-") && (textBox15.Text != "-0") && (textBox15.Text != "-0."))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";



                    /////////////////////////////////////////////
                    ///////////////发送斜率//////////////////////
                    #region 发送数据 P45

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F19";    //高位寄存器地址
                    string addr3_down = "3F1A";  //地位寄存器地址

                    string data_str = textBox15.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int_10 = (int)((data_num * (decimal)1024) - (decimal)0.5);
                    string data_bin = Convert.ToString(data_int_10, 2);
                    
                    if(data_bin.Length < 12)
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

                        for (int i = 0; i < 2; i++)
                        {
                            string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                        }

                        for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                        {
                            if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                            {
                                f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                break;
                            }
                        }

                    }
                    else
                    {//保留后12位
                        string data_bin_12 = data_bin.Substring(data_bin.Length - 12, 12);
                        int data_int = Convert.ToInt32(data_bin_12, 2);
                        int data_h = data_int / 256;
                        string data_16h = Convert.ToString(data_h, 16);  //高八位
                        int data_l = data_int - 256 * data_h;
                        string data_16l = Convert.ToString(data_l, 16);  //低八位

                        string data4_up = data_16h;//高位寄存器数据
                        string data4_down = data_16l;  //低位寄存器数据
                        string[] addr3_same = new string[2] { addr3_up, addr3_down };
                        string[] data4_same = new string[2] { data4_up, data4_down };

                        for (int i = 0; i < 2; i++)
                        {
                            string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                            sendData = f1.strToHexByte(send_text);
                            Form2_serial.sp.Write(sendData, 0, sendData.Length);
                        }

                        for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                        {
                            if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                            {
                                f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                                f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                                break;
                            }
                        }

                    }
                   
                    #endregion

                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            f1 = (Form1)this.Owner; //与主窗口数据联动的
            if ((textBox13.Text != "") && (textBox14.Text != ""))
            {
                try
                {
                    chart1.Series.Clear();
                    Series series1 = new Series("白点范围");
                    Series series2 = new Series("色温点");
                    series1.ChartType = SeriesChartType.Line;
                    series2.ChartType = SeriesChartType.Point;
                    series1.BorderWidth = 3;
                    series1.ShadowOffset = 1;
                    series1.Color = Color.Red;

                    int p0_x = (int)(Convert.ToDecimal(textBox9.Text));
                    int p0_y = (int)(Convert.ToDecimal(textBox10.Text));

                    int p1_x = p0_x;
                    int p1_y = (int)((Convert.ToDecimal(textBox13.Text) * p1_x) + Convert.ToDecimal(textBox14.Text));

                    int p3_x = (int)(Convert.ToDecimal(textBox11.Text));
                    int p3_y = (int)(Convert.ToDecimal(textBox12.Text));

                    int p2_y = p3_y;
                    int p2_x = (int)((p3_y - Convert.ToDecimal(textBox14.Text)) / (Convert.ToDecimal(textBox13.Text)));

                    int p4_x = p3_x;
                    int p4_y = (int)((Convert.ToDecimal(textBox15.Text)) * p4_x + Convert.ToDecimal(textBox16.Text));

                    int p5_y = p0_y;
                    int p5_x = (int)((p5_y - Convert.ToDecimal(textBox16.Text)) / (Convert.ToDecimal(textBox15.Text)));


                    series1.Points.AddXY(p0_x, p0_y);     //P0
                    series1.Points.AddXY(p1_x, p1_y);     //P1
                    series1.Points.AddXY(p2_x, p2_y);     //P2
                    series1.Points.AddXY(p3_x, p3_y);     //P3
                    series1.Points.AddXY(p4_x, p4_y);     //P4
                    series1.Points.AddXY(p5_x, p5_y);     //P5
                    series1.Points.AddXY(p0_x, p0_y);
                    chart1.Series.Add(series1);

                    series1.Points[0].Label = "P0" + "(#VALX,#VAL)";
                    series1.Points[1].Label = "P1" + "(#VALX,#VAL)";
                    series1.Points[2].Label = "P2" + "(#VALX,#VAL)";
                    series1.Points[3].Label = "P3" + "(#VALX,#VAL)";
                    series1.Points[4].Label = "P4" + "(#VALX,#VAL)";
                    series1.Points[5].Label = "P5" + "(#VALX,#VAL)";


                    chart1.ChartAreas[0].AxisX.Maximum = 4096;
                    chart1.ChartAreas[0].AxisY.Maximum = 4096;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 400;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 400;

                    int CT0_X = (int)(Convert.ToDecimal(textBox1.Text));
                    int CT0_Y = (int)(Convert.ToDecimal(textBox5.Text));

                    int CT1_X = (int)(Convert.ToDecimal(textBox2.Text));
                    int CT1_Y = (int)(Convert.ToDecimal(textBox6.Text));

                    int CT2_X = (int)(Convert.ToDecimal(textBox3.Text));
                    int CT2_Y = (int)(Convert.ToDecimal(textBox7.Text));

                    int CT3_X = (int)(Convert.ToDecimal(textBox4.Text));
                    int CT3_Y = (int)(Convert.ToDecimal(textBox8.Text));


                    series2.Points.AddXY(CT0_X, CT0_Y);
                    series2.Points.AddXY(CT1_X, CT1_Y);
                    series2.Points.AddXY(CT2_X, CT2_Y);
                    series2.Points.AddXY(CT3_X, CT3_Y);
                    chart1.Series.Add(series2);

                    series2.Points[0].Label = "CT0" + "(#VALX,#VAL)";
                    series2.Points[1].Label = "CT1" + "(#VALX,#VAL)";
                    series2.Points[2].Label = "CT2" + "(#VALX,#VAL)";
                    series2.Points[3].Label = "CT3" + "(#VALX,#VAL)";

                    #region 发送数据

                    string op1 = "10";        //操作码
                    string slave2 = "60";     //设备码
                    byte[] sendData = null;
                    string addr3_up = "3F1B";    //高位寄存器地址
                    string addr3_down = "3F1C";  //地位寄存器地址

                    string data_str = textBox16.Text.ToString();
                    decimal data_num = Convert.ToDecimal(data_str);
                    int data_int = (int)(data_num);
                    int data_h = data_int / 256;
                    string data_16h = Convert.ToString(data_h, 16);  //高八位
                    int data_l = data_int - 256 * data_h;
                    string data_16l = Convert.ToString(data_l, 16);  //低八位

                    string data4_up = data_16h;//高位寄存器数据
                    string data4_down = data_16l;  //低位寄存器数据
                    string[] addr3_same = new string[2] { addr3_up, addr3_down };
                    string[] data4_same = new string[2] { data4_up, data4_down };

                    for (int i = 0; i < 2; i++)
                    {
                        string send_text = op1 + slave2 + addr3_same[i] + data4_same[i];
                        sendData = f1.strToHexByte(send_text);
                        Form2_serial.sp.Write(sendData, 0, sendData.Length);

                    }

                    for (int j = 0; j < f1.dataGridView1.Rows.Count; j++)  //实时更新主页中的对应寄存器地址
                    {
                        if (f1.dataGridView1.Rows[j].Cells[1].Value.ToString() == "0x" + addr3_up)
                        {
                            f1.dataGridView1.Rows[j].Cells[3].Value = "0x" + data4_up.PadLeft(2, '0');
                            f1.dataGridView1.Rows[j + 1].Cells[3].Value = "0x" + data4_down.PadLeft(2, '0');
                            break;
                        }
                    }

                    #endregion

                }
                catch
                {
                    MessageBox.Show("非法字符，请重新输入");
                }

            }
        }
        #endregion

        
    }
}
