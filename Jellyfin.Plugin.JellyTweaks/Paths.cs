using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks
{
    public static class Paths
    {
        /// <summary>
        /// Gets or sets 'index.html' path.
        /// </summary>
        public static string? IndexHTML { get; set; }

        /// <summary>
        /// Gets or sets 'main.jellyfin.bundle.js' path.
        /// </summary>
        public static string? MainJS { get; set; }
    }
}
