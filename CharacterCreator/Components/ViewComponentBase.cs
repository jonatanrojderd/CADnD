using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public class ViewComponentBase<TViewModel> : ComponentBase where TViewModel : IViewModel
{
    [Inject]
    [NotNull]
    protected TViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.PropertyChanged += OnPropertyChanged;
        await base.OnInitializedAsync();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }
}