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

        public List<Area> inputHouseStates = new List<Area>(); // only used when calculating house results
        public List<Area> midHouseStates = new List<Area>(); // used to calculate swings
        public int counter = 0;

        public List<Area> OutputAreas = new List<Area>();

        public void Setup()
        {

            // GOP data
            Republicans.name = "GOP";

            Republicans.userPercentage = inputR; //CHANGE THIS WHEN WE DO THE GUI
            Republicans.lastPercentages = new List<double> { 49.8, 49.8, 49.3};
            Republicans.CalculateSwing();

            Republicans.lastElectoralVote = 312;
            Republicans.lastHouseSeats = 220;
            Republicans.lastSenateSeats = 53;
            Republicans.heldSenateSeats = 31;

            // DEM data
            Democrats.name = "DEM";

            Democrats.userPercentage = inputD;
            Democrats.lastPercentages = new List<double> { 48.3, 47.2, 47.0};
            Democrats.CalculateSwing();

            Democrats.lastElectoralVote = 226;
            Democrats.lastHouseSeats = 215;
            Democrats.lastSenateSeats = 45;
            Democrats.heldSenateSeats = 32;

            // OTH data
            Others.name = "OTH";

            Others.userPercentage = inputI; //CHANGE THIS WHEN WE DO THE GUI
            Others.lastPercentages = new List<double> { 1.9, 3.0, 3.2};
            Others.CalculateSwing();

            Others.lastElectoralVote = 0;
            Others.lastHouseSeats = 0;
            Others.lastSenateSeats = 2;
            Others.heldSenateSeats = 2;

            counter = 0;
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
            }

            tempArea = new Area(currentArea.name, currentArea.shortName, currentArea.type, currentArea.electoralValue, currentArea.previousWinner,
                currentArea.republicanPercentage + inputGop[mode],
                currentArea.democraticPercentage + inputDem[mode],
                currentArea.otherPercentage + inputOth[mode]);
            tempArea.CalculateMargin();
            tempArea.GetColour();

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
                MessageBox.Show("Tie error, please adjust values", "Tie error");
                return tempArea;
            }
        }

        public void GetOutput(int mode) // 0 for pres, 1 for house, 2 for senate
        {
            switch (mode)
            {
                case 0:
                    Files.filePath = "..\\..\\presidential.csv";
                    Files.ParseFile("Presidential");
                    break;
                case 1:
                    Files.filePath = "..\\..\\house_by_district.csv";
                    Files.ParseFile("House");
                    break;
                case 2:
                    Files.filePath = "..\\..\\senate.csv";
                    Files.ParseFile("Senate");
                    break;
            }

            inputAreas = Files.fileAreas;

            if (mode != 1)
            {
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
            }
            else //for the house, we do the state by state results, and then for each district we use the state change
            {
                foreach (Area singleInputArea in inputAreas)
                {
                    OutputAreas.Add(Swingometer(singleInputArea, Democrats.newPercentages, Republicans.newPercentages, Others.newPercentages));
                }

            }

            Democrats.CalculateHouseChange();
            Republicans.CalculateHouseChange();
            Others.CalculateHouseChange();

            
        }
        
    }
}
