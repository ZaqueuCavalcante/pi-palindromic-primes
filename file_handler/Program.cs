using System.Diagnostics;
using System.Text;
using Dapper;
using Npgsql;

var cnn = "Server=35.199.95.198;Database=postgres;Uid=zaqueu;Pwd=HK|G]TsGr8$NEp7r#4m3E&1%mR6c;";

var watch = Stopwatch.StartNew();
Console.WriteLine("Start...");

var million = 0;

using var connection = new NpgsqlConnection(cnn);
var sql = "SELECT million FROM ppp.pi WHERE million >= @Million AND million <= 100000";
var allMillions = (await connection.QueryAsync<int>(sql, new { @Million = million })).ToList();

while (million < 144_000)
{
    var myPi = new StringBuilder(100_000_000, 100_000_000);

    var billion = (int) (million / 1000);
    var fileName = $"..\\pi_billion_{billion}_{billion+1}\\pi_million_{million}_{million+1}.txt";

    if (!allMillions.Contains(million) && File.Exists(fileName))
    {
        myPi.Append(await File.ReadAllTextAsync(fileName));

        var sqlInsert = "INSERT INTO ppp.pi (million, digits) VALUES(@Million, @Digits)";
        await connection.ExecuteAsync(sqlInsert, new { Million = million, Digits = myPi.ToString(), });

        Console.WriteLine($"Finish million = {million}");
    }

    million ++;
}



watch!.Stop();
Console.WriteLine($"Duration = {watch.ElapsedMilliseconds/1000} seconds...");
