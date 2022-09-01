using System.Net;
using System.Text;

async Task GetOneMillionDigits(int start)
{
    Console.WriteLine($"START MILLION -> {start}");

    using var client = new HttpClient();
    var numberOfDigits = 1_000;
    var numberOfDigitsLong = (ulong) numberOfDigits;

    var billion = (int) (start / 1000);
    ulong startMillion = (ulong) start * 1_000_000;
    var myPi = new StringBuilder(1_000_000, 1_000_000);

    HttpResponseMessage response;
    string json;

    for (int i = 0; i < 1000; i++)
    {
        try
        {
            response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={startMillion}&numberOfDigits={numberOfDigits}&radix=10");
            if (response.StatusCode != HttpStatusCode.OK) { throw new Exception(""); }
        }
        catch (System.Exception)
        {
            Console.WriteLine($"File with HTTP ERROR -> {billion}__{start}");
            return;
        }

        json = await response.Content.ReadAsStringAsync();

        myPi.Append(json.Substring(12, numberOfDigits));

        startMillion += numberOfDigitsLong;
    }

    Directory.CreateDirectory($"..\\pi_billion_{billion}_{billion+1}");
    await File.WriteAllTextAsync($"..\\pi_billion_{billion}_{billion+1}\\pi_million_{start}_{start+1}.txt", myPi.ToString());

    Console.WriteLine($"END -> {start}_{start+1}");
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

while (true)
{
    var tasks = new List<Task>();

    var billion = 0;
    var million = 99_000;
    var filesToGet = 26;

    for (int i = 0; i < filesToGet;)
    {
        billion = (int) (million / 1000);

        var fileName = $"..\\pi_billion_{billion}_{billion+1}\\pi_million_{million}_{million+1}.txt";

        if (!File.Exists(fileName)) {
            tasks.Add(GetOneMillionDigits(million));
            i++;
        }

        million ++;
    }

    Console.WriteLine($"Fetching {tasks.Count} files from PI API...");
    await Task.WhenAll(tasks);
}
