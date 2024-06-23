namespace CharacterCreator.Components.Pages.Class;

public partial class EditView : ViewComponentBase<EditClassViewModel>
{
    protected override async Task OnInitializedAsync()
    {
        var parameters = GetQueryParameters(NavigationManager.Uri);

        var classIndex = parameters[0].Split("=")[^1];
        ViewModel.ClassIndex = int.Parse(classIndex);

        await base.OnInitializedAsync();
    }

    private static string[] GetQueryParameters(string uri)
    {
        var querySplit = uri.Split("?", StringSplitOptions.RemoveEmptyEntries);

        var parameters = querySplit[^1].Split("&");
        return parameters;
    }
}