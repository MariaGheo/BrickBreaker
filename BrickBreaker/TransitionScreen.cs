﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class TransitionScreen : UserControl
    {
      //  Image transitionBackgroundImage = Properties.Resources.instructionsBackground2;
        public TransitionScreen()
        {
            InitializeComponent();
        }

        private void TransitionScreen_Load(object sender, EventArgs e)
        {

        }

        private void TransitionScreen_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.DrawImageUnscaled(transitionBackgroundImage, 0, 0);
        }
    }
}
