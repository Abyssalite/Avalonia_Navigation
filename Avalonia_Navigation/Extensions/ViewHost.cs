using System.ComponentModel;
namespace Avalonia_Navigation;

public sealed class ViewHost : IViewHost, INotifyPropertyChanged
{
    private object? _sideView;
    private object? _mainView;
    private object? _topBar;

    public object? SideView
    {
        get => _sideView;
        set
        {
            if (!ReferenceEquals(_sideView, value))
            {
                _sideView = value;
                OnPropertyChanged(nameof(SideView));
            }
        }
    }

    public object? MainView
    {
        get => _mainView;
        set
        {
            if (!ReferenceEquals(_mainView, value))
            {
                _mainView = value;
                OnPropertyChanged(nameof(MainView));
            }
        }
    }

    public object? TopBar
    {
        get => _topBar;
        set
        {
            if (!ReferenceEquals(_topBar, value))
            {
                _topBar = value;
                OnPropertyChanged(nameof(TopBar));
            }
        }
    }

    public Task NavigateSide(object? viewModel)
    {
        SideView = viewModel;
        return Task.CompletedTask;
    }

    public Task NavigateMain(object? viewModel)
    {
        MainView = viewModel;
        return Task.CompletedTask;
    }

    public Task ChangeTopBar(object? viewModel)
    {
        TopBar = viewModel;
        return Task.CompletedTask;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}