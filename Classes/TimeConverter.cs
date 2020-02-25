using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        //Split
        private int hours { get; set; }
        private int minutes { get; set; }
        private int seconds { get; set; }

        // For NCrunch it works by "\n"only , 
        //For NUnit, the Delimiter should be "\r\n"
        private string delimiter
        {
            get { return "\n"; } 
           
        }
        /// <summary>
        /// Convert Time to Berlin Clock
        /// </summary>
        /// <param name="aTime"> format is "00:00:00"</param>
        /// <returns>the rows of lamps of Berlin Clock.</returns>
        public string convertTime(string aTime)
        {
            StringBuilder sb = new StringBuilder();
       
            var result = aTime.Split(':');
            hours = int.Parse(result[0]);
            minutes = int.Parse(result[1]);
            seconds = int.Parse(result[2]);

            // Add Y or O for the first row
            sb.Append(getSeconds(seconds));
            sb.Append(delimiter);
            sb.Append(getHours(hours));
            sb.Append(delimiter);
            sb.Append(getMinutes(minutes));
            return sb.ToString();
        }
        private string getMinutes(int minutes)
        {
            int numberTopMinutesLamps = minutes / 5;
            int numberBottomMinutesLamps = minutes % 5;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < numberTopMinutesLamps; i++)
            {
                sb.Append((i + 1) % 3 == 0 ? "R" : "Y");
            }
            for (int i = numberTopMinutesLamps; i < 11; i++)
            {
                sb.Append("O");
            }
            sb.Append(delimiter);
            sb.Append(getLampRow(4, numberBottomMinutesLamps, "Y"));
            return sb.ToString();
        }

        /// <summary>
        /// Calculates the top row lamps by dividing by 5 and the the buttom hour lamps by % 5.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        private string getHours(int hours)
        {
            int numberTopHourLamps = hours / 5;
            int numberBottomHourLamps = hours % 5;

            StringBuilder sb = new StringBuilder();
            sb.Append(getLampRow(4, numberTopHourLamps, "R"))
                    .Append(delimiter)
                    .Append(getLampRow(4, numberBottomHourLamps, "R"));

            return sb.ToString();
        }

        private string getLampRow(int totalNumberLamps, int numberLampsOn, string lampSymbol)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberLampsOn; i++)
            {
                sb.Append(lampSymbol);
            }
            for (int i = numberLampsOn; i < totalNumberLamps; i++)
            {
                sb.Append("O");
            }
            return sb.ToString();
        }

        private string getSeconds(int seconds)
        {
            return seconds % 2 == 0 ? "Y" : "O";
        }
    }
}
