# Pi Palindromic Primes Finder

See challenge here: https://sigmageek.com

Prime checker: https://www.numberempire.com/primenumbers.php

Pi digits API: https://api.pi.delivery/v1/pi?start=0&numberOfDigits=100&radix=10

First palprimes:

| digits | number                | index           | time (seconds) |
|:------:|:---------------------:|:---------------:|:--------------:|
| 01     | 3                     | 0               | 0              |
| 02     | 11                    | 94              | 0              |
| 03     | 383                   | 25              | 0              |
| 04     | -                     | -               | -              |
| 05     | 38183                 | 488             | 0              |
| 06     | -                     | -               | -              |
| 07     | 9149419               | 13.901          | 0              |
| 08     | -                     | -               | -              |
| 09     | 318272813             | 129.079         | 0              |
| 10     | -                     | -               | -              |
| 11     | 74670707647           | 5.793.497       | 1              |
| 12     | -                     | -               | -              |
| 13     | 1020776770201         | 25.803.984      | 2              |
| 14     | -                     | -               | -              |
| 15     | 176860696068671       | 298.503.033     | 4              |
| 16     | -                     | -               | -              |
| 17     | 30948834143884903     | 6.604.858.609   | 70             |
| 18     | -                     | -               | -              |
| 19     | 7240428184818240427   | 72.075.707.767  | 500            |
| 20     | -                     | -               | -              |
| 21     | 151978145606541879151 | 140.672.630.233 | 6000           |

## Guess to first 21 digits palprime

#### y = e^(0.64305387405183245253 + 1.28183914103204066315*x)

| digits | index           | guess             | error           | error |
|:------:|:---------------:|:-----------------:|:---------------:|:-----:|
| 09     | 129.079         | 194.776           | +65.697         | +51%  |
| 11     | 5.793.498       | 2.528.873         | -3.264.625      | -56%  |
| 13     | 25.803.984      | 32.833.592        | +7.029.608      | +27%  |
| 15     | 298.503.034     | 426.294.504       | +127.791.470    | +43%  |
| 17     | 6.604.858.609   | 5.534.788.946     | -1.070.069.663  | -16%  |
| 19     | 72.075.707.767  | 71.860.857.656    | +214.850.111    | +0.3% |
| 21     | ?               | 933.004.476.480   | ?               | ?     |
| 21     | 880.347.462.679 | 933.004.476.480   | ?               | ?     |


## Find Palindromes

- Download Pi_0.ycd and Pi_1.ycd
- Extract 10.000 to 100.000.020.000 digits => Pi_0_1.txt
- Run C# code for Pi_0_1.txt with lenght >= 21 | Save in output.txt
- Delete Pi_0.ycd and Pi_0_1.txt files
- Download Pi_2.ycd
- Extract 100.000.010.000 to 200.000.020.000 digits => Pi_1_2.txt
- Run C# code for Pi_1_2.txt... and repeat


