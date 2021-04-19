using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Utilities
{
    static class RandomString
    {
        public static string Generate(int length, Random randomGenerator)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
        }
    }
}
