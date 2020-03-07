using KeySim.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardSim.Parser
{
    public class ParserRepository
    {
        #region Singlton Methods
        private static ParserRepository _instance = null;
        public static ParserRepository Instance => _instance ?? (_instance = new ParserRepository());
        private ParserRepository()
        {
            Parsers = new Dictionary<string, IParser>();
        }
        #endregion

        private const string Separator = "|";
        public IDictionary<string, IParser> Parsers { get; private set; }

        public string SupportFormatFilter {
            get
            {
                List<string> filter = new List<string>();
                foreach (IParser parser in Parsers.Values)
                {
                    filter.Add(parser.Format + Separator + string.Join(";", parser.Exts.Select(e => "*" + e)));
                }
                return string.Join(Separator, filter);
            }
        }

        public void RegisterParser(IParser parser)
        {
            List<string> key = parser.Exts.ToList();
            key.Add(parser.Format.ToLower());
            Parsers[parser.Name] = parser;
        }

        public IParser GetParser(string ext)
        {
            string e = ext?.Trim().ToLower();
            foreach (IParser parser in Parsers.Values)
            {
                if (parser.Exts.Any(p => p.Contains(e)) || parser.Format.Contains(e, StringComparison.InvariantCultureIgnoreCase, false))
                {
                    return parser;
                }
            }
            return null;
        }
    }
}
