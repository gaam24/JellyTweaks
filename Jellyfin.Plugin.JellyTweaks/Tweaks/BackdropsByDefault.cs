using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks
{
    public class BackdropsByDefault : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static string name = "EnableBackdropsByDefault";
        private static Collection<Searching> searching = new Collection<Searching>()
        {
            new Searching(Paths.MainJS!, "enableBackdrops:function(){return ", "}")
        };

        public BackdropsByDefault(ILogger<Tweak> logger) : base(name, searching)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            var value = configuration.EnableBackdropsByDefault ? "_" : "P";
            await FileUtils.ChangeInLine(_logger, this, value).ConfigureAwait(false);
        }
    }
}
