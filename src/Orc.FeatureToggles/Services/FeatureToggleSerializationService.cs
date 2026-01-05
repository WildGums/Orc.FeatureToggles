namespace Orc.FeatureToggles;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catel;
using Catel.Logging;
using Catel.Services;
using FileSystem;
using Microsoft.Extensions.Logging;
using Orc.Serialization.Json;

public class FeatureToggleSerializationService : IFeatureToggleSerializationService
{
    private readonly ILogger<FeatureToggleSerializationService> _logger;
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    private readonly IAppDataService _appDataService;
    private readonly IJsonSerializerFactory _jsonSerializerFactory;

    public FeatureToggleSerializationService(ILogger<FeatureToggleSerializationService> logger,
        IDirectoryService directoryService, IFileService fileService, IAppDataService appDataService,
        IJsonSerializerFactory jsonSerializerFactory)
    {
        _logger = logger;
        _directoryService = directoryService;
        _fileService = fileService;
        _appDataService = appDataService;
        _jsonSerializerFactory = jsonSerializerFactory;
    }

    protected virtual string GetFileName()
    {
        return Path.Combine(_appDataService.GetApplicationDataDirectory(Catel.IO.ApplicationDataTarget.UserRoaming), "FeatureToggles.json");
    }

    public async Task<IReadOnlyList<FeatureToggleValue>> LoadAsync()
    {
        var toggles = new List<FeatureToggleValue>();

        var fileName = GetFileName();

        _logger.LogDebug($"Loading feature toggle values from '{fileName}'");

        if (_fileService.Exists(fileName))
        {
            using (var stream = _fileService.OpenRead(fileName))
            {
                var serializer = _jsonSerializerFactory.CreateSerializer();

                var deserializedToggleValues = serializer.Deserialize<List<FeatureToggleValue>>(stream);
                if (deserializedToggleValues is not null)
                {
                    toggles.AddRange(deserializedToggleValues);
                }
            }
        }

        return toggles.ToArray();
    }

    public async Task SaveAsync(IReadOnlyList<FeatureToggleValue> toggleValues)
    {
        var fileName = GetFileName();

        _logger.LogDebug($"Saving feature toggle values to '{fileName}'");

        var directory = Path.GetDirectoryName(fileName);
        if (directory is null)
        {
            throw _logger.LogErrorAndCreateException<InvalidOperationException>($"Invalid file name '{fileName}'");
        }

        _directoryService.Create(directory);

        using (var stream = _fileService.Create(fileName))
        {
            var togglesValuesToSerialize = (from toggleValue in toggleValues
                                            where toggleValue.Value.HasValue
                                            select toggleValue).ToList();

            var serializer = _jsonSerializerFactory.CreateSerializer();

            serializer.Serialize(stream, togglesValuesToSerialize);
        }
    }
}
