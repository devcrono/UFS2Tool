// Copyright (c) 2026, SvenGDK
// Licensed under the BSD 2-Clause License. See LICENSE file for details.

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using UFS2Tool.GUI.Models;
using UFS2Tool.GUI.ViewModels;

namespace UFS2Tool.GUI.Views;

public partial class PS5QuickCreateView : UserControl
{
    public PS5QuickCreateView()
    {
        InitializeComponent();
    }

    private async void BrowseInputDirectory_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false,
        });

        if (folders.Count > 0 && DataContext is PS5QuickCreateViewModel vm)
        {
            vm.InputDirectory = folders[0].Path.LocalPath;
        }
    }

    private async void BrowseImagePath_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            DefaultExtension = "ffpkg",
            FileTypeChoices =
            [
                new FilePickerFileType("fflat package files") { Patterns = ["*.ffpkg"] },
                new FilePickerFileType("Image files") { Patterns = ["*.img"] },
                new FilePickerFileType("All files") { Patterns = ["*"] },
            ],
        });

        if (file != null && DataContext is PS5QuickCreateViewModel vm)
        {
            vm.ImagePath = file.Path.LocalPath;
        }
    }

    private async void BrowseBatchOutputDirectory_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false,
        });

        if (folders.Count > 0 && DataContext is PS5QuickCreateViewModel vm)
        {
            vm.BatchOutputDirectory = folders[0].Path.LocalPath;
        }
    }

    private async void BrowseBatchFolders_Click(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = true,
        });

        if (folders.Count > 0 && DataContext is PS5QuickCreateViewModel vm)
        {
            foreach (var folder in folders)
            {
                string localPath = folder.Path.LocalPath;

                // Skip folders already in the batch list
                if (vm.BatchItems.Any(b => string.Equals(b.InputDirectory, localPath, StringComparison.OrdinalIgnoreCase)))
                    continue;

                vm.BatchItems.Add(new PS5BatchItem
                {
                    InputDirectory = localPath,
                });
            }
        }
    }
}
