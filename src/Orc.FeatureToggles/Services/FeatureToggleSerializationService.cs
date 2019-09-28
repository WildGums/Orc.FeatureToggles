namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Runtime.Serialization.Xml;
    using Orc.FileSystem;

    public class FeatureToggleSerializationService : IFeatureToggleSerializationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly IXmlSerializer _xmlSerializer;

        public FeatureToggleSerializationService(IDirectoryService directoryService, IFileService fileService,
            IXmlSerializer xmlSerializer)
        {
            Argument.IsNotNull(() => directoryService);
            Argument.IsNotNull(() => fileService);
            Argument.IsNotNull(() => xmlSerializer);

            _directoryService = directoryService;
            _fileService = fileService;
            _xmlSerializer = xmlSerializer;
        }

        protected virtual string GetFileName()
        {
            return Catel.IO.Path.GetApplicationDataDirectory();
        }

        public async Task<List<FeatureToggle>> LoadAsync()
        {
            var toggles = new List<FeatureToggle>();

            var fileName = GetFileName();

            Log.Debug($"Loading feature toggles from '{fileName}'");

            if (_fileService.Exists(fileName))
            {
                using (var stream = _fileService.OpenRead(fileName))
                {
                    var deserializedToggles = (List<FeatureToggle>)_xmlSerializer.Deserialize(typeof(List<FeatureToggle>), stream);
                    if (deserializedToggles != null)
                    {
                        toggles.AddRange(deserializedToggles);
                    }
                }
            }

            return toggles;
        }

        public async Task SaveAsync(List<FeatureToggle> featureToggles)
        {
            var fileName = GetFileName();

            Log.Debug($"Saving feature toggles to '{fileName}'");

            using (var stream = _fileService.Create(fileName))
            {
                _xmlSerializer.Serialize(featureToggles, stream);
            }
        }
    }
}
