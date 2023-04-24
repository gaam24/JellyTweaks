using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks
{
    public class EnableBackdropsByDefault : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static readonly string _name = "EnableBackdropsByDefault";
        private static readonly Collection<TweakFile> _files = new()
        {
            new TweakFile(Paths.MainJS!, new Collection<TweakSearching>() {
                new TweakSearching("enableBackdrops:function(){return ", "}")
            })
        };

        public EnableBackdropsByDefault(ILogger<Tweak> logger) : base(_name, _files)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            var value = configuration.EnableBackdropsByDefault ? "_" : "P";
            await TweakUtils.ApplyTweakAsync(_logger, this, value).ConfigureAwait(false);
        }
    }
}
