namespace Orc.FeatureToggles
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Threading;
    using MethodTimer;

    public class FeatureToggleService : IFeatureToggleService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IFeatureToggleSerializationService _featureToggleSerializationService;

        private readonly Dictionary<string, FeatureToggle> _featureToggles = new Dictionary<string, FeatureToggle>(StringComparer.OrdinalIgnoreCase);

        public FeatureToggleService(IFeatureToggleSerializationService featureToggleSerializationService)
        {
            Argument.IsNotNull(() => featureToggleSerializationService);

            _featureToggleSerializationService = featureToggleSerializationService;
        }

        public event EventHandler<ToggleEventArgs> ToggleAdded;
        public event EventHandler<ToggleEventArgs> ToggleRemoved;

        public event EventHandler<EventArgs> Loaded;
        public event EventHandler<EventArgs> Saved;

        public event EventHandler<ToggledEventArgs> Toggled;

        public IEnumerable<FeatureToggle> GetToggles()
        {
            return _featureToggles.Values;
        }

        public FeatureToggle GetToggle(string name)
        {
            Argument.IsNotNull(() => name);

            if (!_featureToggles.TryGetValue(name, out var toggle))
            {
                Log.Warning($"Feature toggle '{name}' not found");
                return null;
            }

            return toggle;
        }

        public bool AddToggle(FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            Log.Debug($"Adding feature toggle '{toggle}'");

            if (_featureToggles.TryGetValue(toggle.Name, out var _))
            {
                Log.Warning($"Feature toggle '{toggle.Name}' is already registered");
                return false;
            }

            _featureToggles[toggle.Name] = toggle;

            Subscribe(toggle);

            ToggleAdded?.Invoke(this, new ToggleEventArgs(toggle));

            return true;
        }

        public bool RemoveToggle(FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            Log.Debug($"Removing feature toggle '{toggle}'");

            if (!_featureToggles.Remove(toggle.Name))
            {
                Log.Warning($"Feature toggle '{toggle.Name}' is not registered");
                return false;
            }

            Unsubscribe(toggle);

            ToggleRemoved?.Invoke(this, new ToggleEventArgs(toggle));

            return true;
        }

        public async Task LoadAsync()
        {
            Log.Debug("Loading feature toggles");

            foreach (var toggle in _featureToggles.Values)
            {
                Unsubscribe(toggle);
            }

            _featureToggles.Clear();

            var newToggles = await _featureToggleSerializationService.LoadAsync();

            foreach (var toggle in newToggles)
            {
                Subscribe(toggle);

                _featureToggles[toggle.Name] = toggle;
            }

            Loaded?.Invoke(this, EventArgs.Empty);

            Log.Debug($"Loaded '{_featureToggles.Count}' feature toggles");
        }

        public async Task SaveAsync()
        {
            Log.Debug("Saving feature toggles");

            await _featureToggleSerializationService.SaveAsync(_featureToggles.Values.ToList());

            Saved?.Invoke(this, EventArgs.Empty);

            Log.Debug($"Saved '{_featureToggles.Count}' feature toggles");
        }

        private void Subscribe(FeatureToggle toggle)
        {
            toggle.Toggled += OnFeatureToggleToggled;
        }

        private void Unsubscribe(FeatureToggle toggle)
        {
            toggle.Toggled -= OnFeatureToggleToggled;
        }

        private void OnFeatureToggleToggled(object sender, ToggledEventArgs e)
        {
            Log.Info($"Feature toggle '{e.Toggle}' was toggled from '{e.OldValue}' => '{e.NewValue}'");

            Toggled?.Invoke(this, e);
        }
    }
}
