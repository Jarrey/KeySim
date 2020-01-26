using KeySim.Common;
using System;

namespace KeyboardSim.Model
{
    public class Diractive : ModelBase
    {
        public Diractive()
        {

        }
        public Diractive(DiractiveSource source, DiractiveFormat format, string uri = null)
        {
            Source = source;
            Format = format;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public DiractiveSource Source { get; }
        public DiractiveFormat Format { get; }

        public Action[] Diractives { get; set; }

        internal void UnregisterHotKey()
        {
            if (Diractives != null)
            {
                foreach (var action in Diractives)
                {
                    action.UnregisterHotKey();
                }
            }
        }

        internal void RegisterHotKey()
        {
            if (Diractives != null)
            {
                foreach (var action in Diractives)
                {
                    action.RegisterHotKey();
                }
            }
        }
    }
}
