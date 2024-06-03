using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Tweaks;
using MediaBrowser.Model.Globalization;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.ScheduledTasks;

public class ApplyTweaks(ILoggerFactory loggerFactory, ILocalizationManager localization) : IScheduledTask
{
    /// <inheritdoc />
    public string Name => "Apply Tweaks";

    /// <inheritdoc />
    public string Key => "ApplyTweaks";

    /// <inheritdoc />
    public string Description => "Apply tweaks enabled in settings.";

    /// <inheritdoc />
    public string Category => localization.GetLocalizedString("TasksLibraryCategory");

    public async Task ExecuteAsync(IProgress<double> progress, CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger<Tweak>();
        var config = JellyTweaks.Instance!.Configuration;

        if (config == null)
        {
            throw new InvalidOperationException("Configuration cannot be null");
        }

        var tasks = new List<Task>
        {
            new EnableBackdropsByDefault(logger).Execute(config),
            new DefaultMaxPage(logger).Execute(config),
            new DefaultTitle(logger).Execute(config)
        };

        await Task.WhenAll(tasks).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
    }

    public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
    {
        return
        [
            new TaskTriggerInfo { Type = TaskTriggerInfo.TriggerStartup }
        ];
    }
}
