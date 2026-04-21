namespace Avalonia_Navigation;

public interface INavigatorService
{
    NavigationEntry? FirstView { get; set; }

    Task NavigateSide(object? viewModel);
    Task NavigateMain(NavigationEntry viewModel);
    Task OpenPrevious();

    void ClearStack();
    bool IsExit();

}
