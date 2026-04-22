namespace Avalonia_Navigation;

public interface INavigatorService
{
    NavigationEntry? FirstView { get; set; }

    Task NavigateSide(object? content);
    Task NavigateMain(NavigationEntry entry);
    Task OpenPrevious();

    void ClearStack();
    bool IsExit();
}