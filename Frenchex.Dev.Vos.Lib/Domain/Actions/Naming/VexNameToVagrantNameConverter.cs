using System.Text.RegularExpressions;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;

public interface IVexNameToVagrantNameConverter
{
    string[] ConvertAll(string[] names, string workingDirectory, Domain.Configuration.Configuration configuration);
}

public class VexNameToVagrantNameConverter : IVexNameToVagrantNameConverter
{
    private static readonly Regex patternFromToWildcard =
        new("(?<machine>.*)\\-\\[(?<instance>.*)\\]", RegexOptions.Compiled);

    private static readonly Regex patternFromTo = new("(?<machine>.*)\\-(?<instance>.*)", RegexOptions.Compiled);

    public string[] ConvertAll(string[] inputNamedPatterns, string workingDirectory,
        Domain.Configuration.Configuration configuration)
    {
        var list = new List<string>();

        foreach (var name in inputNamedPatterns)
        {
            var matchesPatternFromToWildcard = patternFromToWildcard.Matches(name);

            switch (matchesPatternFromToWildcard.Count)
            {
                case 0:
                {
                    var matchesPatternFromTo = patternFromTo.Matches(name);

                    var firstMatches = matchesPatternFromTo.First();
                    var machineName = "";
                    var instances = "";

                    if (firstMatches.Groups.ContainsKey("machine")) machineName = firstMatches.Groups["machine"].Value;

                    if (firstMatches.Groups.ContainsKey("instance")) instances = firstMatches.Groups["instance"].Value;

                    var instanceAsStr = string.Format(configuration?.Vagrant?.NumberingFormat ?? "{0,2:D2}",
                        instances == "0" ? 0 : int.Parse(instances));

                    list.Add($"{machineName}-{instanceAsStr}");
                    break;
                }
                case > 0:
                {
                    var firstMatches = matchesPatternFromToWildcard.First();
                    var machineName = "";
                    var instances = "";

                    if (firstMatches.Groups.ContainsKey("machine")) machineName = firstMatches.Groups["machine"].Value;

                    if (firstMatches.Groups.ContainsKey("instance")) instances = firstMatches.Groups["instance"].Value;

                    var from = 0;
                    var to = 0;

                    if (instances.Contains('-')) // [2-*], [2-3-4], 
                    {
                        var instancesSplit = instances.Split('-');

                        from = int.Parse(instancesSplit[0]);

                        if (instancesSplit.Length == 2)
                        {
                            var instanceSplitTo = instancesSplit[1];

                            if (instanceSplitTo == "*") to = (configuration?.Machines[machineName]?.Instances ?? 0) - 1;
                        }
                    }

                    if (from > to)
                        throw new InvalidDataException("from is gt to");

                    for (var i = from; i <= to; i++)
                    {
                        var instanceAsStr = string.Format(configuration?.Vagrant?.NumberingFormat ?? "{0,2:D2}", i);
                        list.Add($"{machineName}-{instanceAsStr}");
                    }

                    break;
                }
            }
        }

        return list.ToArray();
    }
}