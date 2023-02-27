using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks.MainJS
{
    public class DefaultTitle : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static string name = "DefaultTitle";
        private static Collection<Searching> searching = new Collection<Searching>()
        {
            new Searching(Paths.MainJS!, "document.title=e||\"", "\"}"),
            new Searching(Paths.MainJS!, "document.title=\"", "\"}"),
            new Searching(Paths.IndexHTML!, "<title>", "</title>"),
            new Searching(Paths.IndexHTML!, "<meta property=\"og:title\" content=\"", "\">"),
            new Searching(Paths.IndexHTML!, "<meta property=\"og:site_name\" content=\"", "\">"),
            new Searching(Paths.IndexHTML!, "<meta name=\"application-name\" content=\"", "\">")
        };

        public DefaultTitle(ILogger<Tweak> logger) : base(name, searching)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            string value = configuration.DefaultTitle;
            await FileUtils.ChangeInLine(_logger, this, value).ConfigureAwait(false);
        }
    }
}
