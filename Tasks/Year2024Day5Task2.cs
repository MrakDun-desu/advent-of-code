namespace AdventOfCode.Tasks;

public class Year2024Day5Task2 : IDailyTask
{
    public string Execute(string input)
    {
        var inputs = input.Split("\r\n\r\n");
        var ruleLines = inputs[0].Split("\r\n");
        var pageLines = inputs[1].Split("\r\n")[..^1];

        var rules = ruleLines
            .Select(static line =>
            {
                var nums = line.Split("|");
                return (int.Parse(nums[0]), int.Parse(nums[1]));
            })
            .ToList();
        var pages = pageLines
            .Select(static line => line.Split(',').Select(int.Parse).ToList())
            .ToList();

        return pages
            .Aggregate(
                0,
                (acc, page) =>
                {
                    var badOrder = false;
                    for (int i = 0; i < rules.Count; i++)
                    {
                        var rule = rules[i];
                        var item1Index = page.FindIndex(val => val == rule.Item1);
                        var item2Index = page.FindIndex(val => val == rule.Item2);
                        if (item1Index > item2Index && item2Index > -1)
                        {
                            (page[item1Index], page[item2Index]) = (
                                page[item2Index],
                                page[item1Index]
                            );
                            badOrder = true;
                            i = 0;
                        }
                    }
                    return acc + (badOrder ? page[page.Count / 2] : 0);
                }
            )
            .ToString();
    }
}
