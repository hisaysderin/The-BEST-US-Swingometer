using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_BEST_US_Swingometer
{
    public class Party
    {
        public string name;
        public double userPercentage;

        public List<double> lastPercentages = new List<double>(); // order: President, House, Senate, Governor
        public List<double> newPercentages = new List<double>();

        public int lastElectoralVote;
        public int newElectoralVote = 0;

        public int lastHouseSeats;
        public int newHouseSeats = 0;
        public int houseChange;

        public int lastSenateSeats;
        public int heldSenateSeats; // some seats are not up for re-election
        public int newSenateSeats = 0;  // only seats contested
        public int totalSenateSeats;

        public int lastGovernorSeats;
        public int heldGovernorSeats;
        public int newGovernorSeats = 0;
        public int totalGovernorSeats;

        public void CalculateSwing()
        {
            foreach (double value in lastPercentages)
            {
                newPercentages.Add(Math.Round(userPercentage - value, 2));
            }
        }

        public void CalculateSenate()
        {
            totalSenateSeats = heldSenateSeats + newSenateSeats;
        }
        public void CalculateHouseChange()
        {
            houseChange = newHouseSeats - lastHouseSeats;
        }
        public void CalculateGovernorChange()
        {
            totalGovernorSeats = newGovernorSeats + heldGovernorSeats;
        }
    }
}