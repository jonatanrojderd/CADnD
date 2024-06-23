using CharacterCreator.Models;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Race;

public partial class EditRaceViewModel : ViewModelBase
{
    [ObservableProperty]
    private RaceModel _race = new();

    [ObservableProperty]
    private IList<ClassModel> _classes = [];

    [ObservableProperty]
    private IList<AbilityScore> _abilityScores = [];

    public int RaceIndex { get; set; }

    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        await base.InitializeAsync(dataSerializer, navigationManager);

        var dataContainer = await DataSerializer.DeserializeAsync();
        
        Race = dataContainer.Races[RaceIndex];
        Classes = dataContainer.Classes;

        AbilityScores = Enum.GetValues<AbilityScore>()
            .Where(score => score != AbilityScore.None)
            .ToList();
    }

    [RelayCommand]
    private void AllowedClassChanged(ClassModel selection)
    {
        if (Race.AllowedClasses.Remove(selection))
            return;

        Race.AllowedClasses.Add(selection);
    }

    [RelayCommand]
    private void MinimumStatChanged(Tuple<AbilityScore, ChangeEventArgs> tuple)
    {
        tuple.Deconstruct(out var abilityScore, out var args);

        var value = int.Parse(args.Value.ToString());
        if (Race.MinimumStats.TryAdd(abilityScore, value))
            return;

        if (value == 0)
            Race.MinimumStats.Remove(abilityScore);
        else
            Race.MinimumStats[abilityScore] = value;
    }

    [RelayCommand]
    private void ModifierChanged(Tuple<AbilityScore, ChangeEventArgs> tuple)
    {
        tuple.Deconstruct(out var abilityScore, out var args);

        var value = int.Parse(args.Value.ToString());
        if (Race.Modifiers.TryAdd(abilityScore, value))
            return;

        if (value == 0)
            Race.Modifiers.Remove(abilityScore);
        else
            Race.Modifiers[abilityScore] = value;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        Race.MinimumStats = Race.MinimumStats.Where(stat => stat.Value != 0).ToDictionary();
        Race.Modifiers = Race.Modifiers.Where(modifier => modifier.Value != 0).ToDictionary();

        var dataContainer = await DataSerializer.DeserializeAsync();
        
        var races = dataContainer.Races;
        races[RaceIndex] = Race;
        
        await DataSerializer.SerializeAsync(dataContainer);
        NavigationManager.NavigateTo("/");
    }
}