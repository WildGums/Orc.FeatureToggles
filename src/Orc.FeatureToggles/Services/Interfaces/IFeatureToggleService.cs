namespace Orc.FeatureToggles;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeatureToggleService
{
    event EventHandler<ToggledEventArgs>? Toggled;
    event EventHandler<ToggleEventArgs>? ToggleAdded;
    event EventHandler<ToggleEventArgs>? ToggleRemoved;
    event EventHandler<EventArgs>? Loaded;
    event EventHandler<EventArgs>? Saved;

    bool AddToggle(FeatureToggle toggle);
    FeatureToggle? GetToggle(string name);
    IReadOnlyList<FeatureToggle> GetToggles();
    Task LoadAsync();
    bool RemoveToggle(FeatureToggle toggle);
    Task SaveAsync();
    Task InitializeAsync();
}
