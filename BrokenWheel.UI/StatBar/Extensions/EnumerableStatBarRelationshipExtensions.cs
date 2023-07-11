using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar.Extensions
{
    internal static class EnumerableStatBarRelationshipExtensions
    {
        internal static bool ContainsStat(this IEnumerable<StatBarRelationship> query, StatType type) =>
            query.Any(_ => _.Type == type);
        
        internal static bool ContainsStat(this IEnumerable<StatBarRelationship> query, string code) =>
            query.Any(_ => _.StatBar.Info.Code == code);
        
        internal static IEnumerable<StatBarRelationship> WhereStat(this IEnumerable<StatBarRelationship> query,
            StatType type) =>
            query.Where(_ => _.Type == type);
        
        internal static IEnumerable<StatBarRelationship> WhereStat(this IEnumerable<StatBarRelationship> query,
            string code) =>
            query.Where(_ => _.StatBar.Info.Code == code);

        internal static IEnumerable<StatBarRelationship> WhereNotStat(this IEnumerable<StatBarRelationship> query,
            StatType type) =>
            query.Where(_ => _.Type != type);
        
        internal static IEnumerable<StatBarRelationship> WhereNotStat(this IEnumerable<StatBarRelationship> query,
            string code) =>
            query.Where(_ => _.StatBar.Info.Code != code);

        internal static IEnumerable<StatBarRelationship> Ordered(this IEnumerable<StatBarRelationship> query) =>
            query.OrderBy(_ => _.Order);
    }
}
