using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Constants;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Stats.Info
{
    public static class StatInfoFactory
    {
        private static readonly ILogger _logger;
        private static readonly IDictionary<Stat, StatInfo> _enumToInfo = new Dictionary<Stat, StatInfo>();
        private static readonly IDictionary<string, StatInfo> _codeToInfo = new Dictionary<string, StatInfo>();

        static StatInfoFactory()
        {
            _logger = Injection.GetModule().GetLogger();
            // register base stats
            foreach (var stat in GetAllBaseRPGStats())
                RegisterStat(stat);
        }

        private static IEnumerable<Stat> GetAllBaseRPGStats()
        {
            var allEnumVals = EnumUtil.GetAllEnumValues<Stat>();
            allEnumVals.Remove(Stat.Custom);
            return allEnumVals;
        }

        private static void RegisterStat(Stat stat)
        {
            var info = stat.GetInfo();
            _enumToInfo.Add(stat, info);
            _codeToInfo.Add(info.Code, info);
            _codeToInfo.Add(info.Namespace + MiscConstants.NAMESPACE_SEPARATOR + info.Code, info);
        }

        /// <summary>
        /// Registers custom stats with the factory. Should be called at launch.
        /// </summary>
        public static void RegisterCustomStats(IEnumerable<StatInfo> stats)
        {
            if (stats == null || stats.Count() < 1)
                return;
            AddCustomStatInfoToDictionary(stats);
        }

        private static void AddCustomStatInfoToDictionary(IEnumerable<StatInfo> customStats)
        {
            if (customStats == null || customStats.Count() < 1)
                _logger.LogCategoryWarning(nameof(StatInfoFactory), "No custom stats - none registered");
            else
                foreach (var statInfo in customStats)
                    AddCustomCodeIfNoConflict(statInfo);
        }

        private static void AddCustomCodeIfNoConflict(StatInfo statInfo)
        {
            if (_codeToInfo.ContainsKey(statInfo.Code))
            {
                _logger.LogCategoryError(nameof(StatInfoFactory), $"Duplicate custom stat code: {statInfo.Code} - not registered");
                return;
            }
            _codeToInfo.Add(statInfo.Code, statInfo);
        }

        /// <summary>
        /// Gets stat info from the enum values' cached attribute data.
        /// </summary>
        /// <exception cref="ArgumentException"> If stat is custom. </exception>
        /// <exception cref="Exception"> If not initiated. </exception>
        public static StatInfo FromEnum(Stat stat)
        {
            if (stat == Stat.Custom)
                throw new ArgumentException($"{nameof(stat)} cannot be {nameof(Stat.Custom)} - custom stats have no default information, and must be specially built.");
            return EnumToStatInfo(stat);
        }

        private static StatInfo EnumToStatInfo(Stat stat)
        {
            if (_enumToInfo.TryGetValue(stat, out var cachedStatInfo))
                return cachedStatInfo;
            var statInfo = stat.GetInfo();
            CacheStatInfo(statInfo); // It would be odd if an enum value to be registered that is not custom, but in case it happens, let's try getting info.
            return statInfo;
        }

        private static void CacheStatInfo(StatInfo statInfo)
        {
            if (!statInfo.IsCustom && !_enumToInfo.ContainsKey(statInfo.Stat))
                _enumToInfo.Add(statInfo.Stat, statInfo);
            if (!_codeToInfo.ContainsKey(statInfo.Code))
                _codeToInfo.Add(statInfo.Code, statInfo);
        }

        /// <summary>
        /// Gets stat info for a given code, if registered.
        /// </summary>
        /// <exception cref="ArgumentException"> If code is null or whitespace. </exception>
        /// <exception cref="InvalidOperationException"> If there is no default or custom stat with the given code. </exception>
        public static StatInfo FromCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or whitespace.");
            if (!_codeToInfo.ContainsKey(code))
                throw new InvalidOperationException($"Custom stat with code '{code}' is not registered.");

            return _codeToInfo[code];
        }
    }
}
