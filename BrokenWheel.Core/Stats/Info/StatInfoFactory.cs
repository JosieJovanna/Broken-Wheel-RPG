using System;
using System.Collections.Generic;
using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Stats.Info
{
    public static class StatInfoFactory
    {
        private const string CATEGORY = "Stat Info Factory";

        private static bool _isInitiated = false;
        private static ILogger _logger;

        private static readonly IDictionary<Stat, StatInfo> _enumToInfo = new Dictionary<Stat, StatInfo>();
        private static readonly IDictionary<string, StatInfo> _codeToInfo = new Dictionary<string, StatInfo>();

        /// <summary>
        /// Gets stat info from the enum values' cached attribute data.
        /// </summary>
        /// <exception cref="ArgumentException"> If stat is custom. </exception>
        /// <exception cref="Exception"> If not initiated. </exception>
        public static StatInfo FromEnum(Stat stat)
        {
            if (!_isInitiated)
                throw new Exception($"{nameof(StatInfoFactory)} is not initiated!");
            if (stat == Stat.Custom)
                throw new ArgumentException($"{nameof(stat)} cannot be {nameof(Stat.Custom)} - custom stats have no default information, and must be specially built.");
            return EnumToStatInfo(stat);
        }

        private static StatInfo EnumToStatInfo(Stat stat)
        {
            if (_enumToInfo.TryGetValue(stat, out var cachedStatInfo))
                return cachedStatInfo;
            var statInfo = stat.GetStatInfoFromAttribute();
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
            if (!_isInitiated)
                throw new Exception($"{nameof(StatInfoFactory)} is not initiated!");
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or whitespace.");
            if (!_codeToInfo.ContainsKey(code))
                throw new InvalidOperationException($"Custom stat with code '{code}' is not registered.");

            return _codeToInfo[code];
        }

        public static void Initialize(ILogger logger, IList<StatInfo> customStats)
        {
            _logger = logger;
            var stats = GetAllNonCustomEnums();
            foreach (var stat in stats)
                AddInfoToDictionaries(stat);
            AddCustomStatInfoToDictionary(customStats);
            _isInitiated = true;
        }

        private static IList<Stat> GetAllNonCustomEnums()
        {
            var allEnumVals = EnumUtils.GetAllEnumValues<Stat>();
            allEnumVals.Remove(Stat.Custom);
            return allEnumVals;
        }

        private static void AddInfoToDictionaries(Stat stat)
        {
            var info = stat.GetStatInfoFromAttribute();
            _enumToInfo.Add(stat, info);
            _codeToInfo.Add(info.Code, info);
            _codeToInfo.Add(info.Namespace + MiscConstants.NAMESPACE_SEPARATOR + info.Code, info);
        }

        private static void AddCustomStatInfoToDictionary(IList<StatInfo> customStats)
        {
            if (customStats == null || customStats.Count < 1)
                _logger.LogCategoryWarning(CATEGORY, "No custom stats - none registered");
            else
                foreach (var statInfo in customStats)
                    AddCustomCodeIfNoConflict(statInfo);
        }

        private static void AddCustomCodeIfNoConflict(StatInfo statInfo)
        {
            if (_codeToInfo.ContainsKey(statInfo.Code))
            {
                _logger.LogCategoryError(CATEGORY, $"Duplicate custom stat code: {statInfo.Code} - not registered");
                return;
            }
            _codeToInfo.Add(statInfo.Code, statInfo);
        }
    }
}
