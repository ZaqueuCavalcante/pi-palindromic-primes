using System.Diagnostics;
using System.Text;

public static class SimpleFinder
{
    public static async Task<char[]> GetDigits(string fileName, int million)
    {
        using var stream = File.OpenRead(fileName);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var digits = 2_000_000;

        stream.Seek((long) million * (long) 1_000_000, SeekOrigin.Begin);
        char[] buffer = new char[digits];
        await reader.ReadBlockAsync(buffer, 0, digits);

        return buffer;
    }

    public static async Task<bool> Find()
    {
        var watch = Stopwatch.StartNew();
        Console.WriteLine("Loading first digits...");

        var fileName = "C:\\Users\\Zaqueu\\Downloads\\first_100_billions.txt";

        var million = 0;
        var digits = 2_000_000;
        var myPi = new StringBuilder(digits, digits);
        myPi.Append(await GetDigits(fileName, million));

        Console.WriteLine("Digits loaded...");

        million ++;

        ulong index = 500_000;
        var center = 500_000;
        var start = center - 1;
        var end = center + 1;

        while (index < 99_000_000_000)
        {
            if (myPi[start] == myPi[end])
            {
                while (true)
                {
                    start --;
                    end ++;
                    if (myPi[start] != myPi[end])
                    {
                        start ++;
                        end --;
                        break;
                    }
                }
                if (myPi[end] is '1' or '3' or '7' or '9')
                {
                    var pppDigits = end - start + 1;
                    if (pppDigits >= 19)
                    {
                        var number = myPi.ToString(start, pppDigits);
                        ulong off = (ulong) (pppDigits+1)/2 - 1;
                        Console.WriteLine($"palindromic={number} ---- with digits={pppDigits} ---- in index={index-off-1}");
                    }
                }
            }

            index ++;

            if (index == (ulong) million * (ulong) 1_000_000 + (ulong) 500_000)
            {
                myPi = new StringBuilder(digits, digits);
                myPi.Append(await GetDigits(fileName, million));
                million++;

                center = 500_000;
                start = center - 1;
                end = center + 1;
                continue;
            }

            center ++;
            start = center - 1;
            end = center + 1;
        }

        watch!.Stop();
        Console.WriteLine($"Duration = {watch.ElapsedMilliseconds/1000} seconds...");

        return true;
    }
}
