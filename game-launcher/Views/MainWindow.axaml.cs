using System;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Styling;
using game_launcher.ViewModels;

namespace game_launcher.Views;

public partial class MainWindow : Window
{
    
    private CancellationTokenSource? _cancelToken;
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void Header_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;
        this.BeginMoveDrag(e);
    }

    private async void Menu_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();
            if (DataContext is MainWindowViewModel vm)
            {
                vm.MenuCollapsed = !vm.MenuCollapsed;
                double endWidth = vm.MenuCollapsed ? 80 : 200; // 或 200
            
                await AnimateWidthAsync(Menu,Menu.Width, endWidth, _cancelToken.Token);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private async Task AnimateWidthAsync(StackPanel target, double from, double to, CancellationToken token,int duration = 200)
    {
        var animation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0), // 起始
                    Setters = { new Setter(StackPanel.WidthProperty, from) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1), // 结束
                    Setters = { new Setter(StackPanel.WidthProperty, to) }
                }
            }
        };
        await animation.RunAsync(target, token);
    }
}