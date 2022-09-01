using System.Net;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PiApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PiController : ControllerBase
{
    [HttpGet("check-prime")]
    public async Task<IActionResult> CheckPrime([FromQuery] string number)
    {
        using var client = new HttpClient();

        var nvc = new List<KeyValuePair<string, string>>();
        nvc.Add(new KeyValuePair<string, string>("number", number));
        nvc.Add(new KeyValuePair<string, string>("action", "check"));
        nvc.Add(new KeyValuePair<string, string>("_p1", "2043"));

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://www.numberempire.com/primenumbers.php") { Content = new FormUrlEncodedContent(nvc) };
        var request = await client.SendAsync(requestMessage);
        if (!request.IsSuccessStatusCode)
        {
            return BadRequest("API OFF");
        }

        var response = await request.Content.ReadAsStringAsync();

        var isPrime = response.Substring(response.IndexOf("Number "), 500).Contains("is a prime");

        return Ok(isPrime);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetMillion([FromQuery] int million)
    {
        using var connection = new NpgsqlConnection(Startup.Cnn);
        var sql = "SELECT digits FROM ppp.pi WHERE million = @Million";

        var pi = await connection.QueryFirstAsync<string>(sql, new { Million = million });

        return Ok(pi);
    }

    [HttpGet("save")]
    public async Task<IActionResult> SaveMillion([FromQuery] int million = 0)
    {
        var start = million;

        using var connection = new NpgsqlConnection(Startup.Cnn);
        var sql = "SELECT count(1) > 0 FROM ppp.pi WHERE million = @Million";

        while (million < 1_000_000_000)
        {
            var tasks = new List<Task>();

            million = start;
            var filesToGet = 300;

            for (int i = 0; i < filesToGet;)
            {
                var exists = await connection.QueryFirstAsync<bool>(sql, new { Million = million });

                if (!exists) {
                    tasks.Add(GetOneMillionDigits(million));
                    i++;
                }

                million ++;
            }

            Console.WriteLine($"Fetching {tasks.Count} files from PI API...");
            Task.WaitAll(tasks.ToArray());
        }

        return Ok($"OK -> {million}");
    }

    [HttpGet("find")]
    public async Task<IActionResult> FindPPP([FromQuery] int start, [FromQuery] int digits)
    {
        await GetFirstPalindromicPrime(start, digits);

        return Ok($"OK -> {start}");
    }

    private async Task GetOneMillionDigits(int start)
    {
        Console.WriteLine($"START MILLION -> {start}");

        using var client = new HttpClient();
        var numberOfDigits = 1_000;
        var numberOfDigitsLong = (ulong) numberOfDigits;

        ulong startMillion = (ulong) start * 1_000_000;
        var myPi = new StringBuilder(1_000_000, 1_000_000);

        HttpResponseMessage response;
        string json;

        while (myPi.Length != 1_000_000)
        {
            try
            {
                do {
                    response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={startMillion}&numberOfDigits={numberOfDigits}&radix=10");
                } while (response.StatusCode != HttpStatusCode.OK);
            }
            catch (System.Exception)
            {
                Console.WriteLine($"HTTP ERROR ON GET MILLION -> {start}");
                return;
            }

            // if (myPi.Length % 100_000 == 0) { Console.WriteLine($"Million={start} -> {myPi.Length / 10_000} %"); }

            json = await response.Content.ReadAsStringAsync();

            myPi.Append(json.Substring(12, numberOfDigits));

            startMillion += numberOfDigitsLong;
        }

        using var connection = new NpgsqlConnection(Startup.Cnn);
        var sql = "INSERT INTO ppp.pi (million, digits) VALUES(@Million, @Digits)";
        await connection.ExecuteAsync(sql, new { Million = start, Digits = myPi.ToString(), });

        Console.WriteLine($"END MILLION -> {start}");
    }

    private async Task<bool> GetFirstPalindromicPrime(int million, int digits)
    {
        var myPi = new StringBuilder(1_000_000, 1_000_000);
        using var connection = new NpgsqlConnection(Startup.Cnn);
        var sql = "SELECT digits FROM ppp.pi WHERE million = @Million";
        myPi.Append(await connection.QueryFirstAsync<string>(sql, new { Million = million }));

        // var nextDigits = (await File.ReadAllTextAsync(nextFileName)).Substring(0, digits);
        // myPi.Append(nextDigits);

        var endMax = myPi.Length - 1;
        var stepToCenter = (int) (digits/2 - 1);

        var start = 0;
        var end = digits - 1;
        var left = start + 1;
        var right = end - 1;

        var isPalindromic = false;

        while (end < endMax)
        {
            if (myPi[end] is '1' or '3' or '7' or '9')
            {
                if (myPi[start] == myPi[end])
                {
                    if (myPi[start+stepToCenter] == myPi[end-stepToCenter])
                    {
                        isPalindromic = true;
                        while (left != right)
                        {
                            if (myPi[left] == myPi[right])
                            {
                                left += 1;
                                right -= 1;
                            } else {
                                isPalindromic = false;
                                break;
                            }
                        }
                        if (isPalindromic)
                        {
                            var number = myPi.ToString(start, digits);
                            Console.WriteLine($"palindromic={number} ---- in index={start} ---- of million={million}");
                        }
                    }
                }
            }

            start += 1;
            end = start + digits - 1;
            left = start + 1;
            right = end - 1;
        }

        return false;
    }
}
