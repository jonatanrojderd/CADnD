using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharacterCreator.Components.Pages;

public partial class CounterViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _counter;
    
    [RelayCommand]
    private void Count() => Counter++;
}