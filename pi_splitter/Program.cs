using System.Diagnostics;
using System.Text;

async Task<char[]> GetDigits(string fileName, int million)
{
    using var stream = File.OpenRead(fileName);
    using var reader = new StreamReader(stream, Encoding.UTF8);

    var digits = 2_000_000;

    stream.Seek((long) million * (long) 1_000_000, SeekOrigin.Begin);
    char[] buffer = new char[digits];
    await reader.ReadBlockAsync(buffer, 0, digits);

    return buffer;
}

async Task<bool> ProcessOneMillion(string fileName, int million)
{
    var digits = 2_000_000;
    var myPi = new StringBuilder(digits, digits);
    myPi.Append(await GetDigits(fileName, million));

    ulong finalIndex = (ulong) 1_500_000;

    ulong index = 500_000;
    var center = 500_000;
    var start = center - 1;
    var end = center + 1;

    while (true)
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
                var initialOff = (ulong) million * (ulong) 1_000_000;
                if (pppDigits >= 21)
                {
                    var number = myPi.ToString(start, pppDigits);
                    ulong off = (ulong) (pppDigits+1)/2 - 1;
                    Console.WriteLine($"palindromic={number} ---- with digits={pppDigits} ---- in index={initialOff+index-off-1}");
                }
            }
        }

        index ++;

        if (index == finalIndex)
        {
            return true;
        }

        center ++;
        start = center - 1;
        end = center + 1;
    }
}




var watch = Stopwatch.StartNew();
Console.WriteLine("Loading first digits...");

var fileName = "C:\\Users\\Zaqueu\\Downloads\\first_100_billions.txt";

var million = 0;

while (million < 95_000)
{
    var tasks = new List<Task>();

    for (int i = 0; i < 500; i++)
    {
        tasks.Add(ProcessOneMillion(fileName, million));
        million ++;
    }

    await Task.WhenAll(tasks);
}

watch!.Stop();
Console.WriteLine($"Duration = {watch.ElapsedMilliseconds/1000} seconds...");



// palindromic=1673530243420353761 ---- with digits=19 ---- in index=836814879
// palindromic=1268008427248008621 ---- with digits=19 ---- in index=1202100884
// palindromic=3593433732373343953 ---- with digits=19 ---- in index=4997740018
// palindromic=1878233768673328781 ---- with digits=19 ---- in index=6051515270
// palindromic=7509766528256679057 ---- with digits=19 ---- in index=14203394886
// palindromic=1032737934397372301 ---- with digits=19 ---- in index=16072000243
// palindromic=7609976354536799067 ---- with digits=19 ---- in index=16784511593
// palindromic=9585209505059025859 ---- with digits=19 ---- in index=19302552302
// palindromic=3966523750573256693 ---- with digits=19 ---- in index=20279649560
// palindromic=1725870480840785271 ---- with digits=19 ---- in index=22046931660
// palindromic=3048711419141178403 ---- with digits=19 ---- in index=22402961369
// palindromic=1159451744471549511 ---- with digits=19 ---- in index=27206294907
// palindromic=9082824024204282809 ---- with digits=19 ---- in index=36131087810
// palindromic=3881775253525771883 ---- with digits=19 ---- in index=36605716286
// palindromic=1158295131315928511 ---- with digits=19 ---- in index=42028953424
// palindromic=1557665335335667551 ---- with digits=19 ---- in index=42170294500
// palindromic=9228173132313718229 ---- with digits=19 ---- in index=45398008050
// palindromic=1820453000003540281 ---- with digits=19 ---- in index=45576894590
// palindromic=9159543969693459519 ---- with digits=19 ---- in index=54502455328
// palindromic=353332089111980233353 ---- with digits=21 ---- in index=60013316585
// palindromic=9447748870788477449 ---- with digits=19 ---- in index=62032845882
// palindromic=756529925030529925657 ---- with digits=21 ---- in index=64851776209
// palindromic=140305973282379503041 ---- with digits=21 ---- in index=64850945818
// palindromic=7240428184818240427 ---- with digits=19 ---- in index=72075707767
// palindromic=36336834991019943863363 ---- with digits=23 ---- in index=76493992135
// palindromic=9125010550550105219 ---- with digits=19 ---- in index=78833628390
// palindromic=133409054787450904331 ---- with digits=21 ---- in index=78938245258
// palindromic=738142485717584241837 ---- with digits=21 ---- in index=79633648073
// palindromic=761342770575077243167 ---- with digits=21 ---- in index=80877652435
// palindromic=3731587489847851373 ---- with digits=19 ---- in index=81915263575
// palindromic=7956862182723272812686597 ---- with digits=25 ---- in index=83804102852
// palindromic=3161323728273231613 ---- with digits=19 ---- in index=84344373362
// palindromic=36293381799299718339263 ---- with digits=23 ---- in index=85775055016
// palindromic=1731955480845591371 ---- with digits=19 ---- in index=86886634381
// palindromic=7242907147417092427 ---- with digits=19 ---- in index=91467990914
// palindromic=9511275355535721159 ---- with digits=19 ---- in index=92291382822
// palindromic=9531205620265021359 ---- with digits=19 ---- in index=92390077443
// palindromic=1804125751575214081 ---- with digits=19 ---- in index=94605800483


// palindromic=353332089111980233353 ---- with digits=21 ---- in index=60013316585
// palindromic=140305973282379503041 ---- with digits=21 ---- in index=64850945818
// palindromic=756529925030529925657 ---- with digits=21 ---- in index=64851776209
// palindromic=36336834991019943863363 ---- with digits=23 ---- in index=76493992135
// palindromic=133409054787450904331 ---- with digits=21 ---- in index=78938245258
// palindromic=738142485717584241837 ---- with digits=21 ---- in index=79633648073
// palindromic=761342770575077243167 ---- with digits=21 ---- in index=80877652435
// palindromic=7956862182723272812686597 ---- with digits=25 ---- in index=83804102852
// palindromic=36293381799299718339263 ---- with digits=23 ---- in index=85775055016

