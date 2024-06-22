using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public interface IViewModel : INotifyPropertyChanged
{
    Task InitializeAsync(NavigationManager navigationManager);
}

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    public virtual async Task InitializeAsync(NavigationManager navigationManager)
    {
    }
}