// Copyright (c) 2026, SvenGDK
// Licensed under the BSD 2-Clause License. See LICENSE file for details.

using CommunityToolkit.Mvvm.ComponentModel;
using UFS2Tool.GUI.Services;

namespace UFS2Tool.GUI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _selectedLanguage;

    public static string[] AvailableLanguages => LocalizationManager.AvailableLanguages;

    public SettingsViewModel()
    {
        _selectedLanguage = Loc.CurrentLanguage;
    }

    partial void OnSelectedLanguageChanged(string value)
    {
        Loc.SetLanguage(value);
    }
}
