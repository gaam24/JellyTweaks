using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Configuration;

namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class Tweak
    {
        public string Name { get; }

        public Collection<Searching> RangeList { get; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        public Tweak(string name, Collection<Searching> rangeList)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Name = name;
            RangeList = rangeList;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task Execute(PluginConfiguration configuration) => throw new NotImplementedException();
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
