using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOCDay2_Passwords
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            MatchCollection matches = Regex.Matches(input, @"(?<min>\d+)\-(?<max>\d+) (?<char>.)\: (?<password>.+)");

            int validCount1 = matches.Where(m => TestPasswordMinMax(int.Parse(m.Groups["min"].Value),
                                                                    int.Parse(m.Groups["max"].Value), 
                                                                    m.Groups["char"].Value[0], 
                                                                    m.Groups["password"].Value)).Count();

            Console.WriteLine($"Valid Password Count Using Min Max Test: {validCount1}");

            int validCount2 = matches.Where(m => TestPasswordPositionalOccurance(int.Parse(m.Groups["min"].Value),
                                                                                 int.Parse(m.Groups["max"].Value),
                                                                                 m.Groups["char"].Value[0],
                                                                                 m.Groups["password"].Value)).Count();

            Console.WriteLine($"Valid Password Count Using Positional Test: {validCount2}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static bool TestPasswordMinMax(int minOccurances, int maxOccurances, char character, string password)
        {
            int occurances = password.Where(c => c == character).Count();
            return occurances >= minOccurances && occurances <= maxOccurances;
        }

        static bool TestPasswordPositionalOccurance(int position1, int postiion2, char character, string password)
        {
            char characterAtPostion1 = password[position1 - 1];
            char characterAtPostion2 = password[postiion2 - 1];

            return characterAtPostion1 != characterAtPostion2 &&
                   (characterAtPostion1 == character || characterAtPostion2 == character);
        }
    }
}
