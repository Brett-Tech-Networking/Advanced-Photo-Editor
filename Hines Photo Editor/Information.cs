﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hines_Photo_Editor
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        private void Website_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.Brett-TechRepair.com");
        }

        private void Information_Load(object sender, EventArgs e)
        {

        }
    }
}
