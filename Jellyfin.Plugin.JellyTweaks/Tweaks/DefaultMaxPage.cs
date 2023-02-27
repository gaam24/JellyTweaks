using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using MediaBrowser.Model.Search;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks
{
    public class DefaultMaxPage : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static string name = "DefaultLibraryPageSize";
        private static Collection<Searching> searching = new Collection<Searching>()
        {
            new Searching(Paths.MainJS!, "this.get(\"libraryPageSize\",!1),10);return 0===t?0:t||", "}")
        };

        public DefaultMaxPage(ILogger<Tweak> logger) : base(name, searching)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            string value = configuration.DefaultLibraryPageSize.ToString(new CultureInfo("en-us"));
            await FileUtils.ChangeInLine(_logger, this, value).ConfigureAwait(false);
        }
    }
}
