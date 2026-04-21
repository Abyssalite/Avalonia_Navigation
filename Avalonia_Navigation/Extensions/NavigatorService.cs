namespace Avalonia_Navigation;

public sealed class NavigatorService : INavigatorService
{
    private readonly IViewHost _host;
    private bool _isExit;

    private readonly Stack<object?> _sideHistory = new();
    private readonly Stack<NavigationEntry> _mainHistory = new();

    private object? _currentSide;
    private object? _currentMain;
    private object? _currentTopBar;

    public NavigationEntry? FirstView { get; set; }

    public NavigatorService(IViewHost host)
    {
        _host = host;
    }

    public async Task NavigateSide(object? viewModel)
    {
        _isExit = false;

        if (_currentSide != null)
            _sideHistory.Push(_currentSide);

        _currentSide = viewModel;
        await _host.NavigateSide(_currentSide);
    }

    public async Task NavigateMain(NavigationEntry view)
    {
        _isExit = false;

        if (_currentMain != null)
            _mainHistory.Push(new NavigationEntry(_currentMain, _currentTopBar));

        _currentMain = view.ViewModel;
        _currentTopBar = view.TopBar;

        await _host.ChangeTopBar(_currentTopBar);
        await _host.NavigateMain(_currentMain);
    }

    public bool IsExit()
    {
        if (_isExit)
            return true;

        if (_mainHistory.Count == 0)
            _isExit = true;

        return false;
    }

    public void ClearStack()
    {
        if (_mainHistory.Count > 0 && FirstView is not null)
        {
            _currentMain = FirstView.ViewModel;
            _currentTopBar = FirstView.TopBar;
            _mainHistory.Clear();
        }
    }

    public async Task OpenPrevious()
    {
        if (_currentMain is IHandleBackNavigation backHandler)
        {
            var handled = await backHandler.HandleBackAsync();
            if (handled)
                return;
        }

        if (_mainHistory.Count > 0)
        {
            var previous = _mainHistory.Pop();
            _currentMain = previous.ViewModel;
            _currentTopBar = previous.TopBar;

            await _host.NavigateMain(_currentMain);
            await _host.ChangeTopBar(_currentTopBar);

            if (_currentMain is IHandleLastPage lastHandler && _mainHistory.Count == 0)
                await lastHandler.HandleLastPageAsync();
        }

        if (_sideHistory.Count > 0)
        {
            _currentSide = _sideHistory.Pop();
            await _host.NavigateSide(_currentSide);
        }
    }
}