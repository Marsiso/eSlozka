﻿using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace eSlozka.Application.ViewModels;

public class PageComponentBase<TViewModel> : ComponentBase where TViewModel : ViewModelBase
{
    [Inject] public required TViewModel Model { get; set; }

    public void Deconstruct()
    {
        Model.PropertyChanged -= OnModelPropertyChanged;
    }

    protected override bool ShouldRender()
    {
        return !Model.Busy;
    }

    protected override Task OnInitializedAsync()
    {
        Model.PropertyChanged += OnModelPropertyChanged;

        return Model.OnViewModelInitialized();
    }

    protected override Task OnParametersSetAsync()
    {
        return Model.OnViewModelParametersSet();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) return Model.OnViewModelAfterRender();

        return base.OnAfterRenderAsync(firstRender);
    }

    private async void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }
}
