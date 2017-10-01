using System;
using System.IO;
using SimpleScanner;
using ScannerHelper;

namespace Main
{
    class mymain
    {
        static void Main(string[] args)
        {
            // Чтобы вещественные числа распознавались и отображались в формате 3.14 (а не 3,14 как в русской Culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            var fname = @"..\..\a.txt";
            Console.WriteLine(File.ReadAllText(fname));
            Console.WriteLine("-------------------------");

            Scanner scanner = new Scanner(new FileStream(fname, FileMode.Open));

            int numberOfTokens = 0;
            int min = int.MaxValue, max = 0;
            double avg = 0;

            int intSum = 0;
            double realSum = 0.0;

            int tok = 0;
            do {
                tok = scanner.yylex();
                if (tok == (int)Tok.EOF)
                    break;
                Console.WriteLine(scanner.TokToString((Tok)tok));

                ++numberOfTokens;
                int len = scanner.yyleng;
                if (len < min) min = len;
                if (max < len) max = len;
                avg += len;

                if (tok == (int)Tok.INUM)
                    intSum += scanner.LexValueInt;
                else if (tok == (int)Tok.RNUM)
                    realSum += scanner.LexValueDouble;
            } while (true);
            avg /= numberOfTokens;

            Console.WriteLine();
            Console.WriteLine("count = " + numberOfTokens);
            Console.WriteLine("min length = " + min);
            Console.WriteLine("max length = " + max);
            Console.WriteLine("average length = " + avg);
            Console.WriteLine();
            Console.WriteLine("int sum = " + intSum);
            Console.WriteLine("real sum = " + realSum);

            Console.ReadKey();
        }
    }
}
