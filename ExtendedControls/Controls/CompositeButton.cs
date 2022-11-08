/*
 * Copyright © 2016-2019 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class CompositeButton : Panel
    {
        public CompositeButton()
        {
        }

        public Label Label { get { return Controls.OfType<Label>().FirstOrDefault(); } }
        public ExtButton[] Buttons { get { return Controls.OfType<ExtButton>().ToArray(); } }
        public Panel[] Decals { get { return Controls.OfType<Panel>().ToArray(); } }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            int decalw = 0;
            int decalmaxh = 0;
            foreach (var p in Decals)       // first measure properties - all panels across
            {
                decalw += p.Width + p.Margin.Left + p.Margin.Right;
                decalmaxh = Math.Max(decalmaxh, p.Height);
            }

            int butw = 0;
            int buthmax = 0;
            foreach (var p in Buttons)      // all buttons across
            {
               // System.Diagnostics.Debug.WriteLine("Button " + p.Size);
                butw += p.Width + p.Margin.Left + p.Margin.Right;
                buthmax = Math.Max(buthmax, p.Height);
            }

            int fonth = (int)Font.GetHeight() + 2;       // +2 is to allow for rounding and nerf
            int decalpos = Label.Top + fonth + Padding.Top;
            int butpos = decalpos +  Padding.Top + decalmaxh;
            int hcentre = ClientRectangle.Width / 2;

            if (Label != null)
                Label.Height = decalpos - Label.Top;

            //System.Diagnostics.Debug.WriteLine(Name + " " + ClientRectangle + " panelh " + decalmaxh + " buth " + buthmax + " fonth " + fonth + Padding + " => ");

            int x = hcentre - decalw / 2;
            foreach (var p in Decals)
            {
                p.Location = new Point(x + p.Margin.Left, decalpos);
                x += p.Width + p.Margin.Left + p.Margin.Right;
            }

            x = hcentre - butw / 2;
            foreach (var p in Buttons)
            {
                p.Location = new Point(x + p.Margin.Left, butpos);
                x += p.Width + p.Margin.Left + p.Margin.Right;
            }

            base.OnLayout(levent);
        }

        // used to create a button dynamically

        public static CompositeButton QuickInit(Image backimage,
                                                string text, Font textfont, Color textfore, Color textbackgroundcol,
                                                Image decal, Size decalsize,
                                                Image[] buttons, Size buttonsize,
                                                int padtop,
                                                Action<object, int> ButtonPressed)
        {
            CompositeButton but = new CompositeButton();
            but.Name = text;
            but.SuspendLayout();
            but.BackgroundImage = backimage;
            but.BackgroundImageLayout = ImageLayout.Stretch;

            Label l = new Label();
            l.Text = text;
            l.Font = textfont;
            l.ForeColor = textfore;
            l.Margin = new Padding(0);
            l.BackColor = textbackgroundcol;
            l.AutoSize = false;
            l.Dock = DockStyle.Top;
            l.TextAlign = ContentAlignment.TopCenter;
            l.AutoEllipsis = true;

            but.Controls.Add(l);

            Panel d = new Panel();
            d.BackgroundImage = decal;
            d.BackgroundImageLayout = ImageLayout.Stretch;
            d.BackColor = Color.Transparent;
            d.Size = decalsize;

            but.Controls.Add(d);

            int butno = 0;
            foreach (Image i in buttons)
            {
                ExtButton b = new ExtButton();
                b.Image = i;
                b.Size = buttonsize;
                b.Tag = butno++;
                b.Click += (o, e) => { ExtButton bhit = o as ExtButton; ButtonPressed?.Invoke(but, (int)bhit.Tag); };
                but.Controls.Add(b);
            }

            but.Padding = new Padding(0, padtop, 0, 0);
            but.ResumeLayout();
            return but;
        }

    }
}
