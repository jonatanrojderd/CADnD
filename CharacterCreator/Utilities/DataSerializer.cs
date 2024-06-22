using System.Text.Json;
using CharacterCreator.Models;

namespace CharacterCreator.Utilities;

public interface IDataSerializer
{
    Task InitializeDataAsync();
    Task SerializeAsync(IDataContainer dataContainer);
    Task<IDataContainer> DeserializeAsync();
}

public class DataSerializer : IDataSerializer
{
    private const string Filename = "dnd_character_data.json";
    
    private readonly string _appDataDirectory = FileSystem.AppDataDirectory;
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private IDataContainer _dataContainer;

    public DataSerializer(IDataContainer dataContainer)
    {
        _dataContainer = dataContainer;
    }

    public async Task InitializeDataAsync()
    {
        try
        {
            if (File.Exists($"{_appDataDirectory}/{Filename}"))
            {
                await DeserializeAsync();
            }
            else
            {
                await using var fileStream = await FileSystem.OpenAppPackageFileAsync(Filename);
                var dataContainer =
                    await JsonSerializer.DeserializeAsync<DataContainer>(fileStream, _serializerOptions);

                await using var file = File.Create($"{_appDataDirectory}/{Filename}");
                await JsonSerializer.SerializeAsync(file, dataContainer, _serializerOptions);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SerializeAsync(IDataContainer dataContainer)
    {
        try
        {
            await using var file = File.Create($"{_appDataDirectory}/{Filename}");
            await JsonSerializer.SerializeAsync(file, dataContainer, _serializerOptions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IDataContainer> DeserializeAsync()
    {
        try
        {
            await using var file = File.OpenRead($"{_appDataDirectory}/{Filename}");
            var dataContainer = await JsonSerializer.DeserializeAsync<DataContainer>(file, _serializerOptions);

            _dataContainer.Characters = dataContainer.Characters;
            _dataContainer.Classes = dataContainer.Classes;
            _dataContainer.Races = dataContainer.Races;

            return _dataContainer;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public interface IDataContainer
{
    IList<CharacterModel> Characters { get; set; }
    IList<RaceModel> Races { get; set; }
    IList<ClassModel> Classes { get; set; }
}

[Serializable]
public class DataContainer : IDataContainer
{
    public IList<CharacterModel> Characters { get; set; } = new List<CharacterModel>();
    public IList<RaceModel> Races { get; set; } = new List<RaceModel>();
    public IList<ClassModel> Classes { get; set; } = new List<ClassModel>();
}