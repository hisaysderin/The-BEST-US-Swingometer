using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace The_BEST_US_Swingometer
{
    public class FileHandler
    {
        public string fileContents;
        public List<string> itemData;
        public string filePath = "";

        public string[] dataPoints;
        public Area tempArea;
        public List<Area> fileAreas = new List<Area>();

        public string colourMode;

        public void ParseFile(string Type)
        {
            fileContents = File.ReadAllText(filePath);

            // split each item up
            itemData = fileContents.Split('\n').ToList();
            itemData.RemoveAt(0);

            foreach (string item in itemData)
            {
                dataPoints = item.Split(',');

                switch (Type)
                {
                    case "Presidential":
                        fileAreas.Add(new Area(dataPoints[0], dataPoints[1], "Presidential", Convert.ToInt16(dataPoints[2]), dataPoints[3],
                    Convert.ToDouble(dataPoints[4]), Convert.ToDouble(dataPoints[5]), Convert.ToDouble(dataPoints[6]), colourMode));
                        break;

                    case "Senate":
                        fileAreas.Add(new Area(dataPoints[0], dataPoints[1], "Senate", 1, dataPoints[2],
                    Convert.ToDouble(dataPoints[3]), Convert.ToDouble(dataPoints[4]), Convert.ToDouble(dataPoints[5]), colourMode));
                        break;

                    case "House":
                        tempArea = new Area(dataPoints[0], dataPoints[1], "House", 1, dataPoints[3],
                    Convert.ToDouble(dataPoints[4]), Convert.ToDouble(dataPoints[5]), Convert.ToDouble(dataPoints[6]), colourMode);
                        tempArea.state = dataPoints[2];

                        fileAreas.Add(tempArea);
                        break;

                    case "Governors":
                        fileAreas.Add(new Area(dataPoints[0], dataPoints[1], "Governors", 1, dataPoints[2],
                            Convert.ToDouble(dataPoints[3]), Convert.ToDouble(dataPoints[4]), Convert.ToDouble(dataPoints[5]), colourMode));
                        break;
                }
            }
        }

    }
}
