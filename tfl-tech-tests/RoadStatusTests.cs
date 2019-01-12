
using NUnit.Framework;
using tfl_tech.Models;

namespace Tests
{
    class RoadStatusTests
    {
        [Test]
        public void TestFormattedText()
        {
            RoadStatus status = new RoadStatus() {
                displayName = "<DISPLAYNAME>",
                statusSeverity = "<STATUS>",
                statusSeverityDescription = "<DESCRIPTION>"
            };

            Assert.AreEqual(
                "The status of the <DISPLAYNAME> is as follows\r\n\tRoad Status is <STATUS>\r\n\tRoad Status Description is <DESCRIPTION>",
                status.ToFormattedString()
            );
        }
    }
}
