using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Tweaks;
using MediaBrowser.Model.Globalization;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.ScheduledTasks
{
    public class ApplyTweaks : IScheduledTask
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILocalizationManager _localization;

        public ApplyTweaks(
            ILoggerFactory loggerFactory,
            ILocalizationManager localization)
        {
            _loggerFactory = loggerFactory;
            _localization = localization;
        }

        /// <inheritdoc />
        public string Name => "Apply Tweaks";

        /// <inheritdoc />
        public string Key => "ApplyTweaks";

        /// <inheritdoc />
        public string Description => "Apply tweaks enabled in settings.";

        /// <inheritdoc />
        public string Category => _localization.GetLocalizedString("TasksLibraryCategory");

        public async Task ExecuteAsync(IProgress<double> progress, CancellationToken cancellationToken)
        {
            ILogger<Tweak> logger = _loggerFactory.CreateLogger<Tweak>();
            PluginConfiguration config = JellyTweaks.Instance!.Configuration;

            await new BackdropsByDefault(logger).Execute(config).ConfigureAwait(false);
            await new DefaultMaxPage(logger).Execute(config).ConfigureAwait(false);
            await new DefaultTitle(logger).Execute(config).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
        }

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
        {
            return new[]
            {
                new TaskTriggerInfo
                {
                    Type = TaskTriggerInfo.TriggerStartup
                }
            };
        }
    }
}
