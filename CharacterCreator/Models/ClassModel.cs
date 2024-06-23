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
public class ClassModel
{
    public string Type { get; set; } = "";
    public int HitDie { get; set; }
    public AbilityScore PrimeStat { get; set; }
}