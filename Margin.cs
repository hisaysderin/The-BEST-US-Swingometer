using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_BEST_US_Swingometer
{
    public class Margin
    {
        public string winner;
        public double number;
        public string complete;

        public Margin(string winner, double number)
        {
            this.winner = winner;
            this.number = number;
        }

        public void createComplete()
        {
            complete = winner + Convert.ToString(number);
        }
    }
}
