
namespace tfl_tech.Views
{
    public interface IView
    {
        string Output { get; }
        int StatusCode { get; }
    }
}
