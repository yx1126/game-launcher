using ReactiveUI;

namespace game_launcher.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private bool _menuCollapsed = false;

    public bool MenuCollapsed
    {
        get => _menuCollapsed;
        set => this.RaiseAndSetIfChanged(ref _menuCollapsed, value);
    }
    
}