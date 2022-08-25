using System.Diagnostics;
using System.Text;

var watch = Stopwatch.StartNew();
Console.WriteLine("Start...");

var million = 6_000;
var millionHundred = 0;
var billion = (int) (million / 1000);

for (int i = 0; i < 10; i++)
{
    var myPi = new StringBuilder(100_000_000, 100_000_000);
    millionHundred = (int) (million / 100);
    
    for (int j = 0; j < 100; j++)
    {
        var fileName = $"..\\pi_billion_{billion}_{billion+1}\\pi_million_{million}_{million+1}.txt";
        myPi.Append(await File.ReadAllTextAsync(fileName));
        million ++;
    }

    await File.WriteAllTextAsync($"..\\pi_billion_{billion}_{billion+1}\\pi_million_hundred_{millionHundred}_{millionHundred+1}.txt", myPi.ToString());
    Console.WriteLine($"Finish million_hundred = {millionHundred}");
}

watch!.Stop();
Console.WriteLine($"Duration = {watch.ElapsedMilliseconds} seconds...");
