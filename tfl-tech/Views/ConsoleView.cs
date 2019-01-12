
namespace tfl_tech.Views
{
    /// <summary>
    /// A view for printing to the console
    /// </summary>
    public class ConsoleView : IView
    {
        /// <summary>
        /// The string output of the view
        /// </summary>
        public string Output { get; private set; }

        /// <summary>
        /// The status code of the view
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Creates a new Console View
        /// </summary>
        /// <param name="message">The string output to use</param>
        /// <param name="status">The status of the message (0 is OK, everything else represents an error)</param>
        public ConsoleView(string message, int status = 0)
        {
            Output = message;
            StatusCode = status;
        }
    }
}
