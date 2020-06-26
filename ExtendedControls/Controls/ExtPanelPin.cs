﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelPin : Panel
    {
        public ExtPanelPin()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
        }

        public ExtPanelPin(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private int opacity = 50;
        [DefaultValue(50)]
        public int Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("value must be between 0 and 100");
                this.opacity = value;
            }
        }
                
        private const int WS_EX_TRANSPARENT = 0x20;
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;        
                cp.ExStyle |= WS_EX_TRANSPARENT;    // Turn on transparencies
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }

}

