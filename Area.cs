using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_BEST_US_Swingometer
{
    public class Area
    {
        public string name;
        public string shortName;
        public string type; // "Presidential", "Senate" or "House"
        public int electoralValue;
        public string previousWinner;

        public double republicanPercentage;
        public double democraticPercentage;
        public double otherPercentage;

        //public string winningMargin;
        public Margin winningMargin;
        public string finalResult;

        public string state; // for house districts

        public Area(string name, string shortName, string type, int electoralValue, string previousWinner, double republicanPercentage, double democraticPercentage, double otherPercentage)
        {
            this.name = name;
            this.shortName = shortName;
            this.type = type;
            this.electoralValue = electoralValue;
            this.previousWinner = previousWinner;
            this.republicanPercentage = republicanPercentage;
            this.democraticPercentage = democraticPercentage;
            this.otherPercentage = otherPercentage;
        }

        public void CalculateMargin()
        {
            if (republicanPercentage > democraticPercentage && republicanPercentage > otherPercentage)
            {
                winningMargin.winner = "R";

                if (democraticPercentage > otherPercentage)
                {
                    winningMargin.number = Math.Round(republicanPercentage - democraticPercentage, 2);
                }
                else
                {
                    winningMargin.number = Math.Round(republicanPercentage - otherPercentage, 2);
                }
            }

            else if (democraticPercentage > republicanPercentage && democraticPercentage > otherPercentage)
            {
                winningMargin.winner = "D";

                if (republicanPercentage > otherPercentage)
                {
                    winningMargin.number = Math.Round(democraticPercentage - republicanPercentage, 2);
                }
                else
                {
                    winningMargin.number = Math.Round(democraticPercentage - otherPercentage, 2);
                }
            }

            else if (otherPercentage > democraticPercentage && otherPercentage > democraticPercentage)
            {
                winningMargin.winner = "I";

                if (democraticPercentage > republicanPercentage)
                {
                    winningMargin.number = Math.Round(otherPercentage - democraticPercentage, 2);
                }
                else
                {
                    winningMargin.number = Math.Round(otherPercentage - republicanPercentage, 2);
                }
            }

            winningMargin.createComplete();
        }
    }
}
