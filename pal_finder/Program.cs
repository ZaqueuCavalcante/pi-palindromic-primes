using CliWrap;

// const string urlBase = "https://storage.googleapis.com/pi100t/Pi%20-%20Dec%20-%20Chudnovsky/Pi%20-%20Dec%20-%20Chudnovsky%20-%20";

// await Cli.Wrap("wget")
//     .WithArguments(urlBase + "0.ycd")
//     .ExecuteAsync();

// await Cli.Wrap("wget")
//     .WithArguments(urlBase + "1.ycd")
//     .ExecuteAsync();


await Cli.Wrap(@".\y-cruncher.exe")
    .ExecuteAsync();









Console.WriteLine("Hello, World!");



