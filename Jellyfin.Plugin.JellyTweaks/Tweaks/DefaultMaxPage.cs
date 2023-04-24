using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks
{
    public class DefaultMaxPage : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static readonly string _name = "DefaultLibraryPageSize";
        private static readonly Collection<TweakFile> _files = new()
        {
            new TweakFile(Paths.MainJS!, new Collection<TweakSearching>()
            {
                new TweakSearching("this.get(\"libraryPageSize\",!1),10);return 0===t?0:t||", "}")
            })
        };

        public DefaultMaxPage(ILogger<Tweak> logger) : base(_name, _files)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            string value = Math.Abs(configuration.DefaultLibraryPageSize).ToString(CultureInfo.InvariantCulture);
            await TweakUtils.ApplyTweakAsync(_logger, this, value).ConfigureAwait(false);
        }
    }
}
