using System.ComponentModel;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public interface IViewModel : INotifyPropertyChanged
{
    Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager);
}

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    protected IDataSerializer DataSerializer { get; set; }
    protected NavigationManager NavigationManager { get; set; }
    
    public virtual async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
        DataSerializer = dataSerializer;
        NavigationManager = navigationManager;
    }
    
    [RelayCommand]
    private void GoBack()
    {
        NavigationManager.NavigateTo("/");
    }
}