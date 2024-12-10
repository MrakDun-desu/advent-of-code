namespace AdventOfCode.Tasks;

public class Year2024Day9Task1 : IDailyTask {
    public string Execute(string input) {
        var diskSize = 0u;
        input = input.Trim();
        var numInput = new byte[input.Length];
        // parse input into numbers
        for (var i = 0; i < input.Length; i++) {
            numInput[i] = (byte)(input[i] - '0');
            diskSize += numInput[i];
        }

        // create "disk"
        var diskContents = new uint[diskSize];
        var diskPos = 0u;
        var fileId = 0u;

        // fill up the disk
        for (uint i = 0; i < numInput.Length; i++) {
            var blockId = (i % 2 == 0) ? fileId++ : uint.MaxValue;
            for (byte j = 0; j < numInput[i]; j++) {
                diskContents[diskPos++] = blockId;
            }
        }

        // fragmentation
        diskPos = 0;
        var diskEndPos = diskContents.Length - 1;
        while (true) {
            while (diskContents[diskPos] != uint.MaxValue) {
                diskPos++;
                if (diskPos >= diskEndPos) {
                    goto checkSum;
                }
            }
            while (diskContents[diskEndPos] == uint.MaxValue) {
                diskEndPos--;
                if (diskPos >= diskEndPos) {
                    goto checkSum;
                }
            }
            diskContents[diskPos] = diskContents[diskEndPos];
            diskContents[diskEndPos] = uint.MaxValue;
        }

checkSum:
        var checkSum = 0uL;
        diskPos = 0;
        while (diskContents[diskPos] != uint.MaxValue) {
            checkSum += diskContents[diskPos] * diskPos;
            diskPos++;
        }

        return checkSum.ToString();
    }
}
