using System;
using System.Collections.Generic;
using System.Linq;

namespace SequencesAndIterators
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IPainter> ThoseAvailable(this IEnumerable<IPainter> painters)
            => painters.Where(p => p.IsAvailable);

        public static IPainter FindCheapestV1(this IEnumerable<IPainter> painters, double sqMeters)
            => painters.Aggregate((best, current) =>
                                      best.EstimateCompensation(sqMeters) < current.EstimateCompensation(sqMeters)
                                          ? best
                                          : current
            );

        public static IPainter FindCheapestV2(this IEnumerable<IPainter> painters, double sqMeters)
            => painters.Aggregate((IPainter) null, (best, current) =>
                                      best == null ||
                                      current.EstimateCompensation(sqMeters) < best.EstimateCompensation(sqMeters)
                                          ? current
                                          : best
            );

        public static T WithMinimum<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criteria) where T : class
                                                                                                   where TKey :
                                                                                                   IComparable<TKey>
            => sequence.Aggregate(
                (T) null, (best, current) => best == null || criteria(current)
                                                 .CompareTo(criteria(best)) < 0
                                                 ? current
                                                 : best);

        public static T WithMinimumV2<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criteria) where T : class
                                                                                                     where TKey :
                                                                                                     IComparable<TKey>
            => sequence.Select(obj => Tuple.Create(obj, criteria(obj)))
                       .Aggregate((Tuple<T, TKey>) null, (best, current) =>
                                      best is null || current.Item2.CompareTo(best.Item2) < 0
                                          ? current
                                          : best)
                       .Item1;
    }
}
