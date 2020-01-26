using KeyboardSim.Parser;
using KeySim.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardSim.Model
{
    public class DiractiveCache : ModelBase
    {
        private static DiractiveCache _instance;
        public static DiractiveCache Instance => _instance ?? (_instance = new DiractiveCache());
        private DiractiveCache()
        {
        }

        public DiractiveSource SourceType { get; set; }
        public string DiractivePath { get; set; }

        private Diractive _diractive;
        public Diractive Diractive
        {
            get { return _diractive; }
            set
            {
                SetProperty(ref _diractive, value);
            }
        }

        public Diractive ParseData(string filePath, DiractiveSource sourceType)
        {
            try
            {
                var parser = ParserRepository.Instance.GetParser(Path.GetExtension(filePath));
                string data = File.ReadAllText(filePath);
                if (parser != null)
                {
                    Diractive = parser.Parse(data);
                    Diractive.FilterDiractives();
                }
                DiractivePath = filePath;
                SourceType = sourceType;
                return Diractive;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void ParseData()
        {
            string lastSource = KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE]?.ToString();
            DiractiveSource lastType = (uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] == 0 ? 0 : (DiractiveSource)(uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE];
            ParseData(lastSource, lastType);
        }
    }
}
