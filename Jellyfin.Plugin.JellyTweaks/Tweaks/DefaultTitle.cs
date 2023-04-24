using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using Jellyfin.Plugin.JellyTweaks.Data;
using Jellyfin.Plugin.JellyTweaks.Utils;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Tweaks
{
    public class DefaultTitle : Tweak
    {
        private readonly ILogger<Tweak> _logger;

        private static readonly string _name = "DefaultTitle";
        private static readonly Collection<TweakFile> _files = new()
        {
            new TweakFile(Paths.MainJS!, new Collection<TweakSearching>()
            {
                new TweakSearching("document.title=e||\"", "\"}"),
                new TweakSearching("document.title=\"", "\"}")
            }),
            new TweakFile(Paths.IndexHTML!, new Collection<TweakSearching>()
            {
                new TweakSearching("<title>", "</title>"),
                new TweakSearching("<meta property=\"og:title\" content=\"", "\">"),
                new TweakSearching("<meta property=\"og:site_name\" content=\"", "\">"),
                new TweakSearching("<meta name=\"application-name\" content=\"", "\">")
            })
        };

        public DefaultTitle(ILogger<Tweak> logger) : base(_name, _files)
        {
            _logger = logger;
        }

        public override async Task Execute(PluginConfiguration configuration)
        {
            string value = configuration.DefaultTitle;
            await TweakUtils.ApplyTweakAsync(_logger, this, value).ConfigureAwait(false);
        }
    }
}
