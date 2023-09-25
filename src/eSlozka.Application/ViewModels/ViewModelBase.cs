using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eSlozka.Application.ViewModels;

public class ViewModelBase : IDisposable
{
    public delegate Task ViewModelAfterRenderHandler();

    public delegate Task ViewModelInitializedHandler();

    public delegate Task ViewModelParametersSetHandler();

    private bool busy;

    public bool Busy
    {
        get => busy;
        set => SetValue(ref busy, value);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event ViewModelInitializedHandler? ViewModelInitialized;
    public event ViewModelAfterRenderHandler? ViewModelAfterRender;
    public event ViewModelParametersSetHandler? ViewModelParametersSet;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = default!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetValue<TItem>(ref TItem field, TItem value, [CallerMemberName] string propertyName = default!)
    {
        if (EqualityComparer<TItem>.Default.Equals(field, value)) return;

        field = value;

        OnPropertyChanged(propertyName);
    }

    public Task OnViewModelInitialized()
    {
        return ViewModelInitialized?.Invoke() ?? Task.CompletedTask;
    }

    public Task OnViewModelAfterRender()
    {
        return ViewModelAfterRender?.Invoke() ?? Task.CompletedTask;
    }

    public Task OnViewModelParametersSet()
    {
        return ViewModelParametersSet?.Invoke() ?? Task.CompletedTask;
    }
}