using CharacterCreator.Components.Pages;
using CharacterCreator.Components.Pages.Character;
using CharacterCreator.Components.Pages.Class;
using CharacterCreator.Components.Pages.Race;
using CharacterCreator.Utilities;
using Microsoft.Extensions.Logging;

namespace CharacterCreator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IDataSerializer, DataSerializer>();
        builder.Services.AddSingleton<IDataContainer, DataContainer>();
        
        builder.Services.AddSingleton<HomeViewModel>();
        
        builder.Services.AddTransient<CreateCharacterViewModel>();
        builder.Services.AddTransient<EditCharacterViewModel>();
        
        builder.Services.AddTransient<CreateRaceViewModel>();
        builder.Services.AddTransient<EditRaceViewModel>();
        
        builder.Services.AddTransient<CreateClassViewModel>();
        builder.Services.AddTransient<EditClassViewModel>();

        return builder.Build();
    }
}