namespace Avalonia_Navigation;

public class NavigatorService : INavigatorService
{
    private readonly IViewHost _host;

    private readonly Stack<object?> _sideStack = new();
    private readonly Stack<NavigationEntry> _mainStack = new();

    private object? _currentSide;
    private NavigationEntry? _currentMain;
    private bool _isExit;

    public NavigationEntry? FirstView { get; set; }

    public NavigatorService(IViewHost host)
    {
        _host = host;
    }

    public async Task NavigateSide(object? content)
    {
        _isExit = false;

        if (_currentSide is not null)
            _sideStack.Push(_currentSide);

        _currentSide = content;
        await _host.NavigateSide(content);
    }

    public async Task NavigateMain(NavigationEntry entry)
    {
        _isExit = false;

        if (_currentMain is not null)
            _mainStack.Push(_currentMain);

        _currentMain = entry;

        await _host.ChangeTopBar(entry.TopBar);
        await _host.NavigateMain(entry.Content);
    }

    public async Task OpenPrevious()
    {
        if (_currentMain?.Content is IHandleBackNavigation backHandler)
        {
            var handled = await backHandler.HandleBackAsync();
            if (handled)
                return;
        }

        if (_mainStack.Count > 0)
        {
            var previous = _mainStack.Pop();
            _currentMain = previous;

            await _host.ChangeTopBar(previous.TopBar);
            await _host.NavigateMain(previous.Content);

            if (previous.Content is IHandleLastPage lastPage && _mainStack.Count == 0)
                await lastPage.HandleLastPageAsync();
        }
    }

    public void ClearStack()
    {
        _mainStack.Clear();

        if (FirstView is not null)
            _currentMain = FirstView;
    }

    public bool IsExit()
    {
        if (_isExit)
            return true;

        if (_mainStack.Count == 0)
        {
            _isExit = true;
            return false;
        }

        return false;
    }
}