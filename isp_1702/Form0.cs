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
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
            

        }

        private void Form0_Load(object sender, EventArgs e)
        {
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //计算窗体显示的坐标值，可以根据需要微调几个像素
            int x = ScreenWidth - this.Width - 5;
            int y = ScreenHeight - this.Height - 5;

            this.Location = new Point(x, y);


        }






    }

    
}
