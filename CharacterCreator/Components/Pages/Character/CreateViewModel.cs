using CharacterCreator.Models;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Character;

public partial class CreateViewModel : ViewModelBase
{
    private IDataSerializer _dataSerializer;

    [ObservableProperty]
    private IList<RaceModel>? _races;

    [ObservableProperty]
    private IList<ClassModel>? _classes;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _dataSerializer = Application.Current.Handler.MauiContext.Services.GetService<IDataSerializer>();
        var data = await _dataSerializer!.DeserializeAsync();

        Races = data.Races;
        Classes = [];
    }

    [RelayCommand]
    private void SelectRace(ChangeEventArgs args)
    {
        var selectedRace = args.Value!.ToString();
        if (string.IsNullOrWhiteSpace(selectedRace))
        {
            // TODO: Unselect it in the model.
            
            Classes = [];
            return;
        }

        var race = Races!.FirstOrDefault(r => r.Type.Equals(selectedRace));

        Classes = race.AllowedClasses;
    }

    [RelayCommand]
    private void SelectClass(ChangeEventArgs args)
    {
        var selectedClass = args.Value!.ToString();
        if (string.IsNullOrWhiteSpace(selectedClass))
        {
            // TODO: Unselect it in the model.
            return;
        }

        var @class = Classes.FirstOrDefault(c => c.Type.Equals(selectedClass));
    }
}