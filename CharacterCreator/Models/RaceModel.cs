namespace CharacterCreator.Models;

public enum RaceType
{
    Dwarf,
    Elf,
    Gnome,
    Halfling,
    Human,

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