using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;

namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class Tweak
    {
        public string Name { get; }

        public Collection<Searching> RangeList { get; }

        public Tweak(string name, Collection<Searching> rangeList)
        {
            Name = name;
            RangeList = rangeList;
        }

        public virtual async Task Execute(PluginConfiguration configuration) => throw new NotImplementedException();
    }
}
