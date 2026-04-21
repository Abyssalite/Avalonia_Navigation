namespace Avalonia_Navigation;

public interface IHandleLastPage
{
    /// <summary>
    /// Returns true if the back action was handled by the current view model
    /// and navigation should stop.
    /// Returns false if navigator should continue normal back navigation.
    /// </summary>
    Task HandleLastPageAsync();
}