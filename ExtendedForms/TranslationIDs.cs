/*
 * Copyright © 2021 EDDiscovery development team
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


namespace ExtendedForms
{
    internal enum CFIDs
    {
        SpeechConfigure, // Control 'Configure Speech'
        SpeechConfigure_Title, // Control 'Set Text to say (use ; to separate randomly selectable phrases and {} to group)'
        SpeechConfigure_buttonExtTest, // Control 'Test'
        SpeechConfigure_buttonExtDevice, // Control 'Device'
        SpeechConfigure_buttonExtEffects, // Control 'Effects'
        SpeechConfigure_checkBoxCustomLiteral, // Control 'Literal'
        SpeechConfigure_checkBoxCustomComplete, // Control 'Wait until speech completes'
        SpeechConfigure_labelEndTrigger, // Control 'End Trigger'
        SpeechConfigure_labelStartTrigger, // Control 'Start Trigger'
        SpeechConfigure_labelRate, // Control 'Rate'
        SpeechConfigure_labelVoice, // Control 'Voice'
        SpeechConfigure_labelVolume, // Control 'Volume'
        SpeechConfigure_checkBoxCustomR, // Control 'Override'
        SpeechConfigure_checkBoxCustomV, // Control 'Override'

        WaveConfigureDialog, // Control 'WaveConfigure'
        WaveConfigureDialog_buttonExtDevice, // Control 'Device'
        WaveConfigureDialog_labelStartTrigger, // Control 'Start Trigger'
        WaveConfigureDialog_labelEndTrigger, // Control 'End Trigger'
        WaveConfigureDialog_labelTitle, // Control 'Select Default device, volume and effects'
        WaveConfigureDialog_buttonExtBrowse, // Control 'Browse'
        WaveConfigureDialog_checkBoxCustomComplete, // Control 'Wait until audio completes'
        WaveConfigureDialog_buttonExtEffects, // Control 'Effects'
        WaveConfigureDialog_buttonExtTest, // Control 'Test'
        WaveConfigureDialog_checkBoxCustomV, // Control 'Override'
        WaveConfigureDialog_labelVolume, // Control 'Volume'

        SoundEffectsDialog, // Control 'SoundEffects'
        SoundEffectsDialog_labelEcho, // Control 'Echo'
        SoundEffectsDialog_buttonExtTest, // Control 'Test'
        SoundEffectsDialog_labelEchoMix, // Control 'Mix'
        SoundEffectsDialog_labelChorus, // Control 'Chorus'
        SoundEffectsDialog_checkBoxCustomNone, // Control 'No effects'
        SoundEffectsDialog_labelReverb, // Control 'Reverb'
        SoundEffectsDialog_checkBoxP, // Control 'Enable'
        SoundEffectsDialog_checkBoxG, // Control 'Enable'
        SoundEffectsDialog_labelDistortion, // Control 'Distortion'
        SoundEffectsDialog_checkBoxD, // Control 'Enable'
        SoundEffectsDialog_labelEchoFeedback, // Control 'Feedback'
        SoundEffectsDialog_labelPitch, // Control 'Pitch'
        SoundEffectsDialog_labelGargle, // Control 'Gargle'
        SoundEffectsDialog_labelChorusMix, // Control 'Mix'
        SoundEffectsDialog_labelEchoDelay, // Control 'Delay'
        SoundEffectsDialog_checkBoxR, // Control 'Enable'
        SoundEffectsDialog_labelReverbMix, // Control 'Mix'
        SoundEffectsDialog_labelDistortionGain, // Control 'Gain'
        SoundEffectsDialog_labelChorusFeedback, // Control 'Feedback'
        SoundEffectsDialog_checkBoxC, // Control 'Enable'
        SoundEffectsDialog_labelPitchOctave, // Control 'Octave'
        SoundEffectsDialog_labelGargleFrequency, // Control 'Frequency'
        SoundEffectsDialog_checkBoxE, // Control 'Enable'
        SoundEffectsDialog_labelReverbTime, // Control 'Time'
        SoundEffectsDialog_labelDistortionEdge, // Control 'Edge'
        SoundEffectsDialog_labelChorusDelay, // Control 'Delay'
        SoundEffectsDialog_labelChorusDepth, // Control 'Depth'
        SoundEffectsDialog_labelReverbHFRatio, // Control 'HF Ratio'
        SoundEffectsDialog_labelDistortionCentreFreq, // Control 'Cent Freq'
        SoundEffectsDialog_labelDistortionFreqWidth, // Control 'Freq Width'

        AudioDeviceConfigure, // Control 'Audio Device Select'
        AudioDeviceConfigure_labelText, // Control 'Select Device'
    }
}
