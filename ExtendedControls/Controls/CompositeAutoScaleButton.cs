/*
 * Copyright © 2022-2022 EDDiscovery development team
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
    // a composite button which autoscales its content to its size

    public class CompositeAutoScaleButton : Panel            
    {
        public CompositeAutoScaleButton()
        {
        }

        public Label Label { get { return Controls.OfType<Label>().FirstOrDefault(); } }
        public Panel[] Decals { get { return Controls.OfType<Panel>().ToArray(); } }
        public ExtButton[] Buttons { get { return Controls.OfType<ExtButton>().ToArray(); } }

        public int AutoScaleFontSizeToWidth { get; set; } = 0;          // set to scaling.. 15 seems good


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            System.Diagnostics.Debug.WriteLine($"{Name} {Size} Font {Label.Font.Height} size {Label.Font.Size}");

            // nice border around dependent on size

            int margin = ClientSize.Width / 20;

            // here we set the font of the label to the size, either scaled by AutoScaleFontSizeToWidth, or just use inheritied font
            if (AutoScaleFontSizeToWidth > 0)
                Label.Font = new Font(this.Font.Name, ClientRectangle.Width / AutoScaleFontSizeToWidth);
            else
                Label.Font = null;

            // set label pos
            Label.Bounds = new Rectangle(margin, margin, ClientRectangle.Width - margin * 2, Label.Font.Height + 2);

            // estimate button sizes. 
            // M Icon M Icon M  across
            // M Label M Decals M Buttons M down

            int decw = (ClientRectangle.Width - margin * (2 + Decals.Length - 1)) / Decals.Length;          // if decals =1 , two margin, =2 three, etc. Then divide by decals to get decal size
            int butw = (ClientRectangle.Width - margin * (2 + Buttons.Length - 1)) / Buttons.Length;        // repeat with buttons
            int decbutw = Math.Min(decw, butw);                                                             // make them all the same size

            int decbuth = (ClientRectangle.Height - Label.Bottom - margin * 3) / 2;                         // height is whats left after label, three margins, and /2 to get size for decal and buttons

            int hpos = ClientRectangle.Width / 2 - (decbutw * Decals.Length + margin * (Decals.Length - 1)) / 2;    // position centrally
            int vpos = Label.Bottom + margin;

           // System.Diagnostics.Debug.WriteLine($"{Name} {Size} m {margin} decw {decw} butw {butw} => {decbutw} : h {decbuth} pos {hpos} {vpos} Font {Label.Font.Height} size {Label.Font.Size}");

            foreach (var p in Decals)       // first measure properties - all panels across
            {
                p.Size = new Size(decbutw, decbuth);
                p.Location = new Point(hpos, vpos);
                hpos += decbutw + margin;
            }

            vpos += decbuth + margin;
            hpos = ClientRectangle.Width / 2 - (decbutw * Buttons.Length + margin + (Buttons.Length - 1)) / 2;      // position centrally

            foreach (var p in Buttons)       // first measure properties - all panels across
            {
                p.Bounds = new Rectangle(hpos, vpos, decbutw, decbuth);
                hpos += decbutw + margin;
                System.Diagnostics.Debug.WriteLine($"    {p.Name} {p.Bounds}");
            }

        }

        // used to create a button dynamically

        public static CompositeAutoScaleButton QuickInit(   Image backimage,
                                                    string text, Color textfore, Color textbackgroundcol,
                                                Image[] decals,
                                                Image[] buttons,
                                                Action<object, int> ButtonPressed)
        {
            CompositeAutoScaleButton cb = new CompositeAutoScaleButton();
            cb.Name = text;
            cb.SuspendLayout();
            cb.BackgroundImage = backimage;
            cb.BackgroundImageLayout = ImageLayout.Stretch;
            cb.AutoSize = false;        // do not size to contents, this creates a circular relationship because we resize the components to the panel

            Label l = new Label();      // Font inherited from this class
            l.Text = text;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.UseMnemonic = false;
            l.Margin = new Padding(0);
            l.ForeColor = textfore;
            l.BackColor = textbackgroundcol;
            l.AutoSize = false;     // we will position
            l.AutoEllipsis = true;

            cb.Controls.Add(l);

            foreach (Image i in decals)
            {
                Panel d = new Panel();
                d.BackgroundImage = i;
                d.BackgroundImageLayout = ImageLayout.Stretch;
                d.BackColor = Color.Transparent;
                cb.Controls.Add(d);
            }

            int butno = 0;
            foreach (Image i in buttons)
            {
                ExtButton b = new ExtButton();
                b.Name = "CB2 " + butno;
                b.Image = i;
                b.ImageLayout = ImageLayout.Stretch;
                b.Tag = butno++;
                b.Click += (o, e) => { ExtButton bhit = o as ExtButton; ButtonPressed?.Invoke(cb, (int)bhit.Tag); };
                cb.Controls.Add(b);
            }

            cb.Padding = new Padding(0, 0, 0, 0);
            cb.ResumeLayout();
            return cb;
        }

    }
}
