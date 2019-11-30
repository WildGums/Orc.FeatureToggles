namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Runtime.Serialization.Xml;
    using Catel.Services;
    using Orc.FileSystem;

    public class FeatureToggleSerializationService : IFeatureToggleSerializationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly IXmlSerializer _xmlSerializer;
        private readonly IAppDataService _appDataService;

        public FeatureToggleSerializationService(IDirectoryService directoryService, IFileService fileService,
            IXmlSerializer xmlSerializer, IAppDataService appDataService)
        {
            Argument.IsNotNull(() => directoryService);
            Argument.IsNotNull(() => fileService);
            Argument.IsNotNull(() => xmlSerializer);
            Argument.IsNotNull(() => appDataService);

            _directoryService = directoryService;
            _fileService = fileService;
            _xmlSerializer = xmlSerializer;
            _appDataService = appDataService;
        }

        protected virtual string GetFileName()
        {
            return Path.Combine(_appDataService.GetApplicationDataDirectory(Catel.IO.ApplicationDataTarget.UserRoaming), "FeatureToggles.xml");
        }

        public async Task<List<FeatureToggleValue>> LoadAsync()
        {
            var toggles = new List<FeatureToggleValue>();

            var fileName = GetFileName();

            Log.Debug($"Loading feature toggle values from '{fileName}'");

            if (_fileService.Exists(fileName))
            {
                using (var stream = _fileService.OpenRead(fileName))
                {
                    var deserializedToggleValues = (List<FeatureToggleValue>)_xmlSerializer.Deserialize(typeof(List<FeatureToggleValue>), stream);
                    if (deserializedToggleValues != null)
                    {
                        toggles.AddRange(deserializedToggleValues);
                    }
                }
            }

            return toggles;
        }

        public async Task SaveAsync(List<FeatureToggleValue> toggleValues)
        {
            var fileName = GetFileName();

            Log.Debug($"Saving feature toggle values to '{fileName}'");

            var directory = Path.GetDirectoryName(fileName);
            _directoryService.Create(directory);

            using (var stream = _fileService.Create(fileName))
            {
                var togglesValuesToSerialize = (from toggleValue in toggleValues
                                                where toggleValue.Value.HasValue
                                                select toggleValue).ToList();

                _xmlSerializer.Serialize(togglesValuesToSerialize, stream);
            }
        }
    }
}
