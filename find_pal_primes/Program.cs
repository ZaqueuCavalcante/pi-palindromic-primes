using System.Text;

Console.WriteLine("START");

var millionOut = 0;
var digitsOut = 23;

while (true)
{
    var tasks = new List<Task>();

    for (int i = 0; i < 500; i++)
    {
        tasks.Add(GetFirstPalindromicPrime(millionOut, digitsOut));
        millionOut ++;
    }

    Console.WriteLine($"Find in {tasks.Count} files --- last_million={millionOut-1}");
    await Task.WhenAll(tasks);
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //



async Task<bool> GetFirstPalindromicPrime(int million, int digits)
{
    var myPi = new StringBuilder(1_000_000 + digits, 1_000_000 + digits);
    var billion = (int) (million / 1000);
    var fileName = $"..\\pi_billion_{billion}_{billion+1}\\pi_million_{million}_{million+1}.txt";
    myPi.Append(await File.ReadAllTextAsync(fileName));

    var nextMillion = million + 1;
    var nextBillion = (int) (nextMillion / 1000);
    var nextFileName = $"..\\pi_billion_{nextBillion}_{nextBillion+1}\\pi_million_{nextMillion}_{nextMillion+1}.txt";
    var nextDigits = (await File.ReadAllTextAsync(nextFileName)).Substring(0, digits);

    myPi.Append(nextDigits);

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
                        Console.WriteLine($"palindromic={number} ---- in index={start} ---- of file=pi_million_{million}");
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
