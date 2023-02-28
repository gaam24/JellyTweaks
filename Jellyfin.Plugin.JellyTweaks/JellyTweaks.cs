using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Jellyfin.Plugin.JellyTweaks.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks;

public class JellyTweaks : BasePlugin<PluginConfiguration>, IHasWebPages
{
    /// <inheritdoc />
    public override string Name => "JellyTweaks";

    /// <inheritdoc />
    public override Guid Id => Guid.Parse("dfee3828-01df-49df-85b1-5c2b75e5ea1a");

    /// <summary>
    /// Initializes a new instance of the <see cref="JellyTweaks"/> class.
    /// </summary>
    /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
    /// <param name="logger">Instance of the <see cref="ILogger"/> interface.</param>
    /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
    public JellyTweaks(IApplicationPaths applicationPaths, ILogger<JellyTweaks> logger, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
    {
        Instance = this;

        if (!string.IsNullOrWhiteSpace(applicationPaths.WebPath))
        {
            Paths.IndexHTML = Path.Combine(applicationPaths.WebPath, "index.html");
            Paths.MainJS = Path.Combine(applicationPaths.WebPath, "main.jellyfin.bundle.js");
        }
    }

    /// <summary>
    /// Gets the current plugin instance.
    /// </summary>
    public static JellyTweaks? Instance { get; private set; }

    /// <inheritdoc />
    public IEnumerable<PluginPageInfo> GetPages()
    {
        return new[]
        {
            new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html", GetType().Namespace)
            }
        };
    }
}
