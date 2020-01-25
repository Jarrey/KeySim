using KeyboardSim_Demo.Model;

namespace KeyboardSim_Demo.Parser
{
    public interface IParser
    {
        string Name { get; }
        string Format { get; }
        string[] Exts { get; }

        Diractive Parse(string data);
    }
}
