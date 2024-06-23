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