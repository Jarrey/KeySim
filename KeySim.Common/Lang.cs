using System.Collections.ObjectModel;

namespace KeySim.Common
{
    /// <summary>
    ///     A collection for language strings
    /// </summary>
    public class Lang : Collection<L>
    {
    }

    /// <summary>
    ///     A class for language string item defined in resources
    /// </summary>
    public class L
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}