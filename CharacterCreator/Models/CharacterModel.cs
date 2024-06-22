namespace CharacterCreator.Models;

[Flags]
public enum AbilityScore
{
    Strength = 1,
    Intelligence = Strength << 1,
    Wisdom = Intelligence << 1,
    Dexterity = Wisdom << 1,
    Constitution = Dexterity << 1,
    Charisma = Constitution << 1,

    None = 0
}

public enum RaceType
{
    Dwarf,
    Elf,
    Gnome,
    Halfling,
    Human,

    None = 0
}

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
public class CharacterModel
{
    public string Name { get; set; } = "";

    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Charisma { get; set; }

    public RaceModel Race { get; set; } = null!;
    public ClassModel Class { get; set; } = null!;

    public int HitPoints { get; set; }

    public int Gold { get; set; }
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

[Serializable]
public class ClassModel
{
    public string Type { get; set; } = "";
    public int HitDie { get; set; }
    public AbilityScore PrimeStat { get; set; }
}