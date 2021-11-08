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

using BaseUtils;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class MessageBoxTheme : DraggableForm
    {
        // null in caption prints "Warning"

        static public DialogResult Show(IWin32Window window, string text, string caption = null , MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, Icon windowicon = null)
        {
            using (MessageBoxTheme msg = new MessageBoxTheme(text, caption, buttons, icon, windowicon))
            {
                return msg.ShowDialog(window);
            }   
        }

        static public MessageBoxTheme ShowModeless(IWin32Window window, string text, string caption = null, MessageBoxIcon icon = MessageBoxIcon.None, Icon windowicon = null)
        {
            MessageBoxTheme msg = new MessageBoxTheme(text, caption, null, icon, windowicon);
            msg.Show(window);
            return msg;
        }

        static public DialogResult Show(string text, string caption = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, Icon windowicon = null)
        {
            using (MessageBoxTheme msg = new MessageBoxTheme(text, caption, buttons, icon, windowicon))
            {
                return msg.ShowDialog(Application.OpenForms[0]);
            }   
        }

        public string MessageText { get { return themeTextBox.Text; } set { themeTextBox.Text = value; } }

        //      public string MsgText { get { return msgText; } set { SetText(value); } }     // modeless update

        MessageBoxButtons? buttons;     // The buttons that this dialog will display
        MessageBoxIcon mbIcon;          // The icon that this dialog will show

        public MessageBoxTheme(string text, string caption = null, MessageBoxButtons? buttons = MessageBoxButtons.OK, MessageBoxIcon messageBoxIcon = MessageBoxIcon.None, Icon formIcon = null)
        {
            InitializeComponent();

            DialogResult = DialogResult.None;

            this.Text = labelCaption.Text = caption ?? "Warning".Tx(this);
            themeTextBox.Text = text;
            this.buttons = buttons;
            this.mbIcon = messageBoxIcon;
            if (formIcon != null)
                this.Icon = formIcon;

            ITheme theme = ThemeableFormsInstance.Instance;

            if (theme != null)  // paranoid
            {
                theme.ApplyStd(this);
                themeTextBox.TextBoxBackColor = this.BackColor; // text box back is form back in this circumstance - we don't want it to stand out.
                if (theme.MessageBoxWindowIcon != null)
                    this.Icon = theme.MessageBoxWindowIcon;
            }
            else
            {
                this.Font = BaseUtils.FontLoader.GetFont("MS Sans Serif", 12.0F);
                this.ForeColor = Color.Black;
            }

            switch (buttons)
            {
                case null:
                    buttonExt1.Visible = buttonExt2.Visible = buttonExt3.Visible = false;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    buttonExt1.DialogResult = DialogResult.Ignore; buttonExt1.Text = "Ignore".Tx(this);
                    buttonExt2.DialogResult = DialogResult.Retry; buttonExt2.Text = "Retry".Tx(this);
                    buttonExt3.DialogResult = DialogResult.Abort; buttonExt3.Text = "Abort".Tx(this);
                    this.AcceptButton = buttonExt2;
                    this.CancelButton = buttonExt3;
                    break;
                case MessageBoxButtons.OKCancel:
                    buttonExt1.DialogResult = DialogResult.Cancel; buttonExt1.Text = "Cancel".Tx(this);
                    buttonExt2.DialogResult = DialogResult.OK; buttonExt2.Text = "OK".Tx(this);
                    buttonExt3.Visible = false;
                    this.AcceptButton = buttonExt2;
                    this.CancelButton = buttonExt1;
                    break;
                case MessageBoxButtons.RetryCancel:
                    buttonExt1.DialogResult = DialogResult.Cancel; buttonExt1.Text = "Cancel".Tx(this);
                    buttonExt2.DialogResult = DialogResult.OK; buttonExt2.Text = "Retry".Tx(this);
                    buttonExt3.Visible = false;
                    this.AcceptButton = buttonExt2;
                    this.CancelButton = buttonExt1;
                    break;
                case MessageBoxButtons.YesNo:
                    buttonExt1.DialogResult = DialogResult.No; buttonExt1.Text = "No".Tx(this);
                    buttonExt2.DialogResult = DialogResult.Yes; buttonExt2.Text = "Yes".Tx(this);
                    buttonExt3.Visible = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    buttonExt1.DialogResult = DialogResult.Cancel; buttonExt1.Text = "Cancel".Tx(this);
                    buttonExt2.DialogResult = DialogResult.No; buttonExt2.Text = "No".Tx(this);
                    buttonExt3.DialogResult = DialogResult.Yes; buttonExt3.Text = "Yes".Tx(this);
                    this.AcceptButton = this.CancelButton = buttonExt1;
                    break;
                case MessageBoxButtons.OK:
                default:
                    buttonExt1.DialogResult = DialogResult.OK; buttonExt1.Text = "OK".Tx(this);
                    buttonExt2.Visible = false;
                    buttonExt3.Visible = false;
                    this.AcceptButton = this.CancelButton = buttonExt1;
                    break;
            }

            Image iconselected = null;                // If not null, this icon will be drawn on the left of this form. Set from mbIcon in OnLoad

            switch (mbIcon)
            {
                // case MessageBoxIcon.Information:
                case MessageBoxIcon.Asterisk:
                    iconselected = SystemIcons.Asterisk.ToBitmap();
                    break;

                // case MessageBoxIcon.Exclamation:
                case MessageBoxIcon.Warning:
                    iconselected = SystemIcons.Warning.ToBitmap();
                    break;

                // case MessageBoxIcon.Error:
                // case MessageBoxIcon.Stop:
                case MessageBoxIcon.Hand:
                    iconselected = SystemIcons.Hand.ToBitmap();
                    break;

                case MessageBoxIcon.Question:
                    iconselected = SystemIcons.Question.ToBitmap();
                    break;

                case MessageBoxIcon.None:
                default:
                    break;
            }

            if (iconselected != null)
            {
                panelIcon.Width = iconselected.Width;
                panelIcon.Height = iconselected.Height;
                panelLeft.Width = panelIcon.Right + 8;
                panelIcon.BackgroundImage = iconselected;
            }
            else
            {
                panelLeft.Width = 4;
            }

            labelCaption.Visible = this.FormBorderStyle == FormBorderStyle.None;

            themeTextBox.BorderStyle = BorderStyle.None;
            themeTextBox.BorderColor = Color.Transparent;
            themeTextBox.ReadOnly = true;

            Size = new Size(20, 20);        // minimise until shown
        }

        private void MessageBoxTheme_Shown(object sender, EventArgs e)
        {
            bool framed = !(FormBorderStyle == FormBorderStyle.None);

            string longest = themeTextBox.Lines.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

            if (labelCaption.Text.Length > longest.Length)
                longest = labelCaption.Text;

            var measuretextcaption = BaseUtils.BitMapHelpers.MeasureStringInBitmap(longest, Font);

            int wantedw = 16 + (int)measuretextcaption.Width;

            int butspacing = buttonExt1.Left - buttonExt2.Left;     // make sure we have enough space for the buttons!
            if (buttonExt3.Visible)
                wantedw = Math.Max(wantedw, butspacing * 3 + 20);       // 20 allows for left edge
            else if (buttonExt2.Visible)
                wantedw = Math.Max(wantedw, butspacing * 2 + 20);
            else
                wantedw = Math.Max(wantedw, butspacing + 20);

            wantedw += panelLeft.Width;        // 0.5 is an estimate of avg ratio

            Width = wantedw;        // changing width changes estimate vert size

            int wantedh = themeTextBox.EstimateVerticalSizeFromText();
            if ( panelIcon.BackgroundImage != null )
                wantedh = Math.Max(panelIcon.Bottom, wantedh);

            wantedh += panelTopGap.Height + panelGap.Height + panelButs.Height  + (framed ? 50 : labelCaption.Height + 8) + Font.ScalePixels(8);

            this.Location = new Point(Owner.Left + Owner.Width / 2 - wantedw / 2, Owner.Top + Owner.Height / 2 - wantedh / 2);
            this.PositionSizeWithinScreen(wantedw, wantedh, false, new Size(64,64));

            buttonExt1.Focus();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (panelIcon.BackgroundImage != null)
                panelIcon.BackgroundImage.Dispose();
        }

        private void buttonExt_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void MoveMouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void MoveMouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
        }

    }
}
