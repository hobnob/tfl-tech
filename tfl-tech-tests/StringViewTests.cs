using NUnit.Framework;
using tfl_tech.Views;

namespace Tests
{
    public class StringViewTests
    {
        [Test]
        public void TestVariableSetting()
        {
            // Test that the default status is zero and that message propogates
            StringView view = new StringView("This is my message");
            Assert.AreEqual("This is my message", view.Output);
            Assert.AreEqual(0, view.StatusCode);

            // Test that sending a status code propogates
            view = new StringView("This is another message", 1);
            Assert.AreEqual("This is another message", view.Output);
            Assert.AreEqual(1, view.StatusCode);
        }
    }
}
