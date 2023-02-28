namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class TweakSearching
    {
        public string Start { get; }

        public string End { get; }

        public TweakSearching(string start, string end)
        {
            Start = start;
            End = end;
        }
    }
}
