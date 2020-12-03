using System.Collections.Generic;

namespace ArgumentParser
{
    public interface IParser<T>
        where T : class, new()
    {
        IReadOnlyCollection<string> RawArguments { get; }

        IReadOnlyCollection<Argument> Arguments { get; }

        T Parse(params string[] args);

        T Parse(string args);

        bool IgnoreRequired { get; set; }
    }
}
