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
    public partial class progressBar : Form
    {
        public progressBar()
        {
            InitializeComponent();
        }

        public void SetNotifyInfo(int percent, string message)
        {
            this.label1.Text = message;
            this.progressBar1.Value = percent;
        }
    }
}
