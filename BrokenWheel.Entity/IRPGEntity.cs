using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenWheel.Entity
{
    public interface IRPGEntity
    {
        string Namespace { get; }
        string Name { get; }
        string UniqueId { get; }
    }
}
