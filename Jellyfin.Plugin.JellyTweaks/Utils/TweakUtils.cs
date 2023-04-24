using System;
using System.IO;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Data;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Utils
{
    public static class TweakUtils
    {
        public static async Task ApplyTweakAsync(ILogger logger, Tweak tweak, string value)
        {
            bool isChanged = false;

            foreach (TweakFile file in tweak.Files)
            {
                string path = file.Path;
                if (File.Exists(path))
                {
                    string originalContent = await File.ReadAllTextAsync(path);
                    string content = await File.ReadAllTextAsync(path);

                    // Find and change values
                    foreach (TweakSearching values in file.SearchingValues)
                    {
                        string start = values.Start;
                        string end = values.End;

                        // Try to find values
                        try
                        {
                            int startIndex = content.IndexOf(start, StringComparison.Ordinal);
                            int endIndex = content.IndexOf(end, startIndex, StringComparison.Ordinal);
                            string result = content.Substring(startIndex + start.Length, endIndex - startIndex - start.Length);

                            string original = $"{start}{result}{end}";
                            string changed = $"{start}{value}{end}";

                            content = content.Replace(original, changed, StringComparison.Ordinal);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            logger.LogError($"Cannot find values '{start}xxx{end}'! | Error: {ex.Message}");
                        }
                    }

                    // If there's any changes, trying to save them to file
                    if (!originalContent.Equals(content, StringComparison.Ordinal))
                    {
                        try
                        {
                            await File.WriteAllTextAsync(path, content).ConfigureAwait(false);
                            isChanged = true;
                        }
                        catch (Exception ex)
                        {
                            logger.LogError($"Encountered exception while applying tweak {tweak.Name}: {ex}");
                            return;
                        }
                    }
                }
                else
                {
                    logger.LogError($"Cannot find path {path} for apply {tweak.Name}!");
                    return;
                }
            }

            if (isChanged)
            {
                logger.LogInformation($"Finished applying tweak: {tweak.Name}!");
            }
            else
            {
                logger.LogInformation($"Tweak {tweak.Name} no need to apply!");
            }
        }
    }
}
