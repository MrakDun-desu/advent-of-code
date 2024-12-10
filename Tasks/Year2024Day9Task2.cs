namespace AdventOfCode.Tasks;

public class Year2024Day9Task2 : IDailyTask {
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
        var diskEndPos = diskContents.Length - 1;
        while (true) {
            if (diskEndPos < 0) {
                goto checkSum;
            }
            while (diskContents[diskEndPos] == uint.MaxValue) {
                diskEndPos--;
                if (diskEndPos < 0) {
                    goto checkSum;
                }
            }

            // found a file (not uint.MaxValue) with fileId
            fileId = diskContents[diskEndPos];
            var fileLength = 0;
            do {
                fileLength++;
                if (diskEndPos < fileLength) {
                    goto checkSum;
                }
            } while (diskContents[diskEndPos - fileLength] == fileId);

            // check for available length
            var availableLength = 0;
            for (diskPos = 0; diskPos < diskEndPos; diskPos++) {
                // if disk is empty at this pos, add to available length, and if length is enough,
                // break
                if (diskContents[diskPos] == uint.MaxValue) {
                    availableLength++;
                    if (availableLength == fileLength) {
                        break;
                    }
                } else {
                    // if disk is not empty, set the available length to 0
                    availableLength = 0;
                }
            }
            // if available length is file length, then diskPos is the last position of file target
            // position and diskPos is the last position of the source file
            // otherwise file can't be placed and loop continues to check other file
            if (availableLength != fileLength) {
                diskEndPos -= fileLength; // decrease end pos pointer because this file can't be placed
                continue;
            }
            // while some file length is remaining, copy file ID into diskPos and change file into
            // uint.MaxValue
            while (fileLength > 0) {
                fileLength--;
                diskContents[diskPos - fileLength] = fileId;
                diskContents[diskEndPos - fileLength] = uint.MaxValue;
            }
            diskEndPos -= availableLength;
        }

checkSum:
        var checkSum = 0uL;
        for (diskPos = 0; diskPos < diskContents.Length; diskPos++) {
            if (diskContents[diskPos] != uint.MaxValue) {
                checkSum += diskContents[diskPos] * diskPos;
            }
        }

        return checkSum.ToString();
    }
}
