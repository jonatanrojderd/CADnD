namespace CharacterCreator.Models;

[Flags]
public enum ClassType
{
    Bard = 1,
    Cleric = Bard << 1,
    Druid = Cleric << 1,
    Fighter = Druid << 1,
    Ranger = Fighter << 1,
    Thief = Ranger << 1,
    Wizard = Thief << 1,

    None = 0
}

[Serializable]
public class RaceModel
{
    public string Type { get; set; } = "";
    public IDictionary<AbilityScore, int> MinimumStats { get; set; }
    public IDictionary<AbilityScore, int> Modifiers { get; set; }
    public string RacialAbility { get; set; } = "";
    public IList<ClassModel> AllowedClasses { get; set; }
}