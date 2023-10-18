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
        public int FixButtonsAcross { get; set; } = 1;                  // use maximum of decals/buttons or this. Must be 1 or more


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            //System.Diagnostics.Debug.WriteLine($"{Name} {Size} Font {Label.Font.Height} size {Label.Font.Size}");

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

            int entriesacross = Math.Max(Math.Max(Decals.Length, Buttons.Length), FixButtonsAcross);

            int decbutw = (ClientRectangle.Width - margin * (2 + entriesacross - 1)) / entriesacross;    // if =1 , two margin, =2 three, etc. Then divide by decals to get decal size

            int decbuth = (ClientRectangle.Height - Label.Bottom - margin * 3) / 2;         // height is whats left after label, three margins, and /2 to get size for decal and buttons

            int hpos = ClientRectangle.Width / 2 - (decbutw * Decals.Length + margin * (Decals.Length - 1)) / 2;    // position centrally
            int vpos = Label.Bottom + margin;

           // System.Diagnostics.Debug.WriteLine($"{Name} {Size} m {margin} decw {decw} butw {butw} => {decbutw} : h {decbuth} pos {hpos} {vpos} Font {Label.Font.Height} size {Label.Font.Size}");

            foreach (var p in Decals.EmptyIfNull())       // first measure properties - all panels across
            {
                p.Bounds = new Rectangle(hpos, vpos, decbutw, decbuth);
                hpos += decbutw + margin;
            }

            vpos += decbuth + margin;
            hpos = ClientRectangle.Width / 2 - (decbutw * Buttons.Length + margin + (Buttons.Length - 1)) / 2;      // position centrally

            foreach (var p in Buttons.EmptyIfNull())       // first measure properties - all panels across
            {
                p.Bounds = new Rectangle(hpos, vpos, decbutw, decbuth);
                hpos += decbutw + margin;
            }
        }

        // used to create a button dynamically

        public static CompositeAutoScaleButton QuickInit( Bitmap backimage,
                                                    string text, 
                                                Image[] decals,
                                                Image[] buttons,
                                                Action<object, int> ButtonPressed,
                                                int fixbuttonacross = 0)
        {
            CompositeAutoScaleButton cb = new CompositeAutoScaleButton();
            cb.Name = text;
            cb.FixButtonsAcross = fixbuttonacross;
            cb.SuspendLayout();
            
            cb.BackgroundImage = backimage;
            cb.BackgroundImageLayout = ImageLayout.Stretch;

            // store the colour used from the centre of the bit map as the backcolour of the composite
            // themer assigns this colour to buttons
            cb.BackColor = backimage.GetPixel(backimage.Width / 2, backimage.Height / 2);        

            cb.AutoSize = false;        // do not size to contents, this creates a circular relationship because we resize the components to the panel

            Label l = new Label();      // Font, colour set by themer
            l.Text = text;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.UseMnemonic = false;
            l.Margin = new Padding(0);
            l.AutoSize = false;     // we will position
            l.BackColor = Color.Transparent;
            l.AutoEllipsis = true;

            cb.Controls.Add(l);

            foreach (Image i in decals)
            {
                PanelNoTheme d = new PanelNoTheme();        // use the no theme, we don't want the backcolor changed. Panels are themed with a back/fore colour.
                d.Name = cb.Name + "_Decals";
                d.BackgroundImage = i;
                d.BackgroundImageLayout = ImageLayout.Stretch;
                d.BackColor = cb.BackColor;
                cb.Controls.Add(d);
            }

            int butno = 0;
            foreach (Image i in buttons)
            {
                ExtButton b = new ExtButton();
                b.Name = cb.Name + "_CB2_" + butno;
                b.Image = i;
                b.Tag = butno++;
                b.BackColor = cb.BackColor;
                b.Click += (o, e) => { ExtButton bhit = o as ExtButton; ButtonPressed?.Invoke(cb, (int)bhit.Tag); };
                cb.Controls.Add(b);
            }

            cb.ResumeLayout();
            return cb;
        }

    }
}
