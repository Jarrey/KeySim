using KeyboardSim.Parser;
using KeySim.Common;
using System;
using System.IO;
using System.Linq;
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

        public async Task<Diractive> ParseData(string path, DiractiveSource sourceType)
        {
            try
            {
                string format = null, data = null;
                if (sourceType == DiractiveSource.FILE)
                {
                    data = File.ReadAllText(path);
                    format = Path.GetExtension(path);
                }
                else if (sourceType == DiractiveSource.WEB)
                {
                    var response = await HttpUtils.GetAsync(path);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK || 
                        !response.Headers.AllKeys.Contains(HttpUtils.HEADER_COTENT_TYPE, StringComparer.InvariantCultureIgnoreCase))
                    {
                        throw new InvalidDataException();
                    }
                    format = Utils.ParseMIMEType(response.Headers[HttpUtils.HEADER_COTENT_TYPE]);
                    data = await HttpUtils.ReadResponseAsync(response);
                }

                var parser = ParserRepository.Instance.GetParser(format);
                if (parser != null)
                {
                    Diractive = parser.Parse(data);
                    Diractive.FilterDiractives();
                }
                DiractivePath = path;
                SourceType = sourceType;
                return Diractive;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async void ParseData()
        {
            string lastSource = KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE]?.ToString();
            DiractiveSource lastType = (uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] == 0 ? 0 : (DiractiveSource)(uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE];
            await ParseData(lastSource, lastType);
        }
    }
}
