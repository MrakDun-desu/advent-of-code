using System.Numerics;

namespace AdventOfCode.Tasks;

public class Year2025Day8Task1 : IDailyTask {
    private record PosRecord(Vector3 Pos) {
        public required HashSet<PosRecord> ContainingSet { get; set; }
    }

    public string Execute(string input) {
        var circuits = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Split(',').Select(float.Parse).ToArray())
            .Select(x => new Vector3(x))
            .Select(p => {
                var output = new HashSet<PosRecord>();
                output.Add(new PosRecord(p) { ContainingSet = output });
                return output;
            }).ToList();

        var distancesCount = circuits.Count * circuits.Count + 1;
        var distances = new List<(float Dist, PosRecord Start, PosRecord End)>(distancesCount);
        for (var i = 0; i < circuits.Count(); i++) {
            for (var j = i + 1; j < circuits.Count; j++) {
                var pos1 = circuits[i].First();
                var pos2 = circuits[j].First();
                var dist = (pos1.Pos - pos2.Pos).Length();
                distances.Add((dist, pos1, pos2));
            }
        }

        distances.Sort((a, b) => a.Dist.CompareTo(b.Dist));

        int iterations = circuits.Count switch {
            20 => 10,
            _ => 1000
        };

        for (var i = 0; i < iterations; i++) {
            var currentDist = distances[i];
            var firstSet = currentDist.Start.ContainingSet;
            var secondSet = currentDist.End.ContainingSet;
            if (firstSet == secondSet) {
                continue;
            }

            foreach (var item in secondSet) {
                item.ContainingSet = firstSet;
                firstSet.Add(item);
            }
            circuits.Remove(secondSet);
        }

        return circuits
            .Select(c => c.Count)
            .OrderDescending()
            .Take(3)
            .Aggregate((acc, curr) => acc * curr)
            .ToString();

    }
}
