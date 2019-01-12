
using System.Text;

namespace tfl_tech.Models
{
    public struct RoadStatus
    {
        public string displayName;
        public string statusSeverity;
        public string statusSeverityDescription;

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
