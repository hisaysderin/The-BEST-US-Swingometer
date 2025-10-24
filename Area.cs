using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public double winnersPercentage;

        public Margin winningMargin = new Margin();
        public string finalResult;
        public string colour;
        public Color realColour;

        public string state; // for house districts

        // colour dictionaries
        // List goes DEMS, REPS, INDS
        Dictionary<string, string[]> margin_colourDict = new Dictionary<string, string[]>();
        Dictionary<string, string[]> share_colourDict = new Dictionary<string, string[]>();
        Dictionary<string, string[]> gains_colourDict = new Dictionary<string, string[]>();
        Dictionary<string, string[]> currentDict = new Dictionary<string, string[]>();
        public string colourMode; // margin, share or gains

        public Area(string name, string shortName, string type, int electoralValue, string previousWinner, double republicanPercentage, double democraticPercentage, double otherPercentage, string colourMode)
        {
            this.name = name;
            this.shortName = shortName;
            this.type = type;
            this.electoralValue = electoralValue;
            this.previousWinner = previousWinner;
            this.republicanPercentage = republicanPercentage;
            this.democraticPercentage = democraticPercentage;
            this.otherPercentage = otherPercentage;
            this.colourMode = colourMode;

            this.winnersPercentage = new List<double> { republicanPercentage, democraticPercentage, otherPercentage }.Max();
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
        public void InitDictionaries()
        {

            margin_colourDict.Add((winningMargin.number < 1).ToString() + "0", new string[]{"#949BB3", "#CF8980", "#F1C92A"});
            margin_colourDict.Add((winningMargin.number >= 1 && winningMargin.number < 5).ToString() + "1", new string[]{"#8AAFFF", "#FF8B98", "#F1C92A"});
            margin_colourDict.Add((winningMargin.number >= 5 && winningMargin.number < 10).ToString() + "2", new string[]{"#577CCC", "#FF5865", "#F1C92A"});
            margin_colourDict.Add((winningMargin.number >= 10 && winningMargin.number < 20).ToString() + "3", new string[] { "#0023D1" , "#FF0026" , "#F1C92A" });
            margin_colourDict.Add((winningMargin.number >= 20 && winningMargin.number < 30).ToString() + "4", new string[] { "#1C408C" , "#BF1D29" , "#F1C92A" });
            margin_colourDict.Add((winningMargin.number >= 30).ToString() + "5", new string[] { "#1D2A5D" , "#610000" , "#F1C92A" });

            share_colourDict.Add((winnersPercentage < 30).ToString() + "0", new string[] { "#E1EFFF", "#FFDFE1", "#FEF7CB"});
            share_colourDict.Add((winnersPercentage >= 30 && winnersPercentage < 40).ToString() + "1", new string[] { "#D3E7FF" , "#FFCCD0" , "#FEF4B4" });
            share_colourDict.Add((winnersPercentage >= 40 && winnersPercentage < 50).ToString() + "2", new string[] { "#B9D7FF" , "#F2B3BE" , "#FEE391" });
            share_colourDict.Add((winnersPercentage >= 50 && winnersPercentage < 60).ToString() + "3", new string[] { "#86B6F2" , "#E27F90" , "#FED463" });
            share_colourDict.Add((winnersPercentage >= 60 && winnersPercentage < 70).ToString() + "4", new string[] { "#4389E3" , "#CC2F4A" , "#FE9929" });
            share_colourDict.Add((winnersPercentage >= 70 && winnersPercentage < 80).ToString() + "5", new string[] { "#1666CB" , "#D40000" , "#EC7014" });
            share_colourDict.Add((winnersPercentage >= 80 && winnersPercentage < 90).ToString() + "6", new string[] { "#0645B4" , "#AA0000" , "#CC4C02" });
            share_colourDict.Add((winnersPercentage >= 90).ToString() + "7", new string[] { "#002B84" , "#800000" , "#8C2D04" });

            gains_colourDict.Add((finalResult.Contains("hold")).ToString() + "0", new string[] { "#92C5DE" , "#F48882" , "#F2BFA6" });
            gains_colourDict.Add((finalResult.Contains("gain")).ToString() + "1", new string[] { "#0671B0" , "#CA0120" , "#FFD700" });
        }           
        public void GetColour()
        {
            InitDictionaries();

            switch (colourMode)
            {
                case "Margin":
                    currentDict = margin_colourDict;
                    break;
                case "Vote share":
                    currentDict = share_colourDict;
                    break;
                case "Gains":
                    currentDict = gains_colourDict;
                    break;
            }

            foreach (KeyValuePair<string, string[]> item in currentDict)
            {
                if (item.Key.Contains("True"))
                {
                    switch (winningMargin.winner)
                    {
                        case "D":
                            colour = item.Value[0];
                            break;
                        case "R":
                            colour = item.Value[1];
                            break;
                        case "I":
                            colour = item.Value[2];
                            break;
                    }
                }
            }

            realColour = ColorTranslator.FromHtml(colour);

        }

    }
}
