using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.JellyTweaks.Configuration;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        EnableBackdropsByDefault = false;
        DefaultLibraryPageSize = 100;
        DefaultTitle = "Jellyfin";
    }

    /// <summary>
    /// Gets or sets a value of default title.
    /// </summary>
    public string DefaultTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether backdrops is enabled by default.
    /// </summary>
    public bool EnableBackdropsByDefault { get; set; }

    /// <summary>
    /// Gets or sets a value of default page size.
    /// </summary>
    public int DefaultLibraryPageSize { get; set; }
}
