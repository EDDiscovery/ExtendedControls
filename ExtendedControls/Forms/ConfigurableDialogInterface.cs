/*
 * Copyright © 2024-2024 EDDiscovery development team
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
 */

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public interface IConfigurableDialog
    {
        void Show(IWin32Window window);
        void ReturnResult(DialogResult result);
        DialogResult DialogResult { get; set; }
        Point Location { get; set; }
        Size Size { get; set; }
        string Add(string instr);
        void UpdateEntries();   // call after add when shown
        bool Remove(string controlname);
        Control GetControl(string controlname);
        string Get(string controlname);
        T GetValue<T>(string controlname);
        bool Set(string controlname, string value);
        void SetCheckedList(IEnumerable<string> controlnames, bool state);
        void RadioButton(string startingcontrolname, string controlhit, int max = 1);
        string AddSetRows(string controlname, string rowstring);
        bool Clear(string controlname);
        int RemoveRows(string controlname, int start, int count);
        bool IsAllValid();
        void CloseDropDown();

        bool SetEnable(string controlname, bool enabled);
        bool SetVisible(string controlname, bool enabled);
        bool IsEnabled(string controlname);
        bool IsVisible(string controlname);
        bool GetPosition(string controlname, out Rectangle r);  // in dialog units
        bool SetPosition(string controlname, Rectangle r); // in dialog units
    }

}
