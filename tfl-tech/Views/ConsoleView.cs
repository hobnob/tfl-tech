
namespace tfl_tech.Views
{
    public class ConsoleView : IView
    {
        public string Output { get; private set; }
        public int StatusCode { get; private set; }

        public ConsoleView(string message, int status = 0)
        {
            Output = message;
            StatusCode = status;
        }
    }
}
