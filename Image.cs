using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_BEST_US_Swingometer
{
    public class Image
    {
        public string name;
        public string filePath;
        public string purpose; //Presidential, Senate, House or Meta
        public string state;

        public string colour;

        public void getColour(Margin margin)
        {
            switch (margin.winner)
            {
                case "D":

                    if (margin.number < 1)
                    {
                        colour = "#949BB3";
                    }
                    else if (margin.number >= 1 && margin.number < 5)
                    {
                        colour = "#8AAFFF";
                    }
                    else if (margin.number >= 5 && margin.number < 10)
                    {
                        colour = "#577CCC";
                    }
                    else if (margin.number >= 10 && margin.number < 20)
                    {
                        colour = "#0023D1";
                    }
                    else if (margin.number >= 20 && margin.number < 30)
                    {
                        colour = "#1C408C";
                    }
                    else if (margin.number > 30)
                    {
                        colour = "#1D2A5D";
                    }
                    break;

                case "R":
                    if (margin.number < 1)
                    {
                        colour = "#CF8980";
                    }
                    else if (margin.number >= 1 && margin.number < 5)
                    {
                        colour = "#FF8B98";
                    }
                    else if (margin.number >= 5 && margin.number < 10)
                    {
                        colour = "#FF5865";
                    }
                    else if (margin.number >= 10 && margin.number < 20)
                    {
                        colour = "#FF0026";
                    }
                    else if (margin.number >= 20 && margin.number < 30)
                    {
                        colour = "#BF1D29";
                    }
                    else if (margin.number > 30)
                    {
                        colour = "#610000";
                    }
                    break;

                case "I":
                    colour = "#F1C92A";
                    break;
            }
        }
    }
}
