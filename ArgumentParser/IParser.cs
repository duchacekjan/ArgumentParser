using System.Collections.Generic;

namespace ArgumentParser
{
    public interface IParser<T>
        where T : class, new()
    {
        IReadOnlyCollection<string> RawArguments { get; }

        T Parse(params string[] args);
    }
}
