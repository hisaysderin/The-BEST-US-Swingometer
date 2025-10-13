using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Margin winningMargin = new Margin();
        public string finalResult;
        public string colour;
        public Color realColour;

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

        public void GetColour()
        {
            switch (winningMargin.winner)
            {
                case "D":

                    if (winningMargin.number < 1)
                    {
                        colour = "#949BB3";
                    }
                    else if (winningMargin.number >= 1 && winningMargin.number < 5)
                    {
                        colour = "#8AAFFF";
                    }
                    else if (winningMargin.number >= 5 && winningMargin.number < 10)
                    {
                        colour = "#577CCC";
                    }
                    else if (winningMargin.number >= 10 && winningMargin.number < 20)
                    {
                        colour = "#0023D1";
                    }
                    else if (winningMargin.number >= 20 && winningMargin.number < 30)
                    {
                        colour = "#1C408C";
                    }
                    else if (winningMargin.number > 30)
                    {
                        colour = "#1D2A5D";
                    }
                    break;

                case "R":
                    if (winningMargin.number < 1)
                    {
                        colour = "#CF8980";
                    }
                    else if (winningMargin.number >= 1 && winningMargin.number < 5)
                    {
                        colour = "#FF8B98";
                    }
                    else if (winningMargin.number >= 5 && winningMargin.number < 10)
                    {
                        colour = "#FF5865";
                    }
                    else if (winningMargin.number >= 10 && winningMargin.number < 20)
                    {
                        colour = "#FF0026";
                    }
                    else if (winningMargin.number >= 20 && winningMargin.number < 30)
                    {
                        colour = "#BF1D29";
                    }
                    else if (winningMargin.number > 30)
                    {
                        colour = "#610000";
                    }
                    break;

                case "I":
                    colour = "#F1C92A";
                    break;
            }

            realColour = ColorTranslator.FromHtml(colour);

        }

    }
}
