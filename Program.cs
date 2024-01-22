using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotRot
{
    public enum CypherTypes
    {
        LettersOnly,
        LettersAndNumbers,
        LettersAndNumbersAndSymbols
    }
    public class RotRot
    {
        private static char[] MatchTo(CypherTypes type)
        {
            if (type == CypherTypes.LettersOnly)
            {
                return letters;
            } else if (type == CypherTypes.LettersAndNumbers)
            {
                return numbers;
            } else if (type == CypherTypes.LettersAndNumbersAndSymbols)
            {
                return everything;
            } else
            {
                return null;
            }
        }
        private static char MatchHeader(CypherTypes type)
        {
            if (type == CypherTypes.LettersOnly)
            {
                return '%';
            }
            else if (type == CypherTypes.LettersAndNumbers)
            {
                return '#';
            }
            else if (type == CypherTypes.LettersAndNumbersAndSymbols)
            {
                return '!';
            }
            else
            {
                return ' ';
            }
        }
        private static CypherTypes InverseMatchHeader(string input)
        {
            char head = input[0];
            if (head.Equals('%'))
            {
                return CypherTypes.LettersOnly;
            } else if (head.Equals('#'))
            {
                return CypherTypes.LettersAndNumbers;
            } else
            {
                return CypherTypes.LettersAndNumbersAndSymbols;
            }
        }
        private static int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
        private static readonly char[] everything = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 ,./;'[]\\=".ToCharArray();
        private static readonly char[] numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 ".ToCharArray();
        private static readonly char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ".ToCharArray();
        public static string Decrypt (string input)//Out: %: Letters only, #: Letters and numbers, !, everything
        {
            char[] chosen = MatchTo(InverseMatchHeader(input));
            int rotinc = 0;
            char[] chars = input.ToCharArray();
            chars = chars.Skip(1).ToArray();//Delete Header
            List<char> result = new List<char>();
            foreach (char c in chars)
            {
                int zloc = Array.IndexOf (chosen, c);
                if (zloc == -1)
                {
                    result.Add(c);
                }
                else
                {       
                    int cr = mod(zloc - rotinc,chosen.Length);//use custom mod function because % operator breaks on negative numbers.
                    result.Add(chosen[cr]);
                }
                rotinc++;
            }
            return new string(result.ToArray ());
        }
        public static string Encrypt(string input,CypherTypes type)
        {
            char[] chosen = MatchTo(type);
            int rotinc = 0;
            char[] chars = input.ToCharArray();
            List<char> output = new List<char>();
            foreach (char c in chars)
            {
                int loc = Array.IndexOf(chosen, c);
                if (loc == -1)
                {
                    //Console.Write("!");
                    //Character not found
                    output.Add(c);
                }
                else
                {
                    int rzval = (loc + rotinc) % chosen.Length; //Avoid calling character 72 or higher by modulusing
                    output.Add(chosen[rzval]);
                }
                rotinc++;
            }
            output.Insert(0,MatchHeader(type));//Insert header
            return new string(output.ToArray());
        }
    }
}
