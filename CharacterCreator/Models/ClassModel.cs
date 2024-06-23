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
public class ClassModel
{
    public string Type { get; set; } = "";
    public int HitDie { get; set; }
    public AbilityScore PrimeStat { get; set; }
}