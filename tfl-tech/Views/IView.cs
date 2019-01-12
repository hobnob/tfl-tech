
namespace tfl_tech.Views
{
    /// <summary>
    /// The interface that all views implement
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// The output of the view
        /// </summary>
        string Output { get; }

        /// <summary>
        /// The status of the view
        /// </summary>
        int StatusCode { get; }
    }
}
