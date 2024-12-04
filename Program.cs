using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Tasks;

var inputPath = $"Inputs/{args[0]}/{args[1]}/";
var shortInput = File.ReadAllText(inputPath + "short");
var longInput = File.ReadAllText(inputPath + "long");

var task1 = GetTask($"Year{args[0]}Day{args[1]}Task1");
var task2 = GetTask($"Year{args[0]}Day{args[1]}Task2");

long time;
string result;
TimeSpan computeTime;

time = Stopwatch.GetTimestamp();
result = task1.Execute(shortInput);
computeTime = Stopwatch.GetElapsedTime(time);
Console.WriteLine($"Task 1 short: {result}. Took {computeTime.TotalSeconds}");

time = Stopwatch.GetTimestamp();
result = task1.Execute(longInput);
computeTime = Stopwatch.GetElapsedTime(time);
Console.WriteLine($"Task 1 long: {result}. Took {computeTime.TotalSeconds}");

time = Stopwatch.GetTimestamp();
result = task2.Execute(shortInput);
computeTime = Stopwatch.GetElapsedTime(time);
Console.WriteLine($"Task 2 short: {result}. Took {computeTime.TotalSeconds}");

time = Stopwatch.GetTimestamp();
result = task2.Execute(longInput);
computeTime = Stopwatch.GetElapsedTime(time);
Console.WriteLine($"Task 2 long: {result}. Took {computeTime.TotalSeconds}");

return;

IDailyTask GetTask(string className)
{
    var assembly = Assembly.GetExecutingAssembly();

    var type = assembly.GetTypes().First(t => t.Name == className);

    return (IDailyTask)Activator.CreateInstance(type)!;
}
