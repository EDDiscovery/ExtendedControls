/*
 * Copyright © 2016-2020 EDDiscovery development team
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

using System.Windows.Forms;

namespace ExtendedControls
{
    // Read only grid, based on the DGV you specify

    public class ExtPanelDataGridViewScrollWithDGV<DGV> : ExtPanelDataGridViewScroll where DGV:DataGridView, new()
    {
        public DGV DataGrid { get; set; }

        public ExtPanelDataGridViewScrollWithDGV() 
        {
            DataGrid = new DGV();
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGrid.ReadOnly = true;
            DataGrid.AllowUserToAddRows = DataGrid.AllowUserToDeleteRows = false;
            DataGrid.ScrollBars = ScrollBars.None;
            DataGrid.Dock = DockStyle.Fill;
            Controls.Add(DataGrid);
            Controls.Add(new ExtendedControls.ExtScrollBar());
        }
    }
}
