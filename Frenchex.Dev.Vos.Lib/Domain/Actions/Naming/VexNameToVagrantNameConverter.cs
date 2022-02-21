using System.Text.RegularExpressions;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;

public interface IVexNameToVagrantNameConverter
{
    string[] ConvertAll(string[] names, string? workingDirectory, Domain.Configuration.Configuration configuration);
}

public class VexNameToVagrantNameConverter : IVexNameToVagrantNameConverter
{
    private static readonly Regex PatternFromToWildcard =
        new("(?<machine>.*)\\-\\[(?<instance>.*)\\]", RegexOptions.Compiled);

    private static readonly Regex PatternFromTo = new("(?<machine>.*)\\-(?<instance>.*)", RegexOptions.Compiled);

    public string[] ConvertAll(
        string[] inputNamedPatterns,
        string? workingDirectory,
        Domain.Configuration.Configuration configuration
    )
    {
        var list = new List<string>();
        var dirBase = Path.GetFileName(workingDirectory);

        if (null == dirBase)
            throw new ArgumentNullException(nameof(dirBase));

        var prefixWithDirBase = configuration.Vagrant.PrefixWithDirBase;
        var namingPattern = configuration.Vagrant.NamingPattern;

        foreach (var name in inputNamedPatterns)
        {
            var matchesPatternFromToWildcard = PatternFromToWildcard.Matches(name);
            var from = 0;
            var to = 0;
            var machineName = "";
            var instances = "";

            switch (matchesPatternFromToWildcard.Count)
            {
                case 0:
                {
                    var matchesPatternFromTo = PatternFromTo.Matches(name);

                    var firstMatches = matchesPatternFromTo.First();

                    if (firstMatches.Groups.ContainsKey("machine"))
                        machineName = firstMatches.Groups["machine"].Value;

                    if (firstMatches.Groups.ContainsKey("instance"))
                        instances = firstMatches.Groups["instance"].Value;

                    if (instances == "*")
                        to = (configuration?.Machines[machineName].Instances ?? 0) - 1;
                    else
                        from = to = int.Parse(instances);

                    break;
                }
                case > 0:
                {
                    var firstMatches = matchesPatternFromToWildcard.First();

                    if (firstMatches.Groups.ContainsKey("machine"))
                        machineName = firstMatches.Groups["machine"].Value;

                    if (firstMatches.Groups.ContainsKey("instance"))
                        instances = firstMatches.Groups["instance"].Value;


                    if (instances.Contains('-')) // [2-*], [2-3-4], 
                    {
                        var instancesSplit = instances.Split('-');

                        from = int.Parse(instancesSplit[0]);

                        if (instancesSplit.Length == 2)
                        {
                            var instanceSplitTo = instancesSplit[1];

                            if (instanceSplitTo == "*")
                                to = (configuration?.Machines[machineName].Instances ?? 0) - 1;
                        }
                    }

                    break;
                }
            }

            if (from > to)
                throw new InvalidDataException("from is gt to");

            for (var i = from; i <= to; i++)
            {
                var instanceAsStr = string.Format(
                    $"{{0,{configuration?.Vagrant?.Zeroes}:D{configuration?.Vagrant?.Zeroes}}}", i);

                list.Add(
                    Prefix(
                        machineName,
                        instanceAsStr,
                        prefixWithDirBase,
                        dirBase,
                        namingPattern
                    )
                );
            }
        }

        return list.ToArray();
    }

    private static string Prefix(
        string s,
        string instance,
        bool prefixWithDirBase,
        string dirBase,
        string namingPattern
    )
    {
        return (prefixWithDirBase ? dirBase + "-" : "")
               + namingPattern
                   .Replace("#MACHINE-NAME#", s)
                   .Replace("#MACHINE-INSTANCE#", instance)
            ;
    }
}