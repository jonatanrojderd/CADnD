using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CharacterCreator.Components;

public interface IViewModel : INotifyPropertyChanged
{
    Task InitializeAsync();
}

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    public virtual async Task InitializeAsync()
    {
    }
}