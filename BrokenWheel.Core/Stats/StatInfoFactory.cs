using System;
using System.Collections.Generic;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats
{
    public static class StatInfoFactory
    {
        private static readonly IDictionary<Stat, StatInfo> _statInfoCache = new Dictionary<Stat, StatInfo>();
        private static readonly IDictionary<string, StatInfo> _codeInfoCache = new Dictionary<string, StatInfo>();

        /// <summary>
        /// Gets stat info from the metadata stored in the enum file.
        /// </summary>
        /// <exception cref="InvalidOperationException"> If stat is custom. </exception>
        public static StatInfo InfoForNonCustomStat(Stat stat)
        {
            if (stat != Stat.Custom)
                return InfoByEnum(stat);
            var customMsg = $"{nameof(stat)} cannot be {nameof(Stat.Custom)} - custom stats have no default information, and must be specially built.";
            throw new InvalidOperationException(customMsg);
        }

        private static StatInfo InfoByEnum(Stat stat)
        {
            var statInfo = _statInfoCache.TryGetValue(stat, out var cachedStatInfo) 
                ? cachedStatInfo 
                : stat.StatInfoFromAttribute();
            CacheStatInfo(statInfo);
            return statInfo;
        }

        /// <summary>
        /// Gets stat info for a given code, if registered.
        /// </summary>
        /// <exception cref="ArgumentException"> If code is null or whitespace. </exception>
        /// <exception cref="InvalidOperationException"> If there is no custom stat with the given code. </exception>
        public static StatInfo InfoForStatCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or whitespace.");
            if (!_codeInfoCache.ContainsKey(code))
                throw new InvalidOperationException($"Custom stat with code '{code}' is not registered.");

            return _codeInfoCache[code];
        }

        /// <summary>
        /// Registers stat info to caches - only by code, if a custom stat.
        /// </summary>
        internal static void CacheStatInfo(StatInfo statInfo)
        {
            if (!statInfo.IsCustom && !_statInfoCache.ContainsKey(statInfo.Stat))
                _statInfoCache.Add(statInfo.Stat, statInfo);
            if (!_codeInfoCache.ContainsKey(statInfo.Code))
                _codeInfoCache.Add(statInfo.Code, statInfo);
        }
    }
}
