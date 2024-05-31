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

namespace ExtendedControls
{
    internal enum ECIDs
    {
        InfoForm_buttonAcknowledge, // Control 'Acknowledge'

        KeyForm, // Control 'Define Key Sequence'
        KeyForm_radioButtonUp, // Control 'Up'
        KeyForm_radioButtonDown, // Control 'Down'
        KeyForm_radioButtonPress, // Control 'Press'
        KeyForm_checkBoxShift, // Control 'Shift'
        KeyForm_checkBoxCtrl, // Control 'Ctrl'
        KeyForm_checkBoxAlt, // Control 'Alt'
        KeyForm_checkBoxKey, // Control ''Press Key''
        KeyForm_labelNextDelay, // Control 'Current Delay'
        KeyForm_labelSelKeys, // Control 'Select Key'
        KeyForm_labelDelay, // Control 'Start Delay(ms)'
        KeyForm_labelSendTo, // Control 'Send To'
        KeyForm_buttonReset, // Control 'Reset'
        KeyForm_labelKeys, // Control 'Keys'
        KeyForm_buttonDelete, // Control 'Delete'
        KeyForm_buttonNext, // Control 'Next'
        KeyForm_buttonTest, // Control 'Test'
        KeyForm_PK, // Press Key
        KeyForm_NOPN, //Name a process to test sending keys
        KeyForm_KERR, // Error {0} - check entry
        KeyForm_NOP, // No process names to send keys to

        InfoForm_Copyingtextfailed,

        None,
        All,
        OK,
        Cancel,

        Disabled,

        MessageBoxTheme_OK,
        MessageBoxTheme_Cancel,
        MessageBoxTheme_Warning,
        MessageBoxTheme_No,
        MessageBoxTheme_Yes,
        MessageBoxTheme_Abort,
        MessageBoxTheme_Retry,
        MessageBoxTheme_Ignore,

        ExtPanelRollUp_RUPPin,
        ExtPanelRollUp_RUPMarker,

        ImportExportForm, // Control 'Export'
        ImportExportForm_buttonExport, // Control 'Export'
        ImportExportForm_ImportTitle,
        ImportExportForm_ImportButton,
        ImportExportForm_labelCVSSep, // Control 'CSV Separator'
        ImportExportForm_radioButtonComma, // Control 'Comma'
        ImportExportForm_radioButtonSemiColon, // Control 'Semicolon'
        ImportExportForm_checkBoxIncludeHeader, // Control 'Include Header'
        ImportExportForm_checkBoxCustomAutoOpen, // Control 'Open'
        ImportExportForm_labelUTCEnd, // Control 'UTC'
        ImportExportForm_labelUTCStart, // Control 'UTC'
        ImportExportForm_labelPaste,
        ImportExportForm_extRadioButtonTab,
        ImportExportForm_extCheckBoxExcludeHeader,
        ImportExportForm_labelSaveImport
    }
}
