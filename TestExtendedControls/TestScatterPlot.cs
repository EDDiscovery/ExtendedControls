using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class TestScatterPlot : Form
    {
        ThemeStandard theme;

        public TestScatterPlot()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();        
            //AddDemoContent();           
            
        }
                
        private void AddDemoContent()
        {
            extScatterPlot1.Clear();
                        
            Random rand = new Random();
            double R = 1;
            List<double[]> Points = new List<double[]>();

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    double theta = Math.PI * rand.NextDouble();
                    double phi = 2 * Math.PI * rand.NextDouble();
                    double x = R * Math.Sin(theta) * Math.Cos(phi);
                    double y = R * Math.Sin(theta) * Math.Sin(phi);
                    double z = R * Math.Cos(theta);
                    Points.Add(new double[] { x, y, z });
                }
                extScatterPlot1.AddPoints(Points);

                Points.Clear();
            }
        }
    }
}

