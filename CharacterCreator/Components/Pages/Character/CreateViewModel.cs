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
    private CharacterModel _character = new();

    [ObservableProperty]
    private IList<RaceModel> _races = [];

    [ObservableProperty]
    private IList<ClassModel> _classes = [];

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
        Character.Class = null!;
        
        var selectedRace = args.Value!.ToString();
        if (string.IsNullOrWhiteSpace(selectedRace))
        {
            Classes = [];
            Character.Race = null!;
            return;
        }

        var race = Races.FirstOrDefault(r => r.Type.Equals(selectedRace));
        if (race is null)
        {
            Classes = [];
            Character.Race = null!;
            return;
        }

        Character.Race = race;
        Classes = race.AllowedClasses;
    }

    [RelayCommand]
    private void SelectClass(ChangeEventArgs args)
    {
        var selectedClass = args.Value!.ToString();
        if (string.IsNullOrWhiteSpace(selectedClass))
        {
            Character.Class = null!;
            return;
        }

        var @class = Classes.FirstOrDefault(c => c.Type.Equals(selectedClass));
        if (@class is null)
        {
            Character.Class = null!;
            return;
        }

        Character.Class = @class;
    }

    [RelayCommand]
    private void RollDie(AbilityScore abilityScore)
    {
        var diceSum = Random.Shared.RollDice(3).Sum();
        switch (abilityScore)
        {
            case AbilityScore.Strength:
            {
                Character.Strength = diceSum;
                break;
            }
            case AbilityScore.Intelligence:
            {
                Character.Intelligence = diceSum;
                break;
            }
            case AbilityScore.Wisdom:
            {
                Character.Wisdom = diceSum;
                break;
            }
            case AbilityScore.Dexterity:
            {
                Character.Dexterity = diceSum;
                break;
            }
            case AbilityScore.Constitution:
            {
                Character.Constitution = diceSum;
                break;
            }
            case AbilityScore.Charisma:
            {
                Character.Charisma = diceSum;
                break;
            }
            case AbilityScore.None:
            default:
            {
                break;
            }
        }
    }

    [RelayCommand]
    private void RollHitPointsDie()
    {
        var diceSum = Random.Shared.RollDie(Character.Class.HitDie);
        var constitutionModifier = Character.Constitution switch
        {
            < 4 => -3,
            < 6 => -2,
            < 9 => -1,
            < 13 => 0,
            < 16 => 1,
            < 18 => 2,
            _ => 3
        };

        Character.HitPoints = int.Max(1, diceSum + constitutionModifier);
    }

    [RelayCommand]
    private void RollStartingGoldDie() => Character.Gold = Random.Shared.RollDice(3).Sum() * 10;
}