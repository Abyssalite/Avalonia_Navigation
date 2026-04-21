namespace Avalonia_Navigation;

public interface IViewHost
{
    object? SideView { get; set; }
    object? MainView { get; set; }
    object? TopBar { get; set; }

    Task NavigateSide(object? viewModel);
    Task NavigateMain(object? viewModel);
    Task ChangeTopBar(object? viewModel);
}

