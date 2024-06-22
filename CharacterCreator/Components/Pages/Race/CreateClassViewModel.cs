using CharacterCreator.Utilities;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Race;

public partial class CreateRaceViewModel : ViewModelBase
{
    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        await base.InitializeAsync(dataSerializer, navigationManager);
    }
}