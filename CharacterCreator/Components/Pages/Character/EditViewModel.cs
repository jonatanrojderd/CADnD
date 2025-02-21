﻿using CharacterCreator.Models;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Character;

public partial class EditViewModel : ViewModelBase
{
    private IDataSerializer _dataSerializer;
    private NavigationManager _navigationManager;
    
    [ObservableProperty]
    private CharacterModel _character;

    public int CharacterIndex { get; set; }
    public bool EditMode { get; set; }

    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _dataSerializer = dataSerializer;
        await base.InitializeAsync(dataSerializer, navigationManager);

        Character = (await dataSerializer.DeserializeAsync()).Characters[CharacterIndex];
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationManager.NavigateTo("/");
    }
    
    [RelayCommand]
    private void SwitchMode()
    {
        EditMode = !EditMode;
    }
}