namespace CharacterCreator.Components.Pages.Character;

public partial class EditView : ViewComponentBase<EditViewModel>
{
    protected override async Task OnInitializedAsync()
    {
        var parameters = GetQueryParameters(NavigationManager.Uri);

        var characterIndex = parameters[0].Split("=")[^1];
        var editMode = parameters[1].Split("=")[^1];

        ViewModel.CharacterIndex = int.Parse(characterIndex);
        ViewModel.EditMode = bool.Parse(editMode);

        await base.OnInitializedAsync();
    }

    // TODO: Look into why the regular Blazor routing doesn't work, ugly fix for now.
    private static string[] GetQueryParameters(string uri)
    {
        var querySplit = uri.Split("?", StringSplitOptions.RemoveEmptyEntries);

        var parameters = querySplit[^1].Split("&");
        return parameters;
    }
}