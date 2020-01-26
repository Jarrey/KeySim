using KeyboardSim.Model;

namespace KeyboardSim.Parser
{
    public interface IParser
    {
        string Name { get; }
        string Format { get; }
        string[] Exts { get; }

        Diractive Parse(string data);
    }
}
