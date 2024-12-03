using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Tasks;

var inputPath = $"Inputs/{args[0]}/{args[1]}/";
var shortInput = File.ReadAllText(inputPath + "short");
var longInput = File.ReadAllText(inputPath + "long");

var task1 = GetTask($"Year{args[0]}Day{args[1]}Task1");
var task2 = GetTask($"Year{args[0]}Day{args[1]}Task2");

var time = Stopwatch.GetTimestamp();
var task1Short = task1.Execute(shortInput);
var task1ShortTime = Stopwatch.GetElapsedTime(time);

time = Stopwatch.GetTimestamp();
var task1Long = task1.Execute(longInput);
var task1LongTime = Stopwatch.GetElapsedTime(time);

time = Stopwatch.GetTimestamp();
var task2Short = task2.Execute(shortInput);
var task2ShortTime = Stopwatch.GetElapsedTime(time);

time = Stopwatch.GetTimestamp();
var task2Long = task2.Execute(longInput);
var task2LongTime = Stopwatch.GetElapsedTime(time);

Console.WriteLine($"Task 1 short: {task1Short}. Took {task1ShortTime.TotalSeconds}");
Console.WriteLine($"Task 1 long: {task1Long}. Took {task1LongTime.TotalSeconds}");
Console.WriteLine($"Task 2 short: {task2Short}. Took {task2ShortTime.TotalSeconds}");
Console.WriteLine($"Task 2 long: {task2Long}. Took {task2LongTime.TotalSeconds}");

return;

IDailyTask GetTask(string className)
{
    var assembly = Assembly.GetExecutingAssembly();

    var type = assembly.GetTypes().First(t => t.Name == className);

    return (IDailyTask)Activator.CreateInstance(type)!;
}
