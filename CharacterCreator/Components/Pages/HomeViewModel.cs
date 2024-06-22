using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages;

public partial class HomeViewModel : ViewModelBase
{
    private IDataSerializer _dataSerializer;

    [ObservableProperty]
    private IList<string> _createdCharacters = [];

    [ObservableProperty]
    private IList<string> _races = [];

    [ObservableProperty]
    private IList<string> _classes = [];

    public override async Task InitializeAsync(NavigationManager navigationManager)
    {
        await base.InitializeAsync(navigationManager);
        
        _dataSerializer = Application.Current.Handler.MauiContext.Services.GetService<IDataSerializer>();
        var data = await _dataSerializer!.DeserializeAsync();

        CreatedCharacters = data.Characters.Select(character => character.Name).ToList();
        
        Races = data.Races.Select(race => race.Type).ToList();

        Classes = data.Classes.Select(c => c.Type).ToList();
    }
}