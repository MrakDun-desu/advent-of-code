namespace AdventOfCode.Tasks;

public class Year2024Day5Task1 : IDailyTask
{
    public string Execute(string input)
    {
        var inputs = input.Split("\r\n\r\n");
        var ruleLines = inputs[0].Split("\r\n");
        var pageLines = inputs[1].Split("\r\n")[..^1];

        var rules = ruleLines.Select(static line =>
        {
            var nums = line.Split("|");
            return (int.Parse(nums[0]), int.Parse(nums[1]));
        });
        var pages = pageLines
            .Select(static line => line.Split(',').Select(int.Parse).ToList())
            .ToList();

        return pages
            .Aggregate(
                0,
                (acc, page) =>
                {
                    foreach (var rule in rules)
                    {
                        var item2Index = page.FindIndex(val => val == rule.Item2);
                        if (
                            page.FindIndex(val => val == rule.Item1) > item2Index
                            && item2Index > -1
                        )
                        {
                            return acc;
                        }
                    }
                    return acc + page[page.Count / 2];
                }
            )
            .ToString();
    }
}
