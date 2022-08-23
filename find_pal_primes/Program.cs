using System.Diagnostics;

bool IsPrime(ulong number)
{
    if (number%2 == 0) return false;

    var max = (ulong) Math.Sqrt(number);
    for (ulong i = 3; i < max+1; i += 2)
    {
        if (number%i == 0) return false;
    }
    return true;
}

async Task<bool> GetFirstPalindromicPrime(int million, int digits)
{
    var myPi = "31415";
    var billion = (int) (million / 1000);
    var fileName = $"..\\pi_billion_{billion}_{billion+1}\\pi_million_{million}_{million+1}.txt";
    myPi = await File.ReadAllTextAsync(fileName);

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
                        var number = myPi.Substring(start, digits);
                        Console.WriteLine($"palindromic={number} ---- in index={start} ---- of file=pi_million_{million}");
                        if (IsPrime(ulong.Parse(number)))
                        {
                            return true;
                        }
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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

var watch = Stopwatch.StartNew();
Console.WriteLine("START");

var million = 0;
var digits = 19;

var found = false;
while (!found)
{
    found = await GetFirstPalindromicPrime(million, digits);
    million ++;
}

watch.Stop();
Console.WriteLine($"Duration = {watch.ElapsedMilliseconds/1000} seconds...");

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //




