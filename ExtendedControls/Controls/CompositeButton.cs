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

        protected override void OnLayout(LayoutEventArgs levent)
        {
            Panel[] panels = Controls.OfType<Panel>().ToArray();
            ExtButton[] buttons = Controls.OfType<ExtButton>().ToArray();
            Label lab = Controls.OfType<Label>().FirstOrDefault();

            int panelw = 0;
            int panelhmax = 0;
            foreach (var p in panels)       // first measure properties - all panels across
            {
                panelw += p.Width + p.Margin.Left + p.Margin.Right;
                panelhmax = Math.Max(panelhmax, p.Height);
            }

            int butw = 0;
            int buthmax = 0;
            foreach (var p in buttons)      // all buttons across
            {
                butw += p.Width + p.Margin.Left + p.Margin.Right;
                buthmax = Math.Max(buthmax, p.Height);
            }

            int fonth = (int)Font.GetHeight() +2;       // +2 is to allow for rounding and nerf
            int neededh = fonth + Padding.Top + panelhmax + Padding.Top + buthmax + Padding.Bottom;     // height needed

            int vcentre = neededh / 2;
            int hcentre = ClientRectangle.Width / 2;

           // System.Diagnostics.Debug.WriteLine("panelh " + panelhmax + " buth " + buthmax + " fonth " + fonth + " => " + neededh + " " + vcentre);

            if (lab != null)
            {
                lab.Dock = DockStyle.Top;
                lab.AutoSize = false;
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Height = vcentre - panelhmax/2;
            }

            int x = hcentre - panelw / 2;
            foreach (var p in panels)
            {
                p.Location = new Point(x + p.Margin.Left, vcentre - p.Height / 2);
                x += p.Width + p.Margin.Left + p.Margin.Right;
            }

            x = hcentre - butw / 2;
            foreach (var p in buttons)
            {
                p.Location = new Point(x + p.Margin.Left, vcentre + panelhmax/2 + Padding.Top + buthmax / 2 - p.Height / 2);
                x += p.Width + p.Margin.Left + p.Margin.Right;
            }

            base.OnLayout(levent);
        }

        // used to create a button dynamically

        public static CompositeButton QuickInit(Image backimage,
                                                string text, Font textfont, Color textfore, Color textbackgroundcol,
                                                Image decal, Size decalsize,
                                                Image[] buttons, Size buttonsize,
                                                int padtop, int padbot,
                                                Action<object, int> ButtonPressed)
        {
            CompositeButton but = new CompositeButton();
            but.BackgroundImage = backimage;
            but.BackgroundImageLayout = ImageLayout.Stretch;

            ExtLabel l = new ExtLabel();
            l.Text = text;
            l.Font = textfont;
            l.ForeColor = textfore;
            l.TextBackColor = l.BackColor = textbackgroundcol;

            but.Controls.Add(l);

            Panel d = new Panel();
            d.BackgroundImage = decal;
            d.BackgroundImageLayout = ImageLayout.Stretch;
            d.Size = decalsize;

            but.Controls.Add(d);

            int butno = 0;
            foreach (Image i in buttons)
            {
                ExtButton b = new ExtButton();
                b.Image = i;
                b.ImageLayout = ImageLayout.Stretch;
                b.Size = buttonsize;
                b.Tag = butno++;
                b.Click += (o, e) => { ExtButton bhit = o as ExtButton; ButtonPressed?.Invoke(but, (int)bhit.Tag); };
                but.Controls.Add(b);
            }

            but.Padding = new Padding(0,padtop,0,padbot);
            return but;
        }

    }
}
