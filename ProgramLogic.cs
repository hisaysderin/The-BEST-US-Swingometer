using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace The_BEST_US_Swingometer
{
    public class ProgramLogic
    {
        public Party Republicans = new Party();
        public Party Democrats = new Party();
        public Party Others = new Party();

        public int mode; // used in swingometer to differentiate between Presidential, House or Senate (0, 1 or 2)
        public Area tempArea; // used in swingometer
        public FileHandler Files;

        public List<Area> OutputAreas;

        public void Setup()
        {

            // GOP data
            Republicans.name = "GOP";

            Republicans.userPercentage = 0; //CHANGE THIS WHEN WE DO THE GUI
            Republicans.lastPercentages = new List<double> { 49.8, 49.8, 49.3};
            Republicans.CalculateSwing();

            Republicans.lastElectoralVote = 312;
            Republicans.lastHouseSeats = 220;
            Republicans.lastSenateSeats = 53;
            Republicans.heldSenateSeats = 31;

            // DEM data
            Democrats.name = "DEM";

            Democrats.userPercentage = 0; //CHANGE THIS WHEN WE DO THE GUI
            Democrats.lastPercentages = new List<double> { 48.3, 47.2, 47.0};
            Democrats.CalculateSwing();

            Democrats.lastElectoralVote = 226;
            Democrats.lastHouseSeats = 215;
            Democrats.lastSenateSeats = 45;
            Democrats.heldSenateSeats = 32;

            // OTH data
            Others.name = "OTH";

            Others.userPercentage = 0; //CHANGE THIS WHEN WE DO THE GUI
            Others.lastPercentages = new List<double> { 1.9, 3.0, 3.2};
            Others.CalculateSwing();

            Others.lastElectoralVote = 0;
            Others.lastHouseSeats = 0;
            Others.lastSenateSeats = 2;
            Others.heldSenateSeats = 2;
        }

        // the program will cycle through a list of areas, doing this function on each of them, producing a new list of areas
        public Area Swingometer(Area currentArea)
        {
            Setup();

            switch (currentArea.type)
            {
                case "Presidential":
                    mode = 0;
                    break;

                case "House":
                    mode = 1;
                    break;

                case "Senate":
                    mode = 2;
                    break;
            }

            tempArea = new Area(currentArea.name, currentArea.shortName, currentArea.type, currentArea.electoralValue, currentArea.previousWinner,
                currentArea.republicanPercentage + Republicans.newPercentages[mode],
                currentArea.democraticPercentage + Democrats.newPercentages[mode],
                currentArea.otherPercentage + Others.newPercentages[mode]);
            tempArea.CalculateMargin();

            if (currentArea.state != null)
            {
                tempArea.state = currentArea.state;
            }

            if (currentArea.republicanPercentage + Republicans.newPercentages[mode] > currentArea.democraticPercentage + Democrats.newPercentages[mode] &&
                currentArea.republicanPercentage + Republicans.newPercentages[mode] > currentArea.otherPercentage + Others.newPercentages[mode]) // GOP win
            {
                switch (currentArea.type)
                {
                    case "Presidential":
                        Republicans.newElectoralVote += currentArea.electoralValue;
                        break;
                    case "House":
                        Republicans.newHouseSeats += 1;
                        break;
                    case "Senate":
                        Republicans.newSenateSeats += 1;
                        break;
                }

                if (currentArea.previousWinner == "GOP") // a hold
                {
                    tempArea.finalResult = "GOP hold";
                    return tempArea;
                }
                else // a gain
                {
                    tempArea.finalResult = "GOP gain from " + currentArea.previousWinner;
                    return tempArea;
                }
            }

            else if (currentArea.democraticPercentage + Democrats.newPercentages[mode] > currentArea.republicanPercentage + Republicans.newPercentages[mode] &&
                     currentArea.democraticPercentage + Democrats.newPercentages[mode] > currentArea.otherPercentage + Others.newPercentages[mode]) // DEM win
            {
                switch (currentArea.type)
                {
                    case "Presidential":
                        Democrats.newElectoralVote += currentArea.electoralValue;
                        break;
                    case "House":
                        Democrats.newHouseSeats += 1;
                        break;
                    case "Senate":
                        Democrats.newSenateSeats += 1;
                        break;
                }

                if (currentArea.previousWinner == "DEM") // a hold
                {
                    tempArea.finalResult = "DEM hold";
                    return tempArea;
                }
                else // a gain
                {
                    tempArea.finalResult = "DEM gain from " + currentArea.previousWinner;
                    return tempArea;
                }
            }

            else if (currentArea.otherPercentage + Others.newPercentages[mode] > currentArea.republicanPercentage + Republicans.newPercentages[mode] &&
                currentArea.otherPercentage + Others.newPercentages[mode] > currentArea.democraticPercentage + Democrats.newPercentages[mode]) // OTH win
            {
                switch (currentArea.type)
                {
                    case "Presidential":
                        Others.newElectoralVote += currentArea.electoralValue;
                        break;
                    case "House":
                        Others.newHouseSeats += 1;
                        break;
                    case "Senate":
                        Others.newSenateSeats += 1;
                        break;
                }
                
                if (currentArea.previousWinner == "OTH") // a hold
                {
                    tempArea.finalResult = "OTH hold";
                    return tempArea;
                } 
                else // a gain
                {
                    tempArea.finalResult = "OTH gain from " + currentArea.previousWinner;
                    return tempArea;
                }
            }

            else // any scenario the program can't handle
            {
                throw new Exception("Something went wrong when checking out who won. One or more values have tied and the program can't handle this.");
            }
        }

        public void GetOutput()
        {
            switch (mode)
            {
                case 0:
                    Files.filePath = "..\\presidential.csv";
                    break;
                case 1:
                    Files.filePath = "..\\house_by_district.csv";
                    break;
                case 2:
                    Files.filePath = "..\\senate.csv";
                    break;
            }
        }
        
    }
}
