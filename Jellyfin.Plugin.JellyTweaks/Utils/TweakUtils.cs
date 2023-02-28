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

                            string original = start + result + end;
                            string changed = start + value + end;

                            content = content.Replace(original, changed, StringComparison.Ordinal);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            logger.LogError("Cannot find values '{0}xxx{1}'! | Error: {2}", start, end, ex.Message);
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
                            logger.LogError("Encountered exception while applying tweak {0}: {1}", tweak.Name, ex);
                            return;
                        }
                    }
                }
                else
                {
                    logger.LogError("Canno't find path {0} for apply {0}!", path, tweak.Name);
                    return;
                }
            }

            if (isChanged)
            {
                logger.LogInformation("Finished applying tweak: {0}!", tweak.Name);
            }
            else
            {
                logger.LogInformation("Tweak {0} no need to apply!", tweak.Name);
            }
        }
    }
}
