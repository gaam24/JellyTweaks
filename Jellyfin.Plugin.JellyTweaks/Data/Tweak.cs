using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;

namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class Tweak
    {
        public string Name { get; }

        public Collection<TweakFile> Files { get; }

        public Tweak(string name, Collection<TweakFile> files)
        {
            Name = name;
            Files = files;
        }

        public virtual async Task Execute(PluginConfiguration configuration) => throw new NotImplementedException();
    }
}
