// Copyright (c) 2026, SvenGDK
// Licensed under the BSD 2-Clause License. See LICENSE file for details.

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UFS2Tool.GUI.ViewModels;
using UFS2Tool.GUI.Views;

namespace UFS2Tool.GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Note: Avalonia 12 no longer registers DataAnnotationsValidationPlugin by
            // default and BindingPlugins is internal, so the v11 workaround that removed
            // the duplicate validator is no longer needed (and is no longer possible).
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}