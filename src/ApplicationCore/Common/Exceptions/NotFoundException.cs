using System;
using System.Linq;

namespace ApplicationCore.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, params object[] keys)
            : base($"Entity \"{name}\" ({String.Join(", ", keys.Select(x => x.ToString()))}) was not found.")
        {
        }
    }
}