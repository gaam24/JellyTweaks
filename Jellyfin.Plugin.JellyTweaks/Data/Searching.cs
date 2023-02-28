namespace Jellyfin.Plugin.JellyTweaks.Data
{
    public class Searching
    {
        public string Path { get; }

        public string Start { get; }

        public string End { get; }

        public Searching(string path, string start, string end)
        {
            Path = path;
            Start = start;
            End = end;
        }
    }
}
