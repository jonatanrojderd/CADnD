using CharacterCreator.Models;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Character;

public partial class CreateViewModel : ViewModelBase
{
    private IDataContainer _dataContainer;

    [ObservableProperty]
    private CharacterModel _character = new();

    [ObservableProperty]
    private IList<RaceModel> _races = [];

    [ObservableProperty]
    private IList<ClassModel> _classes = [];

    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        await base.InitializeAsync(dataSerializer, navigationManager);
        
        _dataContainer = await DataSerializer.DeserializeAsync();

        Races = _dataContainer.Races;
        Classes = [];
    }

    public int GetAbilityScoreSum(AbilityScore abilityScore)
    {
        if (Character.Race is null)
            return 0;

        if (Character.Race.Modifiers.TryGetValue(abilityScore, out var value))
        {
            return abilityScore switch
            {
                AbilityScore.Strength => Character.Strength + value,
                AbilityScore.Intelligence => Character.Intelligence + value,
                AbilityScore.Wisdom => Character.Wisdom + value,
                AbilityScore.Dexterity => Character.Dexterity + value,
                AbilityScore.Constitution => Character.Constitution + value,
                AbilityScore.Charisma => Character.Charisma + value,
                AbilityScore.None => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(abilityScore), abilityScore, null)
            };
        }

        return abilityScore switch
        {
            AbilityScore.Strength => Character.Strength,
            AbilityScore.Intelligence => Character.Intelligence,
            AbilityScore.Wisdom => Character.Wisdom,
            AbilityScore.Dexterity => Character.Dexterity,
            AbilityScore.Constitution => Character.Constitution,
            AbilityScore.Charisma => Character.Charisma,
            AbilityScore.None => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(abilityScore), abilityScore, null)
        };
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
    
    [RelayCommand]
    private async Task Save()
    {
        if (!Verify())
            return;

        _dataContainer.Characters.Add(Character);
        await DataSerializer.SerializeAsync(_dataContainer);
        
        NavigationManager.NavigateTo("/");
    }

    private bool Verify()
    {
        if (string.IsNullOrWhiteSpace(Character.Name))
            return false;

        var characterRace = Character.Race;
        if (characterRace is not null && characterRace.MinimumStats.Count > 0)
        {
            var meetsRequirement = false;
            foreach (var (abilityScore, value) in characterRace.MinimumStats)
            {
                switch (abilityScore)
                {
                    case AbilityScore.Strength:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Strength) >= value;
                        break;
                    }
                    case AbilityScore.Intelligence:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Intelligence) >= value;
                        break;
                    }
                    case AbilityScore.Wisdom:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Wisdom) >= value;
                        break;
                    }
                    case AbilityScore.Dexterity:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Dexterity) >= value;
                        break;
                    }
                    case AbilityScore.Constitution:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Constitution) >= value;
                        break;
                    }
                    case AbilityScore.Charisma:
                    {
                        meetsRequirement = GetAbilityScoreSum(AbilityScore.Charisma) >= value;
                        break;
                    }
                    case AbilityScore.None:
                    default:
                    {
                        break;
                    }
                }

                if (!meetsRequirement)
                    return false;
            }
        }

        return true;
    }
}