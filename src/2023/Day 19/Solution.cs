using Common;
using Day_19;

internal class Solution : PuzzleSolution<OrganizingSystem>
{
    public override OrganizingSystem ParseInput(string[] lines)
    {
        var workflows = new Dictionary<string, Workflow>();
        var ratings = new List<IDictionary<char, int>>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            if (line[0] == '{')
            {
                // Rating
                var splitted = line.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<char, int> ratingsDict = new Dictionary<char, int>();
                foreach (var item in splitted)
                {
                    ratingsDict.Add(item[0], int.Parse(item[2..]));
                }
                ratings.Add(ratingsDict);
            }
            else
            {
                // Workflow
                var splitted = line.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                var key = splitted[0];
                var rules = new List<Rule>();
                foreach (var item in splitted[1].Split(','))
                {
                    // Rule
                    if(!item.Contains(':'))
                    {
                        rules.Add(new Rule(item));
                    }
                    else
                    {
                        var ruleSplitted = item.Split(':');
                        rules.Add(new Rule(item[0], item[1], int.Parse(ruleSplitted[0][2..]), ruleSplitted[1]));
                    }
                }
                workflows.Add(key, new Workflow(key, rules.ToArray()));
            }
        }
        return new OrganizingSystem(workflows, ratings.ToArray());
    }

    public override string[] Part1()
    {
        int result = Input.GetSumOfAllAcceptedRatings();
        return new string[] { result.ToString() };
    }

    public override string[] Part2()
    {
        int result = Input.GetSumOfAllPossibleRanges();
        return new string[] { result.ToString() };
    }
}