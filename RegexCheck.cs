using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FlyffUniverse_WebClient
{
    public static class RegexCheck
    {
        public static GroupCollection Test(string arg, string rg)
        {
            Regex r = new Regex(rg, RegexOptions.IgnoreCase);
            Match m = r.Match(arg);
            if (m.Success) { return m.Groups; }
            else { return null; }
        }
    }
}
