using System.Text;

namespace tfl_tech.Models
{
    /// <summary>
    /// A structure for storing the status of a road
    /// </summary>
    public struct RoadStatus
    {
        /// <summary>
        /// The display name for the road
        /// </summary>
        public string displayName;

        /// <summary>
        /// The severity status of the road
        /// </summary>
        public string statusSeverity;

        /// <summary>
        /// A more detailed description of the severity
        /// </summary>
        public string statusSeverityDescription;

        /// <summary>
        /// Formats the data so that it can be output
        /// </summary>
        /// <returns>A string of the data</returns>
        public string ToFormattedString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("The status of the {0} is as follows\r\n", displayName);
            str.AppendFormat("\tRoad Status is {0}\r\n", statusSeverity);
            str.AppendFormat("\tRoad Status Description is {0}", statusSeverityDescription);

            return str.ToString();
        }
    }
}
