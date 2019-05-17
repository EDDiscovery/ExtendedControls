/*
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
using BaseUtils.Win32Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using BaseUtils;

using System.Windows.Forms;

namespace ExtendedConditionsForms
{
    public partial class ConditionFilterUC : UserControl
    {
        #region Public Interface

        public ConditionLists Result;

        public Func<string, List<TypeHelpers.PropertyNameInfo>> VariableNamesEvents;         // set to hook up additional names, keyed by event
        public List<TypeHelpers.PropertyNameInfo> VariableNames { get; set; } = null;        // set to add variable names

        public int Groups { get { return groups.Count; } }
        public Action<int> onChangeInGroups;                        // called if any change in group numbers

        public Action<int> onCalcMinsize;                           // called with the y size of the windows, allows the caller if implemented to resize to suit

        public ConditionFilterUC()
        {
            groups = new List<Group>();
            InitializeComponent();
        }

        // used to start when just filtering.. uses a fixed event list .. must provide a call back to obtain names associated with an event

        public void InitFilter(List<string> events, ConditionLists j = null)
        {
            this.eventlist = events;
            events.Add("All");
            Init(events, true);
            LoadConditions(j);
        }

        // used to start when inside a condition of an IF of a program action (does not need additional names, already resolved)

        public void InitConditionList(ConditionLists j = null)
        {
            Init(null, true);
            LoadConditions(j);
        }

        // used to start for a condition on an action form (does not need additional names, already resolved)

        public void InitCondition(Condition j)
        {
            ConditionLists l = new ConditionLists();
            if ( j!= null && j.fields != null )
                l.Add(j);
            Init(null, false);          // no outer conditions..
            LoadConditions(l);
        }

        public void LoadConditions( ConditionLists clist )
        {
            if (clist != null)
            {
                SuspendLayout();
                this.panelConditionUC.SuspendLayout();
                panelVScroll.SuspendLayout();

                panelVScroll.RemoveAllControls( new List<Control> { buttonMore } );            // except these!

                groups = new List<Group>();

                Condition fe = null;
                for(int i = 0; (fe = clist.Get(i))!=null; i++)
                {
                    Group g = CreateGroupInternal(fe.eventname, fe.action, fe.actiondata, fe.innercondition.ToString(), fe.outercondition.ToString());

                    foreach (ConditionEntry f in fe.fields)
                        CreateConditionInt(g, f.itemname, ConditionEntry.MatchNames[(int)f.matchtype], f.matchstring);

                    ExtendedControls.ThemeableFormsInstance.Instance?.ApplyDialogSubControls(g.panel);

                    groups.Add(g);
                }

                System.Runtime.GCLatencyMode lm = System.Runtime.GCSettings.LatencyMode;
                System.Runtime.GCSettings.LatencyMode = System.Runtime.GCLatencyMode.LowLatency;

                foreach (Group g in groups)                         // FURTHER investigation.. when VScroll has been used after the swap, this and fix groups takes ages
                    panelVScroll.Controls.Add(g.panel);             // why? tried a brand new vscroll.  Tried a lot of things.  Is it GC?

                FixUpGroups();                                      // this takes ages too on second load..

                panelVScroll.ResumeLayout();
                this.panelConditionUC.ResumeLayout();
                ResumeLayout();

                System.Runtime.GCSettings.LatencyMode = lm;
            }

            onChangeInGroups?.Invoke(groups.Count);
        }

        public void Clear()
        {
            List<Group.Conditions> cd = new List<Group.Conditions>();

            foreach (Group g in groups)                         // FURTHER investigation.. when VScroll has been used after the swap, this and fix groups takes ages
                foreach (var c in g.condlist)
                    cd.Add(c);

            foreach( var c in cd )
            {
                Delete(c);
            }
        }

        #endregion

        #region Implementation

        private void Init(List<string> el, bool outerconditions)                        // outc selects if group outer action can be selected, else its OR
        {
            eventlist = el;
            allowoutercond = outerconditions;
        }

        private void panelVScroll_Resize(object sender, EventArgs e)
        {
            if ( Width>0 && Height>0)
                FixUpGroups(false);
        }

        #endregion

        #region Groups

        private void buttonMore_Click(object sender, EventArgs e)       // main + button
        {
            SuspendLayout();
            panelVScroll.SuspendLayout();

            Group g;

            g = CreateGroupInternal(null, null, null, null, null);

            if (eventlist == null)      // if we don't have any event list, auto create a condition
                CreateConditionInt(g, null, null, null);

            ExtendedControls.ThemeableFormsInstance.Instance?.ApplyDialogSubControls(g.panel);

            groups.Add(g);
            panelVScroll.Controls.Add(g.panel);

            FixUpGroups();

            panelVScroll.ResumeLayout();
            ResumeLayout();

            panelVScroll.ToEnd();       // tell it to scroll to end

            onChangeInGroups?.Invoke(groups.Count);
        }

        private Group CreateGroupInternal(string initialev, string initialaction, string initialactiondatastring,
                                string initialcondinner, string initialcondouter)
        {
            Group g = new Group();

            g.panel = new Panel();
            g.panel.SuspendLayout();

            int p24 = Font.ScalePixels(24);

            if (eventlist != null)
            {
                g.evlist = new ExtendedControls.ExtComboBox();
                g.evlist.Items.AddRange(eventlist);
                g.evlist.Location = new Point(panelxmargin, panelymargin);
                g.evlist.Size = new Size(Font.ScalePixels(150), p24);
                if (initialev != null && initialev.Length > 0)
                    g.evlist.Text = initialev;
                g.evlist.SelectedIndexChanged += Evlist_SelectedIndexChanged;
                g.evlist.Tag = g;
                g.panel.Controls.Add(g.evlist);
            }

            g.innercond = new ExtendedControls.ExtComboBox();
            g.innercond.Items.AddRange(Enum.GetNames(typeof(ConditionEntry.LogicalCondition)));
            g.innercond.SelectedIndex = 0;
            g.innercond.Size = new Size(Font.ScalePixels(60), p24);
            g.innercond.Visible = false;
            if ( initialcondinner != null)
                g.innercond.Text = initialcondinner;
            g.panel.Controls.Add(g.innercond);

            g.outercond = new ExtendedControls.ExtComboBox();
            g.outercond.Items.AddRange(Enum.GetNames(typeof(ConditionEntry.LogicalCondition)));
            g.outercond.SelectedIndex = 0;
            g.outercond.Size = new Size(Font.ScalePixels(60), p24);
            g.outercond.Enabled = g.outercond.Visible = false;
            if (initialcondouter != null)
                g.outercond.Text = initialcondouter;
            g.panel.Controls.Add(g.outercond);

            g.outerlabel = new Label();
            g.outerlabel.Text = " with group(s) above";
            g.outerlabel.AutoSize = true;
            g.outerlabel.Visible = false;
            g.panel.Controls.Add(g.outerlabel);

            g.upbutton = new ExtendedControls.ExtButton();
            g.upbutton.Size = new Size(p24,p24);
            g.upbutton.Text = "^";
            g.upbutton.Click += Up_Click;
            g.upbutton.Tag = g;
            g.panel.Controls.Add(g.upbutton);

            g.panel.ResumeLayout();

            return g;
        }


        private void Evlist_SelectedIndexChanged(object sender, EventArgs e)                // EVENT list changed
        {
            ExtendedControls.ExtComboBox b = sender as ExtendedControls.ExtComboBox;
            Group g = (Group)b.Tag;

            if ( g.condlist.Count == 0 )        // if no conditions, create one..
            {
                if ( g.evlist.Text.Equals("onKeyPress"))                    // special fill in for some events..
                    CreateCondition(g,"KeyPress", "== (Str)","");
                else
                    CreateCondition(g);
            }
            else
            {
                g.variables = CreateVariables(g.evlist?.Text);          // rework out variables

                foreach (var c in g.condlist)           // clear any tool tips
                {
                    c.fname.EndButtonEnable = (g.variables?.Count ?? 0) > 0;
                    c.fname.SetTipDynamically(toolTip, "");
                }
            }

            FixUpGroups();                      // and reposition and maybe turn on/off the group outer cond
        }

        private void Up_Click(object sender, EventArgs e)
        {
            ExtendedControls.ExtButton b = sender as ExtendedControls.ExtButton;
            Group g = (Group)b.Tag;
            int indexof = groups.IndexOf(g);
            groups.Remove(g);
            groups.Insert(indexof - 1, g);
            FixUpGroups();
        }

        #endregion

        #region Condition

        private void CreateCondition(Group g, string initialfname = null, string initialcond = null, string initialvalue = null )
        {
            CreateConditionInt(g, initialfname, initialcond, initialvalue);
            ExtendedControls.ThemeableFormsInstance.Instance?.ApplyDialogSubControls(g.panel);
            FixUpGroups();
        }

        private void CreateConditionInt( Group g , string initialfname, string initialcond, string initialvalue)
        {
            g.panel.SuspendLayout();

            Group.Conditions c = new Group.Conditions();

            int p24 = Font.ScalePixels(24);

            c.fname = new ExtendedControls.ExtTextBoxAutoComplete();
            c.fname.EndButtonVisible = true;
            c.fname.Name = "CVar";
            c.fname.Size = new Size(Font.ScalePixels(200), p24);
            c.fname.SetAutoCompletor(AutoCompletor);
            c.fname.Tag = new Tuple<Group,Group.Conditions>(g,c);
            c.fname.AutoCompleteCommentMarker = commentmarker;
            if (initialfname != null)
                c.fname.Text = initialfname;
            g.panel.Controls.Add(c.fname);                                                // 1st control

            c.cond = new ExtendedControls.ExtComboBox();
            c.cond.Items.AddRange(ConditionEntry.MatchNames);
            c.cond.SelectedIndex = 0;
            c.cond.Size = new Size(Font.ScalePixels(140), p24);
            c.cond.Tag = g;

            if (initialcond != null)
                c.cond.Text = initialcond.SplitCapsWord();

            c.cond.SelectedIndexChanged += Cond_SelectedIndexChanged; // and turn on handler

            g.panel.Controls.Add(c.cond);         // must be next

            c.value = new ExtendedControls.ExtTextBox();
            c.value.Size = new Size(100, p24);  // width will be set in positioning

            if (initialvalue != null)
                c.value.Text = initialvalue;

            g.panel.Controls.Add(c.value);         // must be next

            c.del = new ExtendedControls.ExtButton();
            c.del.Size = new Size(p24, p24);
            c.del.Text = "X";
            c.del.Click += ConditionDelClick;
            c.del.Tag = c;
            g.panel.Controls.Add(c.del);

            c.more = new ExtendedControls.ExtButton();
            c.more.Size = new Size(p24, p24);
            c.more.Text = "+";
            c.more.Click += NewConditionClick;
            c.more.Tag = g;
            g.panel.Controls.Add(c.more);

            c.group = g;
            g.condlist.Add(c);

            g.variables = CreateVariables(g.evlist?.Text);
            c.fname.EndButtonEnable = (g.variables?.Count ?? 0) > 0;

            c.fname.TextChanged += TextChangedInLeft;       // only when fully set up do we turn on the text change handler - exception if you do it sooner

            g.panel.ResumeLayout();
        }

        private void TextChangedInLeft(object sender, EventArgs e)          // something changed in text field..
        {
            ExtendedControls.ExtTextBoxAutoComplete t = sender as ExtendedControls.ExtTextBoxAutoComplete;
            Tuple<Group, Group.Conditions> gc = t.Tag as Tuple<Group, Group.Conditions>;

            if ( gc.Item1.variables != null )      // if variables associated
            {
                string text = t.Text;
                foreach( var v in gc.Item1.variables)
                {
                    if ( text.Equals(v.Name))
                    {
                        t.SetTipDynamically(toolTip, v.Help ?? "");

                        if (v.DefaultCondition != null )
                        {
                            ExtendedControls.ExtComboBox cb = gc.Item2.cond;

                            if ( ConditionEntry.MatchTypeFromString(cb.Text, out ConditionEntry.MatchType mt) )
                            {
                                ConditionEntry.Classification mtc = ConditionEntry.Classify(mt);
                                ConditionEntry.Classification defcls = ConditionEntry.Classify(v.DefaultCondition.Value);

                                if ( mtc != defcls)
                                {
                                    System.Diagnostics.Debug.WriteLine("Select cond" + v.DefaultCondition.Value + " cur " + mt);
                                    cb.SelectedItem = ConditionEntry.MatchNames[(int)v.DefaultCondition.Value];
                                }
                            }
                        }

                        return;
                    }
                }
            }

            t.SetTipDynamically(toolTip, "");
        }

        private void Cond_SelectedIndexChanged(object sender, EventArgs e)          // on condition changing, see if value is needed 
        {
            FixUpGroups();
        }

        private void ConditionDelClick(object sender, EventArgs e)
        {
            ExtendedControls.ExtButton b = sender as ExtendedControls.ExtButton;
            Group.Conditions c = (Group.Conditions)b.Tag;
            Delete(c);
        }

        private void Delete(Group.Conditions c)
        { 
            Group g = c.group;

            g.panel.Controls.Remove(c.fname);
            g.panel.Controls.Remove(c.cond);
            g.panel.Controls.Remove(c.value);
            g.panel.Controls.Remove(c.more);
            g.panel.Controls.Remove(c.del);

            g.condlist.Remove(c);

            if ( g.condlist.Count == 0 )
            {
                panelVScroll.Controls.Remove(g.panel);
                g.panel.Controls.Clear();
                groups.Remove(g);
                onChangeInGroups?.Invoke(groups.Count);
            }

            FixUpGroups();
        }

        private void NewConditionClick(object sender, EventArgs e)
        {
            ExtendedControls.ExtButton b = sender as ExtendedControls.ExtButton;
            Group g = (Group)b.Tag;
            CreateCondition(g);
        }

        #endregion


        #region Positioning

        private void FixUpGroups(bool calcminsize = true)      // fixes and positions groups.
        {
            SuspendLayout();
            panelVScroll.SuspendLayout();

            int panelwidth = Math.Max(panelVScroll.Width - panelVScroll.ScrollBarWidth, 10);
            int y = panelymargin;

            for (int i = 0; i < groups.Count; i++)
            {
                Group g = groups[i];
                g.panel.SuspendLayout();

                // for all groups, see if another group below it has the same event selected as ours

                bool showouter = false;                     

                if (eventlist != null)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (groups[j].evlist.Text.Equals(groups[i].evlist.Text) && groups[i].evlist.Text.Length > 0)
                            showouter = true;
                    }

                    showouter = showouter && allowoutercond;     // qualify with outer condition switch
                }
                else
                    showouter = (i > 0) && allowoutercond;       // and enabled/disable the outer condition switch

                // Now position the conditions inside the panel

                int vnextcond = panelymargin;

                int condxoffset = (g.evlist != null) ? (g.evlist.Right + 8) : panelxmargin;
                int farx = condxoffset; // should never not have a condition but ..

                int panelxspacing = Font.ScalePixels(4);
                int panelyspacing = Font.ScalePixels(6);

                for (int condc = 0; condc < g.condlist.Count; condc++)
                {
                    Group.Conditions c = g.condlist[condc];
                    c.fname.Location = new Point(condxoffset, vnextcond);
                    c.fname.Enabled = !ConditionEntry.IsNullOperation(c.cond.Text);
                    if (!c.fname.Enabled)
                        c.fname.Text = "";

                    c.cond.Location = new Point(c.fname.Right + panelxspacing, vnextcond);

                    c.value.Location = new Point(c.cond.Right + panelxspacing, vnextcond );
                    c.value.Width = panelwidth - condxoffset - c.fname.Width - 4 - c.cond.Width - 4 - c.del.Width - 4 - c.more.Width - 4 - g.innercond.Width - 4 - g.upbutton.Width + 8;
                    c.value.Enabled = !ConditionEntry.IsNullOperation(c.cond.Text) && !ConditionEntry.IsUnaryOperation(c.cond.Text);
                    if (!c.value.Enabled)
                        c.value.Text = "";

                    c.del.Location = new Point(c.value.Right + panelxspacing, vnextcond);
                    c.more.Location = new Point(c.del.Right + panelxspacing, vnextcond);
                    c.more.Visible = (condc == g.condlist.Count - 1);

                    vnextcond += c.fname.Height + panelyspacing;
                    farx = c.more.Left;     // where the innercond/up buttons are
                }

                // and the outer/inner conditions

                g.innercond.Visible = (g.condlist.Count > 1);       // inner condition on if multiple
                g.innercond.Location = new Point(farx, panelymargin);    // inner condition is in same place as more button
                farx = g.innercond.Right + panelxspacing;                       // move off    

                // and the up button.. 
                g.upbutton.Visible = (i != 0 && g.condlist.Count > 0);
                g.upbutton.Location = new Point(farx, panelymargin);

                // allocate space for the outercond if req.
                g.outercond.Enabled = g.outercond.Visible = g.outerlabel.Visible = showouter;       // and enabled/disable the outer condition switch

                if (showouter)
                {
                    g.outercond.Location = new Point(panelxmargin, vnextcond);
                    g.outerlabel.Location = new Point(g.outercond.Location.X + g.outercond.Width + panelxspacing, g.outercond.Location.Y + 3);
                    vnextcond += g.outercond.Height+ panelyspacing;
                }

                // pos the panel

                g.panel.Location = new Point(panelxmargin, y + panelVScroll.ScrollOffset);
                g.panel.Size = new Size(Math.Max(panelwidth - panelxmargin * 2, farx), Math.Max(vnextcond + panelyspacing, g.innercond.Bottom + panelyspacing));
                g.panel.BorderStyle = (g.condlist.Count > 1) ? BorderStyle.FixedSingle : BorderStyle.None;

                y += g.panel.Height + panelyspacing*2;

                // and make sure actions list is right

                g.panel.ResumeLayout();
            }

            if (allowoutercond || groups.Count == 0)
            {
                buttonMore.Location = new Point(panelxmargin, y + panelVScroll.ScrollOffset);
                buttonMore.Visible = true;
                y = buttonMore.Bottom;
            }
            else
                buttonMore.Visible = false;

            if (calcminsize )
                onCalcMinsize?.Invoke(y);

            panelVScroll.ResumeLayout();
            ResumeLayout();
            Invalidate(true);
            Update();
        }

        #endregion


#region Checking

        public string Check()   // calculate if in error, fill Result with what is valid. Empty string if ok
        {
            Result = new ConditionLists();

            string errorlist = "";

            foreach (Group g in groups)
            {
                string innerc = g.innercond.Text;
                string outerc = g.outercond.Text;
                string evt = (eventlist != null) ? g.evlist.Text : "Default";

                //System.Diagnostics.Debug.WriteLine("Event {0} inner {1} outer {2} action {3} data '{4}'", evt, innerc, outerc, actionname, actiondata );

                Condition fe = new Condition();

                if (evt.Length == 0)        // must have name
                    errorlist += "Ignored group with empty name" + Environment.NewLine;
                else
                {
                    if (fe.Create(evt, "","", innerc, outerc)) // create must work
                    {
                        for (int i = 0; i < g.condlist.Count; i++)
                        {
                            Group.Conditions c = g.condlist[i];
                            string fieldn = c.fname.Text;
                            string condn = c.cond.Text;
                            string valuen = c.value.Text;

                            if (fieldn.Length > 0 || ConditionEntry.IsNullOperation(condn))
                            {
                                ConditionEntry f = new ConditionEntry();

                                if (f.Create(fieldn, condn, valuen))
                                {
                                    if (valuen.Length == 0 && !ConditionEntry.IsUnaryOperation(condn) && !ConditionEntry.IsNullOperation(condn))
                                        errorlist += "Do you want filter '" + fieldn + "' in group '" + fe.eventname + "' to have an empty value" + Environment.NewLine;

                                    fe.Add(f);
                                }
                                else
                                    errorlist += "Cannot create filter '" + fieldn + "' in group '" + fe.eventname + "' check value" + Environment.NewLine;
                            }
                            else
                                errorlist += "Ignored empty filter " + (i+1).ToString(System.Globalization.CultureInfo.InvariantCulture) + " in " + fe.eventname + Environment.NewLine;
                        }

                        if (fe.fields != null)
                            Result.Add(fe);
                        else
                            errorlist += "No valid filters found in group '" + fe.eventname + "'" + Environment.NewLine;
                    }
                    else
                        errorlist += "Cannot create " + evt + " not a normal error please report" + Environment.NewLine;
                }
            }

            return errorlist;
        }

        public DialogResult CheckAndAsk()      // OK - all okay, else Retry, Abort, Cancel.  result is set..
        {
            string res = Check();

            if (res.HasChars())
            {
                string acceptstr = "Click Retry to correct errors, Abort to cancel, Ignore to accept valid entries";

                DialogResult dr = ExtendedControls.MessageBoxTheme.Show(this, "Filters produced the following warnings and errors" + Environment.NewLine + Environment.NewLine + res + Environment.NewLine + acceptstr,
                                                  "Warning", MessageBoxButtons.AbortRetryIgnore );

                return dr;
            }

            return DialogResult.OK;
        }


#endregion

        #region Autocomplete event field types.. complicated

        List<string> AutoCompletor(string s, ExtendedControls.ExtTextBoxAutoComplete t)
        {
            Tuple<Group, Group.Conditions> gc = t.Tag as Tuple<Group, Group.Conditions>;

            List<string> ret = new List<string>();

            if (gc.Item1.variables != null)
            {
                foreach (var x in gc.Item1.variables)
                {
                    if (x.Name.StartsWith(s, StringComparison.InvariantCultureIgnoreCase))
                        ret.Add(x.Name + (x.Comment != null ? (" " + commentmarker + " "+ x.Comment) : ""));
                }
            }

            return ret;
        }

        private List<TypeHelpers.PropertyNameInfo> CreateVariables(string evname)       // may return null
        {
            List<TypeHelpers.PropertyNameInfo> names = null;

            if (evname.HasChars())                                      // if event name present..
                names = VariableNamesEvents?.Invoke(evname) ?? null;       // if present, call it, else null.  may return null

            if (names == null)
                names = VariableNames;
            else if (VariableNames != null)
                names.AddRange(VariableNames);

            if ( names != null )
                names.Sort(delegate (TypeHelpers.PropertyNameInfo left, TypeHelpers.PropertyNameInfo right) { return left.Name.CompareTo(right.Name); });

            return names;
        }

        #endregion

        #region Variables

        private List<string> eventlist;
        private bool allowoutercond;

        private class Group
        {
            public Panel panel;
            public ExtendedControls.ExtButton upbutton;
            public ExtendedControls.ExtComboBox evlist;
            public ExtendedControls.ExtComboBox innercond;
            public ExtendedControls.ExtComboBox outercond;
            public Label outerlabel;
            public List<TypeHelpers.PropertyNameInfo> variables;        // collated variables against the evlist and VariableNames..

            public class Conditions
            {
                public ExtendedControls.ExtTextBoxAutoComplete fname;
                public ExtendedControls.ExtComboBox cond;
                public ExtendedControls.ExtTextBox value;
                public ExtendedControls.ExtButton del;
                public ExtendedControls.ExtButton more;
                public Group group;
            }

            public List<Conditions> condlist = new List<Conditions>();
        }

        private List<Group> groups;

        private const int panelxmargin = 3;
        private const int panelymargin = 3;
        private const string commentmarker = "|";

        #endregion

    }
}
