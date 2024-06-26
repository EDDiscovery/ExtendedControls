﻿/*
 * Copyright © 2017-2021 EDDiscovery development team
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
using ExtendedControls;
using ExtendedForms;
using System;
using System.Windows.Forms;

namespace ExtendedAudioForms
{
    public partial class AudioDeviceConfigure : DraggableForm
    {
        public string Selected { get { return comboBoxCustomDevice.SelectedIndex >= 0 ? (string)comboBoxCustomDevice.SelectedItem : null; } }

        public AudioDeviceConfigure()
        {
            InitializeComponent();
        }

        public void Init( AudioExtensions.IAudioDriver dr )
        {
            comboBoxCustomDevice.Items.AddRange(dr.GetAudioEndpoints().ToArray());
            comboBoxCustomDevice.SelectedItem = dr.GetAudioEndpoint();

            var enumlist = new Enum[] { CFIDs.AudioDeviceConfigure, CFIDs.AudioDeviceConfigure_labelText };
            BaseUtils.Translator.Instance.TranslateControls(this, enumlist);

            bool border = ExtendedControls.Theme.Current?.ApplyDialog(this) ?? true;
            labelText.Visible = !border;
        }

        private void buttonExtOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
