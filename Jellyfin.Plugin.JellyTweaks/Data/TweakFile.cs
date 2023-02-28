using System.Collections.ObjectModel;

namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class TweakFile
    {
        public string Path { get; }

        public Collection<TweakSearching> SearchingValues { get; }

        public TweakFile(string path, Collection<TweakSearching> searchingValues)
        {
            Path = path;
            SearchingValues = searchingValues;
        }
    }
}
