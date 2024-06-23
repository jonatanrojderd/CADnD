using CharacterCreator.Models;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Class;

public partial class CreateClassViewModel : ViewModelBase
{
    private IDataSerializer _dataSerializer;
    private NavigationManager _navigationManager;

    [ObservableProperty]
    private ClassModel _class = new();

    [ObservableProperty]
    private IList<AbilityScore> _abilityScores = [];

    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        _dataSerializer = dataSerializer;
        _navigationManager = navigationManager;
        
        await base.InitializeAsync(dataSerializer, navigationManager);

        AbilityScores = Enum.GetValues<AbilityScore>();
    }

    [RelayCommand]
    private void AbilityScoreChanged(ChangeEventArgs args)
    {
        var abilityScore = Enum.Parse<AbilityScore>(args.Value.ToString());
        Class.PrimeStat = abilityScore;
    }

    [RelayCommand]
    private void HitDieChanged(ChangeEventArgs args)
    {
        var hitDie = int.Parse(args.Value.ToString());
        Class.HitDie = hitDie;
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationManager.NavigateTo("/");
    }

    [RelayCommand]
    private async Task Save()
    {
        var dataContainer = await _dataSerializer.DeserializeAsync();
        dataContainer.Classes.Add(Class);

        await _dataSerializer.SerializeAsync(dataContainer);
        _navigationManager.NavigateTo("/");
    }
}