using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DummyLibrary
{
    public class TextSearch
    {
        public List<string> GetPhoneNumber(string text)
        {
            string pattern = @"06-\d{8}";
            Regex rg = new Regex(pattern);
            var matches = rg.Matches(text);
            return matches.Select(t => t.Value).ToList();
        }
    }
}
