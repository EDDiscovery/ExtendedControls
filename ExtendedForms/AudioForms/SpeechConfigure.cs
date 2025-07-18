﻿/*
 * Copyright © 2017 EDDiscovery development team
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
using System.Windows.Forms;
using AudioExtensions;
using BaseUtils;
using ExtendedControls;
using ExtendedForms;

namespace ExtendedAudioForms
{
    public partial class SpeechConfigure : DraggableForm
    {
        public string SayText { get { return textBoxBorderText.Text; } }
        public bool Wait { get { return checkBoxCustomComplete.Checked; } }
        public bool Literal { get { return checkBoxCustomLiteral.Checked; } }
        public AudioQueue.Priority Priority { get { return (AudioQueue.Priority)Enum.Parse(typeof(AudioQueue.Priority), comboBoxCustomPriority.Text); } }
        public string StartEvent { get { return textBoxBorderStartTrigger.Text; } }
        public string FinishEvent { get { return textBoxBorderEndTrigger.Text; } }
        public string VoiceName { get { return comboBoxCustomVoice.Text; } }
        public string Volume { get { return (checkBoxCustomV.Checked) ? trackBarVolume.Value.ToString() : "Default"; } }
        public string Rate { get { return (checkBoxCustomR.Checked) ? trackBarRate.Value.ToString() : "Default"; } }
        public Variables Effects { get { return effects;  } }

        AudioQueue queue;
        SpeechSynthesizer synth;
        Variables effects;

        public SpeechConfigure()
        {
            InitializeComponent();
        }

        public void Init(bool nospeechinputbox , bool nodevice, bool novoicename,  bool norate,
                            AudioQueue qu, SpeechSynthesizer syn,
                            string caption, Icon ic,
                            string text,                            // when speech input box
                            bool waituntilspeechcompletes, bool literaltext,  // when speech input box
                            AudioQueue.Priority prio,               // when speech input box
                            string startname, string endname,       // when speech input box
                            string voicename,                       // when voice name 
                            string volume,
                            string rate,
                            Variables ef)     // effects can also contain other vars, it will ignore
        {
            comboBoxCustomPriority.Items.AddRange(Enum.GetNames(typeof(AudioQueue.Priority)));

            queue = qu;
            synth = syn;

            var enumlist = new Enum[] { CFIDs.SpeechConfigure, CFIDs.SpeechConfigure_buttonExtTest, CFIDs.SpeechConfigure_buttonExtDevice, CFIDs.SpeechConfigure_buttonExtEffects, CFIDs.SpeechConfigure_checkBoxCustomLiteral, CFIDs.SpeechConfigure_checkBoxCustomComplete, CFIDs.SpeechConfigure_labelEndTrigger, CFIDs.SpeechConfigure_labelStartTrigger, CFIDs.SpeechConfigure_labelRate, CFIDs.SpeechConfigure_labelVoice, CFIDs.SpeechConfigure_labelVolume, CFIDs.SpeechConfigure_checkBoxCustomR, CFIDs.SpeechConfigure_checkBoxCustomV, CFIDs.SpeechConfigure_Title };
            BaseUtils.Translator.Instance.TranslateControls(this, enumlist);

            if (caption != null)
                this.Text = caption;

            this.Icon = ic;
            textBoxBorderTest.Text = "The quick brown fox jumped over the lazy dog";

            if (nospeechinputbox)
            {
                textBoxBorderText.Visible = checkBoxCustomComplete.Visible = comboBoxCustomPriority.Visible = labelStartTrigger.Visible = labelEndTrigger.Visible =
                checkBoxCustomLiteral.Visible = textBoxBorderStartTrigger.Visible = checkBoxCustomV.Visible = checkBoxCustomR.Visible = textBoxBorderEndTrigger.Visible = false;

                int offset = comboBoxCustomVoice.Top - textBoxBorderText.Top;
                foreach (Control c in panelOuter.Controls)
                {
                    if (!c.Name.Equals("Title"))
                        c.Location = new Point(c.Left, c.Top - offset);
                }

                this.Height -= offset;
            }
            else
            {
                textBoxBorderText.Text = text;
                checkBoxCustomComplete.Checked = waituntilspeechcompletes;
                checkBoxCustomLiteral.Checked = literaltext;
                comboBoxCustomPriority.SelectedItem = prio.ToString();
                textBoxBorderStartTrigger.Text = startname;
                textBoxBorderEndTrigger.Text = endname;
            }

            if ( novoicename )
            {
                labelVoice.Visible =  comboBoxCustomVoice.Visible = false;

                int offset = trackBarVolume.Top - comboBoxCustomVoice.Top;
                foreach (Control c in panelOuter.Controls)
                {
                    if (c.Top >= trackBarVolume.Top)
                        c.Location = new Point(c.Left, c.Top - offset);
                }

                this.Height -= offset;
            }

            if ( norate )
            {
                labelRate.Visible = trackBarRate.Visible = false;
                int offset = textBoxBorderTest.Top - trackBarRate.Top;
                foreach (Control c in panelOuter.Controls)
                {
                    if (c.Top >= trackBarRate.Top)
                        c.Location = new Point(c.Left, c.Top - offset);
                }

                this.Height -= offset;
            }

            buttonExtDevice.Visible = !nodevice;

            comboBoxCustomVoice.Items.Add("Default");
            comboBoxCustomVoice.Items.Add("Female");
            comboBoxCustomVoice.Items.Add("Male");
            comboBoxCustomVoice.Items.AddRange(synth.GetVoiceNames());
            if (comboBoxCustomVoice.Items.Contains(voicename))
                comboBoxCustomVoice.SelectedItem = voicename;
            else
                comboBoxCustomVoice.SelectedIndex = 0;

            int i;
            if (!nospeechinputbox && volume.Equals("Default", StringComparison.InvariantCultureIgnoreCase))  
            {
                checkBoxCustomV.Checked = false;
                trackBarVolume.Enabled = false;
            }
            else
            {
                checkBoxCustomV.Checked = true;
                if (volume.InvariantParse(out i))
                    trackBarVolume.Value = i;
            }

            if (!nospeechinputbox && rate.Equals("Default", StringComparison.InvariantCultureIgnoreCase))
            {
                checkBoxCustomR.Checked = false;
                trackBarRate.Enabled = false;
            }
            else
            {
                checkBoxCustomR.Checked = true;
                if (rate.InvariantParse(out i))
                    trackBarRate.Value = i;
            }

            effects = ef;

            ExtendedControls.Theme.Current?.ApplyDialog(this);
        }

        private void buttonExtOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonExtEffects_Click(object sender, EventArgs e)
        {
            SoundEffectsDialog sfe = new SoundEffectsDialog();
            sfe.Init(this.Icon, effects, textBoxBorderText.Visible);           // give them the none option ONLY if we are allowing text
            sfe.TestSettingEvent += Sfe_TestSettingEvent;           // callback to say test
            sfe.StopTestSettingEvent += Sfe_StopTestSettingEvent;   // callback to say stop
            if ( sfe.ShowDialog(this) == DialogResult.OK )
            {
                effects = sfe.GetEffects();
            }
        }

        private void Sfe_TestSettingEvent(SoundEffectsDialog sfe, Variables effects)
        {
            var ms = synth.Speak(textBoxBorderTest.Text, "Default", comboBoxCustomVoice.Text, trackBarRate.Value);
            if (ms != null)
            {
                AudioQueue.AudioSample a = queue.Generate(ms, new SoundEffectSettings(effects));
                a.sampleOverEvent += SampleOver;
                a.sampleOverTag = sfe;
                queue.Submit(a, trackBarVolume.Value, AudioQueue.Priority.High);
            }
        }

        private void Sfe_StopTestSettingEvent(SoundEffectsDialog sender)
        {
            queue.StopCurrent();
        }

        private void SampleOver(AudioQueue s, Object tag)
        {
            SoundEffectsDialog sfe = tag as SoundEffectsDialog;
            sfe.TestOver();
        }

        private void checkBoxCustomV_CheckedChanged(object sender, EventArgs e)
        {
            trackBarVolume.Enabled = checkBoxCustomV.Checked;

        }
        private void checkBoxCustomR_CheckedChanged(object sender, EventArgs e)
        {
            trackBarRate.Enabled = checkBoxCustomR.Checked;
        }

        private void buttonExtDevice_Click(object sender, EventArgs e)
        {
            AudioDeviceConfigure adc = new AudioDeviceConfigure();
            adc.Init(queue.Driver);
            if ( adc.ShowDialog(this) == DialogResult.OK )
            {
                if (!queue.SetAudioEndpoint(adc.Selected))
                    ExtendedControls.MessageBoxTheme.Show(this, "Audio Device Selection failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonExtTest_Click(object sender, EventArgs e)
        {
            if (buttonExtTest.Text.Equals("Stop"))
            {
                queue.StopCurrent();
            }
            else
            {
                try
                {
                    var ms = synth.Speak(textBoxBorderTest.Text, "Default", comboBoxCustomVoice.Text, trackBarRate.Value);
                    if (ms != null)
                    {
                        AudioExtensions.AudioQueue.AudioSample audio = queue.Generate(ms, new SoundEffectSettings(effects));
                        audio.sampleOverEvent += Audio_sampleOverEvent;
                        queue.Submit(audio, trackBarVolume.Value, AudioQueue.Priority.High);
                        buttonExtTest.Text = "Stop";
                    }
                }
                catch
                {
                    ExtendedControls.MessageBoxTheme.Show(this,"Unable to play " + textBoxBorderText.Text);
                }
            }
        }

        private void Audio_sampleOverEvent(AudioQueue sender, object tag)
        {
            buttonExtTest.Text = "Test";
        }

        private void CapMouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void CapMouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
        }

    }
}
