using System;
using System.IO;
using System.Text;
using EquationSimplification.Lex;
using EquationSimplification.Parser;

using static   System.Console;

namespace EquationSimplification
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prevent example from ending if CTL+C is pressed.
            TreatControlCAsInput = false;
            WriteLine(" For read equations from file, press 1:");
            WriteLine(" For read equations from Console, press 2:");
            ConsoleKeyInfo cki;
            WriteLine("Press the Ctrl+C key to quit: \n");
            var equationSimplifier = new EquationSimplifier(new EquationParser(new Tokenizer()));
            cki = ReadKey();
            WriteLine();
            if (cki.Key.ToString().Contains("1") )
            {
                ReadFromFile(equationSimplifier);
            }

            if (cki.Key.ToString().Contains("2"))
            {
                ReadFromConsole(equationSimplifier);
            }

        }

        private static void ReadFromFile(EquationSimplifier equationSimplifier)
        {
            var lines = File.ReadAllLines("input.txt");
            var stringBuilder = new StringBuilder();
            foreach (var line in lines)
            {
                var simplify = equationSimplifier.Simplify(line);
                WriteLine("Simplifed equation");
                WriteLine(simplify);
                stringBuilder.AppendLine(simplify);
            }
            File.WriteAllText("equations.out", stringBuilder.ToString());
            ReadLine();
        }

        private static void ReadFromConsole(EquationSimplifier equationSimplifier)
        {
            do
            {
                WriteLine("Enter equation to simplify: \n");
                string input = ReadLine();
                WriteLine("Simplifed equation");

                var simplified = equationSimplifier.Simplify(input);
                // cki = ReadKey();
                WriteLine(simplified);
                // cki = GetConsoleKeyInfo();
            } while (true);
        }
    }
}
