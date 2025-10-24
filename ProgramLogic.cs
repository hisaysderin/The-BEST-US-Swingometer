using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_BEST_US_Swingometer
{
    public class ProgramLogic
    {
        public Party Republicans = new Party();
        public Party Democrats = new Party();
        public Party Others = new Party();

        public double inputD;
        public double inputR;
        public double inputI;

        public Area tempArea; // used in swingometer
        public int mode;
        public FileHandler Files = new FileHandler();
        public List<Area> inputAreas = new List<Area>();

        public List<Area> OutputAreas = new List<Area>();

        public string colourMode;

        public void Setup()
        {

            // GOP data
            Republicans.name = "GOP";

            Republicans.userPercentage = inputR;
            Republicans.lastPercentages = new List<double> { 49.8, 49.8, 49.3, 49.1};
            Republicans.CalculateSwing();

            Republicans.lastElectoralVote = 312;
            Republicans.lastHouseSeats = 220;
            Republicans.lastSenateSeats = 53;
            Republicans.heldSenateSeats = 31;

            Republicans.lastGovernorSeats = 27;
            Republicans.heldGovernorSeats = 8;

            // DEM data
            Democrats.name = "DEM";

            Democrats.userPercentage = inputD;
            Democrats.lastPercentages = new List<double> { 48.3, 47.2, 47.0, 49.3};
            Democrats.CalculateSwing();

            Democrats.lastElectoralVote = 226;
            Democrats.lastHouseSeats = 215;
            Democrats.lastSenateSeats = 45;
            Democrats.heldSenateSeats = 32;

            Democrats.lastGovernorSeats = 23;
            Democrats.heldGovernorSeats = 6;

            // OTH data
            Others.name = "OTH";

            Others.userPercentage = inputI;
            Others.lastPercentages = new List<double> { 1.9, 3.0, 3.2, 1.7};
            Others.CalculateSwing();

            Others.lastElectoralVote = 0;
            Others.lastHouseSeats = 0;
            Others.lastSenateSeats = 2;
            Others.heldSenateSeats = 2;

            Others.lastGovernorSeats = 0;
            Others.heldGovernorSeats = 0;

        }

        // the program will cycle through a list of areas, doing this function on each of them, producing a new list of areas
        public Area Swingometer(Area currentArea, List<double> inputDem, List<double> inputGop, List<double> inputOth)
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

                case "Governors":
                    mode = 3;
                    break;
            }

            tempArea = new Area(currentArea.name, currentArea.shortName, currentArea.type, currentArea.electoralValue, currentArea.previousWinner,
                currentArea.republicanPercentage + inputGop[mode],
                currentArea.democraticPercentage + inputDem[mode],
                currentArea.otherPercentage + inputOth[mode], colourMode);
            

            if (currentArea.state != null)
            {
                tempArea.state = currentArea.state;
            }

            if (currentArea.republicanPercentage + inputGop[mode] > currentArea.democraticPercentage + inputDem[mode] &&
                currentArea.republicanPercentage + inputGop[mode] > currentArea.otherPercentage + inputOth[mode]) // GOP win
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
                    case "Governors":
                        Republicans.newGovernorSeats += 1;
                        break;
                }

                if (currentArea.previousWinner == "GOP") // a hold
                {
                    tempArea.finalResult = "GOP hold";
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                }
                else // a gain
                {
                    tempArea.finalResult = "GOP gain from " + currentArea.previousWinner;
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                }
            }

            else if (currentArea.democraticPercentage + inputDem[mode] > currentArea.republicanPercentage + inputGop[mode] &&
                     currentArea.democraticPercentage + inputDem[mode] > currentArea.otherPercentage + inputOth[mode]) // DEM win
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
                    case "Governors":
                        Democrats.newGovernorSeats += 1;
                        break;
                }

                if (currentArea.previousWinner == "DEM") // a hold
                {
                    tempArea.finalResult = "DEM hold";
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                }
                else // a gain
                {
                    tempArea.finalResult = "DEM gain from " + currentArea.previousWinner;
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                }
            }

            else if (currentArea.otherPercentage + inputOth[mode] > currentArea.republicanPercentage + inputGop[mode] &&
                currentArea.otherPercentage + inputOth[mode] > currentArea.democraticPercentage + inputDem[mode]) // OTH win
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
                    case "Governors":
                        Others.newGovernorSeats += 1;
                        break;
                }
                
                if (currentArea.previousWinner == "OTH") // a hold
                {
                    tempArea.finalResult = "OTH hold";
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                } 
                else // a gain
                {
                    tempArea.finalResult = "OTH gain from " + currentArea.previousWinner;
                    tempArea.CalculateMargin();
                    tempArea.GetColour();
                    return tempArea;
                }
            }

            else // any scenario the program can't handle
            {
                MessageBox.Show("Tie error, please adjust values", "Tie error");
                return tempArea;
            }
        }

        public void GetOutput(int mode) // 0 for pres, 1 for house, 2 for senate, 3 for governor
        {
            switch (mode)
            {
                case 0:
                    Files.filePath = "..\\..\\csv\\presidential.csv";
                    Files.ParseFile("Presidential");
                    break;
                case 1:
                    Files.filePath = "..\\..\\csv\\house_by_district.csv";
                    Files.ParseFile("House");
                    break;
                case 2:
                    Files.filePath = "..\\..\\csv\\senate.csv";
                    Files.ParseFile("Senate");
                    break;
                case 3:
                    Files.filePath = "..\\..\\csv\\governors.csv";
                    Files.ParseFile("Governors");
                    break;
            }

            inputAreas = Files.fileAreas;

            foreach (Area singleInputArea in inputAreas)
            {
                OutputAreas.Add(Swingometer(singleInputArea, Democrats.newPercentages, Republicans.newPercentages, Others.newPercentages));

            }

            if (mode == 2)
            {
                Democrats.CalculateSenate();
                Republicans.CalculateSenate();
                Others.CalculateSenate();
            }
            else if (mode == 1)
            {
                Democrats.CalculateHouseChange();
                Republicans.CalculateHouseChange();
                Others.CalculateHouseChange();
            }
            else if (mode == 3)
            {
                Democrats.CalculateGovernorChange();
                Republicans.CalculateGovernorChange();
                Others.CalculateGovernorChange();
            }
            

            
        }
        
    }
}
