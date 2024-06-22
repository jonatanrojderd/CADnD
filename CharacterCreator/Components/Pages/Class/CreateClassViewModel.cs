using CharacterCreator.Utilities;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components.Pages.Class;

public partial class CreateClassViewModel : ViewModelBase
{
    public override async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        await base.InitializeAsync(dataSerializer, navigationManager);
    }
}