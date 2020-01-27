using KeyboardSim.Model;
using Newtonsoft.Json;
using System;

namespace KeyboardSim.Parser
{
    public class JsonDiractiveParser : IParser
    {
        #region Properties

        public string Name => "JSON Data Source Parser";
        public string Format => "JSON";
        public string[] Exts => new[] { ".json", ".js" };

        #endregion

        public JsonDiractiveParser()
        {
        }

        public Diractive Parse(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<Diractive>(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
