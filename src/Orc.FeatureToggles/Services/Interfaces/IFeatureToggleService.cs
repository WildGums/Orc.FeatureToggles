﻿namespace Orc.FeatureToggles;

using System;
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
    FeatureToggle[] GetToggles();
    Task LoadAsync();
    bool RemoveToggle(FeatureToggle toggle);
    Task SaveAsync();
    Task InitializeAsync();
}
