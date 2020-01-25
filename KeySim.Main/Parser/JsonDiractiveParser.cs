using KeyboardSim_Demo.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardSim_Demo.Parser
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
