using System.ComponentModel;
using CharacterCreator.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public interface IViewModel : INotifyPropertyChanged
{
    Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager);
}

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    public virtual async Task InitializeAsync(IDataSerializer dataSerializer, NavigationManager navigationManager)
    {
    }
}