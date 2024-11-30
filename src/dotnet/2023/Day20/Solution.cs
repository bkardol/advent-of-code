using Common;
using Common.MathExtensions;
using Day20;

internal class Solution : PuzzleSolution<Dictionary<string, Module>>
{
    public override Dictionary<string, Module> ParseInput(string[] lines)
    {
        var dict = lines.Select(line =>
        {
            var splitted = line.Split(" -> ");
            var isFlipFlop = splitted[0][0] == '%';
            var isConjunction = splitted[0][0] == '&';
            return new Module(splitted[0][1..], isFlipFlop, isConjunction, splitted[1].Split(", "));
        }).ToDictionary(m => m.Key, m => m);

        foreach (var key in dict.Keys)
        {
            var module = dict[key];
            module.SetPossibleSources(dict.Values.Where(m => m.Destinations.Contains(key)).Select(m => m.Key).ToArray());
        }

        return dict;
    }

    public override string[] Part1()
    {
        List<bool> pulses = [];
        Queue<(string source, string module, bool pulse)> instructions = new();
        for (int i = 0; i < 1000; i++)
        {
            instructions.Enqueue(("button", "roadcaster", false));
            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                pulses.Add(instruction.pulse);
                if (!Input.ContainsKey(instruction.module))
                {
                    continue;
                }

                var module = Input[instruction.module];
                var isHighPulse = module.ProcessPulse(instruction.source, instruction.pulse);

                if (isHighPulse != null)
                {
                    foreach (var dest in module.Destinations)
                    {
                        instructions.Enqueue((module.Key, dest, (bool)isHighPulse));
                    }
                }
            }
        }
        return [(pulses.Count(p => p) * pulses.Count(p => !p)).ToString()];
    }

    public override string[] Part2()
    {
        Queue<(string source, string module, bool pulse)> instructions = new();

        var rxConjunctionSource = Input.Values.First(m => m.Destinations.Contains("rx")).Key;
        Dictionary<string, int> rxSources = Input.Values.Where(m => m.Destinations.Contains(rxConjunctionSource)).ToDictionary(m => m.Key, m => 0);

        for (int i = 1; ; i++)
        {
            instructions.Enqueue(("button", "roadcaster", false));
            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                if (!Input.ContainsKey(instruction.module))
                {
                    continue;
                }

                var module = Input[instruction.module];
                var isHighPulse = module.ProcessPulse(instruction.source, instruction.pulse);

                if (rxSources.TryGetValue(instruction.module, out int value) && value == 0 && isHighPulse == true)
                {
                    rxSources[instruction.module] = i;
                }

                if (isHighPulse != null)
                {
                    foreach (var dest in module.Destinations)
                    {
                        instructions.Enqueue((module.Key, dest, (bool)isHighPulse));
                    }
                }
            }
            if (rxSources.Values.All(highPulseIndex => highPulseIndex > 0))
            {
                break;
            }
        }

        var result = rxSources.Values.Select(i => (long)i).Aggregate(CommonMath.LeastCommonMultiple);
        return [result.ToString()];
    }
}