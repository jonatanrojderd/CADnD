using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages;

public partial class HomeViewModel : ViewModelBase
{
    private IDataSerializer _dataSerializer;
    private NavigationManager _navigationManager;

    [ObservableProperty]
    private IList<string> _createdCharacters = [];

    [ObservableProperty]
    private IList<string> _races = [];

    [ObservableProperty]
    private IList<string> _classes = [];

    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        await base.InitializeAsync(dataSerializer, navigationManager);

        _dataSerializer = dataSerializer;
        var data = await _dataSerializer.DeserializeAsync();

        CreatedCharacters = data.Characters.Select(character => character.Name).ToList();
        Races = data.Races.Select(race => race.Type).ToList();
        Classes = data.Classes.Select(c => c.Type).ToList();
    }

    [RelayCommand]
    private void ViewCharacter(int index)
    {
        _navigationManager.NavigateTo($"/edit-character?index={index}&edit={false}");
    }
    
    [RelayCommand]
    private void EditRace(int index){}
    
    [RelayCommand]
    private void EditClass(int index){}
}