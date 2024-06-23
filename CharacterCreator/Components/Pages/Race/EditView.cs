namespace CharacterCreator.Components.Pages.Race;

public partial class EditView : ViewComponentBase<EditRaceViewModel>
{
    protected override async Task OnInitializedAsync()
    {
        var parameters = GetQueryParameters(NavigationManager.Uri);

        var raceIndex = parameters[0].Split("=")[^1];
        ViewModel.RaceIndex = int.Parse(raceIndex);

        await base.OnInitializedAsync();
    }

    private static string[] GetQueryParameters(string uri)
    {
        var querySplit = uri.Split("?", StringSplitOptions.RemoveEmptyEntries);

        var parameters = querySplit[^1].Split("&");
        return parameters;
    }
}