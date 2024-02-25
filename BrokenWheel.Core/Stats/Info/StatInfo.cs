using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Info
{
    public class StatInfo
    {
        public Stat Stat { get; internal set; }
        public StatType Type { get; internal set; }
        public StatCategory Category { get; internal set; }

        public bool IsCustom { get; internal set; } = false;
        public bool IsComplex { get; internal set; }

        public string Namespace { get; internal set; } = DebugConstants.BROKEN_WHEEL_NAMESPACE;
        public string Code { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public int MaxValue { get; internal set; } = 100;
        public int MinValue { get; internal set; } = 0;
        public int DefaultValue { get; internal set; } = 10;

        /// <summary>
        /// Returns <see cref="Stat"/>.ToString() if not custom; otherwise returns <see cref="Code"/>.
        /// </summary>
        public string Id()
        {
            return IsCustom ? Stat.ToString() : Code;
        }
    }
}
