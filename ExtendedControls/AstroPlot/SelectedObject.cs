/*
 * Copyright © 2016 - 2020 EDDiscovery development team
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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        public class SelectedObject : INotifyPropertyChanged
        {
            public string Name;
            public PointF Coords;

            public event PropertyChangedEventHandler PropertyChanged;

            public SelectedObject()
            { }

            public SelectedObject(string name, PointF coords)
            {
                Name = name;
                Coords = coords;
            }

            public string SelectedObjectName
            {
                get => Name;
                set
                {
                    Name = value;
                    OnNamePropertyChanged();
                }
            }

            public PointF SelectedObjectCoords
            {
                get => Coords;
                set => Coords = value;
            }

            private void OnNamePropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }     
    }
}
