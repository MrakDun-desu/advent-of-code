namespace AdventOfCode.Tasks;

public class Year2025Day7Task1 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        HashSet<int> beamPositions = [lines[0].IndexOf('S')];

        var splitCount = 0;
        for (var y = 1; y < lines.Length; y++) {
            for (var x = 0; x < lines[y].Length; x++) {
                if (lines[y][x] == '^' && beamPositions.Contains(x)) {
                    splitCount++;
                    beamPositions.Remove(x);
                    beamPositions.Add(x + 1);
                    beamPositions.Add(x - 1);
                }
            }
        }

        return splitCount.ToString();
    }
}
