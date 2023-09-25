﻿using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace eSlozka.Application.ViewModels;

public class PageComponentBase<TViewModel> : ComponentBase, IDisposable where TViewModel : ViewModelBase
{
    [Inject] public required TViewModel Model { get; set; }

    public void Dispose()
    {
        Model.PropertyChanged -= OnModelPropertyChanged;
        Model.Dispose();
        GC.SuppressFinalize(this);
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
        return firstRender
            ? Model.OnViewModelAfterRender()
            : base.OnAfterRenderAsync(firstRender);
    }

    private async void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }
}
