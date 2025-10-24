using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_BEST_US_Swingometer
{
    public class ChartHandler
    {
        public Panel panel1;
        public Panel panel2;
        public Panel panel3;

        public Party dem;
        public Party gop;
        public Party oth;

        public Label label1;
        public Label label2;
        public Label label3;

        public int mode;

        public double demPercentage;
        public double gopPercentage;
        public double othPercentage;

        public List<int> totals = new List<int> {538, 435, 100, 50};

        public ChartHandler(Panel panel1, Panel panel2, Panel panel3, Party dem, Party gop, Party oth, int mode, 
            Label label1, Label label2, Label label3)
        {
            this.panel1 = panel1;
            this.panel2 = panel2;
            this.panel3 = panel3;
            this.dem = dem;
            this.gop = gop;
            this.oth = oth;
            this.mode = mode;
            this.label1 = label1;
            this.label2 = label2;
            this.label3 = label3;
        }

        public void DrawCharts()
        {
            panel1.Height = 200;
            panel2.Height = 200;
            panel3.Height = 200;

            switch (mode)
            {
                case 0:
                    demPercentage = (double)dem.newElectoralVote / (double)totals[0];
                    gopPercentage = (double)gop.newElectoralVote / (double)totals[0];
                    othPercentage = (double)oth.newElectoralVote / (double)totals[0];

                    label1.Text = dem.newElectoralVote.ToString();
                    label2.Text = gop.newElectoralVote.ToString();
                    label3.Text = oth.newElectoralVote.ToString();

                    break;
                case 1:
                    demPercentage = (double)dem.newHouseSeats / (double)totals[1];
                    gopPercentage = (double)gop.newHouseSeats / (double)totals[1];
                    othPercentage = (double)oth.newHouseSeats / (double)totals[1];

                    label1.Text = dem.newHouseSeats.ToString();
                    label2.Text = gop.newHouseSeats.ToString();
                    label3.Text = oth.newHouseSeats.ToString();

                    break;
                case 2:
                    demPercentage = (double)dem.totalSenateSeats / (double)totals[2];
                    gopPercentage = (double)gop.totalSenateSeats / (double)totals[2];
                    othPercentage = (double)oth.totalSenateSeats / (double)totals[2];

                    label1.Text = dem.totalSenateSeats.ToString();
                    label2.Text = gop.totalSenateSeats.ToString();
                    label3.Text = oth.totalSenateSeats.ToString();

                    break;

                case 3:
                    demPercentage = (double)dem.totalGovernorSeats / (double)totals[3];
                    gopPercentage = (double)gop.totalGovernorSeats / (double)totals[3];
                    othPercentage = (double)oth.totalGovernorSeats / (double)totals[3];

                    label1.Text = dem.totalGovernorSeats.ToString();
                    label2.Text = gop.totalGovernorSeats.ToString();
                    label3.Text = oth.totalGovernorSeats.ToString();

                    break;
            }
           
            panel1.Height = Convert.ToInt32(Math.Round(Convert.ToDouble(panel1.Height) * demPercentage));
            panel1.Location = new System.Drawing.Point(panel1.Location.X, 486 - panel1.Height);
            //MessageBox.Show(panel1.Height.ToString());

            panel2.Height = Convert.ToInt32(Math.Round(Convert.ToDouble(panel2.Height) * gopPercentage));
            panel2.Location = new System.Drawing.Point(panel2.Location.X, 486 - panel2.Height);

            panel3.Height = Convert.ToInt32(Math.Round(Convert.ToDouble(panel3.Height) * othPercentage));
            panel3.Location = new System.Drawing.Point(panel3.Location.X, 486 - panel3.Height);
        }
    }
}
