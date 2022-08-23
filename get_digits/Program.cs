using System.Net;

async Task GetOneMillionDigits(int start)
{
    using var client = new HttpClient();
    var numberOfDigits = 1000;
    var numberOfDigitsLong = (ulong) numberOfDigits;

    var billion = (int) (start / 1000);
    ulong startMillion = (ulong) start * 1_000_000;
    var myPi = "";

    HttpResponseMessage response;
    string json;

    for (int i = 0; i < 1000; i++)
    {
        try
        {
            do {
                response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={startMillion}&numberOfDigits={numberOfDigits}&radix=10");
            } while (response.StatusCode != HttpStatusCode.OK);
        }
        catch (System.Exception)
        {
            Console.WriteLine($"File with HTTP ERROR -> {billion}__{start}");
            return;
        }

        json = await response.Content.ReadAsStringAsync();

        myPi += json.Substring(12, numberOfDigits);

        startMillion += numberOfDigitsLong;

        if (i % 100 == 0)
        {
            Console.WriteLine($"File_{billion}__{start} -> {i/10}%");
        }
    }

    Directory.CreateDirectory($"..\\pi_billion_{billion}_{billion+1}");
    await File.WriteAllTextAsync($"..\\pi_billion_{billion}_{billion+1}\\pi_million_{start}_{start+1}.txt", myPi);

    Console.WriteLine($"END -> {start}_{start+1}");
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

while (true)
{
    var tasks = new List<Task>();

    var billion = 0;
    var million = 33_000;
    var filesToGet = 500;

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
