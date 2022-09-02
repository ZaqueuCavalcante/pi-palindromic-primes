
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

// https://cp-algorithms.com/algebra/primality_tests.html

// Factoring with Elliptic Curves - https://youtu.be/2JlpeQWtGH8
// 1º -> Fermat Test for a bunch of random bases (make optimizations)

bool IsPrimeTrivial(string number)
{
    BigInteger numberAsIint = BigInteger.Parse(number);

    if (numberAsIint % 2 == 0) return false;

    BigInteger max = (BigInteger) Math.Sqrt(double.Parse(number));
    for (BigInteger d = 3; d*d < numberAsIint; d += 2)
    {
        if (numberAsIint % d == 0) return false;
    }
    return true;
}

bool IsPrimeProbMillerRabin(string number, int certainty)
{
    BigInteger source = BigInteger.Parse(number);

    if (source % 2 == 0) return false;

    BigInteger d = source - 1;
    int s = 0;

    while(d % 2 == 0)
    {
        d /= 2;
        s += 1;
    }

    // There is no built-in method for generating random BigInteger values.
    // Instead, random BigIntegers are constructed from randomly generated
    // byte arrays of the same length as the source.
    RandomNumberGenerator rng = RandomNumberGenerator.Create();
    byte[] bytes = new byte[source.ToByteArray().LongLength];
    BigInteger a;

    for (int i = 0; i < certainty; i++)
    {
        do
        {
            rng.GetBytes(bytes);
            a = new BigInteger(bytes);
        }
        while (a < 2 || a >= source - 2);

        BigInteger x = BigInteger.ModPow(a, d, source);
        if (x == 1 || x == source - 1)
            continue;

        for (int r = 1; r < s; r++)
        {
            x = BigInteger.ModPow(x, 2, source);
            if (x == 1)
                return false;
            if (x == source - 1)
                break;
        }

        if (x != source - 1)
            return false;
    }

    return true;
}

var watch = Stopwatch.StartNew();
Console.WriteLine("Start...");

// NUMBER                | TIME - Trivial | TIME - |
// 176860696068671       | 0              | 0       
// 30948834143884903     | 12             | 0
// 7240428184818240427   | 212            | 0
// 151978145606541879151 | ?              |

Console.WriteLine(IsPrimeProbMillerRabin("176860696068671", 1_000_000));

watch!.Stop();
Console.WriteLine($"Duration = {watch.ElapsedMilliseconds/1000} seconds...");
