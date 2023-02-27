using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Jellyfin.Plugin.JellyTweaks.Data;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.JellyTweaks.Utils
{
    public static class FileUtils
    {
        public static async Task ChangeInLine(ILogger logger, Tweak tweak, string value)
        {
            bool isChanged = false;

            foreach (Searching range in tweak.RangeList)
            {
                string path = range.Path;
                if (File.Exists(path))
                {
                    string start = range.Start;
                    string end = range.End;

                    string orginalContent = File.ReadAllText(path);
                    string content = File.ReadAllText(path);

                    int startIndex = content.IndexOf(start, StringComparison.Ordinal);
                    int endIndex = content.IndexOf(end, startIndex, StringComparison.Ordinal);
                    string result = content.Substring(startIndex + start.Length, endIndex - startIndex - start.Length);

                    string orginal = start + result + end;
                    string changed = start + value + end;

                    content = content.Replace(orginal, changed, StringComparison.Ordinal);

                    if (!orginalContent.Equals(content, StringComparison.Ordinal))
                    {
                        try
                        {
                            await File.WriteAllTextAsync(path, content).ConfigureAwait(false);
                            isChanged = true;
                        }
                        catch (Exception e)
                        {
                            logger.LogError("Encountered exception while applying tweak {0}: {1}", tweak.Name, e);
                        }
                    }
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
