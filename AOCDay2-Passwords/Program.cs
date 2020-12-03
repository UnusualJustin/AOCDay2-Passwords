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

            MatchCollection matches = Regex.Matches(input, @"(?<num1>\d+)\-(?<num2>\d+) (?<char>.)\: (?<password>.+)");
            var policyPasswordPairs = matches.Select(m => new {
                Number1 = int.Parse(m.Groups["num1"].Value),
                Number2 = int.Parse(m.Groups["num2"].Value),
                Character = m.Groups["char"].Value[0],
                Password = m.Groups["password"].Value
            });

            int validCount1 = policyPasswordPairs.Count(p => TestPasswordMinMax(p.Number1, p.Number2, p.Character, p.Password));
            int validCount2 = policyPasswordPairs.Count(p => TestPasswordPositionalOccurance(p.Number1, p.Number2, p.Character, p.Password));

            Console.WriteLine($"Valid Password Count Using Min Max Test: {validCount1}");
            Console.WriteLine($"Valid Password Count Using Positional Test: {validCount2}");
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
