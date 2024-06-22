using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CharacterCreator.Utilities;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public class ViewComponentBase<TViewModel> : ComponentBase where TViewModel : IViewModel
{
    [Inject, NotNull]
    protected TViewModel? ViewModel { get; set; }
    
    [Inject, NotNull]
    public IDataSerializer? DataSerializer { get; set; }
    
    [Inject, NotNull]
    public NavigationManager? NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ViewModel.PropertyChanged += OnPropertyChanged;
        
        await base.OnInitializedAsync();
        
        await ViewModel.InitializeAsync(DataSerializer, NavigationManager);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }
}