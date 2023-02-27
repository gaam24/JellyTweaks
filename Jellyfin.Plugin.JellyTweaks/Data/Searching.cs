namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class Searching
    {
        public string Path { get; }

        public string Start { get; }

        public string End { get; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        public Searching(string path, string start, string end)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            Path = path;
            Start = start;
            End = end;
        }
    }
}
