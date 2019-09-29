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

        private readonly IFeatureToggleInitializationService _featureToggleInitializationService;
        private readonly IFeatureToggleSerializationService _featureToggleSerializationService;

        private readonly Dictionary<string, FeatureToggle> _featureToggles = new Dictionary<string, FeatureToggle>(StringComparer.OrdinalIgnoreCase);

        public FeatureToggleService(IFeatureToggleInitializationService featureToggleInitializationService,
            IFeatureToggleSerializationService featureToggleSerializationService)
        {
            Argument.IsNotNull(() => featureToggleInitializationService);
            Argument.IsNotNull(() => featureToggleSerializationService);

            _featureToggleInitializationService = featureToggleInitializationService;
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

        [Time]
        public async Task InitializeAsync()
        {
            Log.Debug("Initializing feature toggles");

            var toggles = await _featureToggleInitializationService.FindTogglesAsync();

            foreach (var toggle in toggles)
            {
                AddToggle(toggle);
            }
        }

        [Time]
        public async Task LoadAsync()
        {
            Log.Debug("Loading feature toggle values");

            var toggleValues = await _featureToggleSerializationService.LoadAsync();
            var count = 0;

            foreach (var toggleValue in toggleValues)
            {
                var toggle = GetToggle(toggleValue.Name);
                if (toggle != null)
                {
                    Log.Debug($"  * {toggle.Name} => {toggleValue.Value}");

                    toggle.Value = toggleValue.Value;
                    count++;
                }
            }

            Loaded?.Invoke(this, EventArgs.Empty);

            Log.Debug($"Loaded '{count}' feature toggle values");
        }

        [Time]
        public async Task SaveAsync()
        {
            Log.Debug("Saving feature toggle values");

            await _featureToggleSerializationService.SaveAsync(_featureToggles.Values.Select(x => new FeatureToggleValue(x)).ToList());

            Saved?.Invoke(this, EventArgs.Empty);

            Log.Debug($"Saved '{_featureToggles.Count}' feature toggle values");
        }

        private void Subscribe(FeatureToggle toggle)
        {
            toggle.Toggled += OnFeatureToggleToggled;
        }

        private void Unsubscribe(FeatureToggle toggle)
        {
            toggle.Toggled -= OnFeatureToggleToggled;
        }

        private async void OnFeatureToggleToggled(object sender, ToggledEventArgs e)
        {
            Log.Info($"Feature toggle '{e.Toggle}' was toggled from '{e.OldValue}' => '{e.NewValue}'");

            await SaveAsync();

            Toggled?.Invoke(this, e);
        }
    }
}
