using System;
using System.Collections.Generic;
using System.Linq;

namespace SequencesAndIterators
{
    public static class CompositePainterFactories
    {
        private static IPainter CreateCheapestSelector(IEnumerable<IPainter> painters) =>
            new CompositePainter<IPainter>(painters, (sqMeters, sequence) => new Painters(sequence).GetAvailable()
                                                                                                   .GetCheapestOne(
                                                                                                       sqMeters));

        private static IPainter CreateFastestSelector(IEnumerable<IPainter> painters) =>
            new CompositePainter<IPainter>(painters, (sqMeters, sequence) => new Painters(sequence).GetAvailable()
                                                                                                   .GetFastestOne(
                                                                                                       sqMeters));

        public static IPainter CreateGroup(IEnumerable<ProportianalPainter> painters) =>
            new CompositePainter<ProportianalPainter>(painters, (sqMeters, sequence) =>
            {
                var time = TimeSpan.FromHours(1 / sequence.Where(p => p.IsAvailable)
                                                          .Select(p => p.EstimateTimeToPaint(sqMeters)
                                                                        .TotalHours)
                                                          .Sum());
                var cost = sequence.Where(p => p.IsAvailable)
                                   .Select(p => p.EstimateCompensation(sqMeters) / p
                                                                                   .EstimateTimeToPaint(
                                                                                       sqMeters)
                                                                                   .TotalHours *
                                                time.TotalHours)
                                   .Sum();

                return new ProportianalPainter()
                {
                    TimePerSqMeter = TimeSpan.FromHours(time.TotalHours / sqMeters),
                    CostPerSqMeter = cost / time.TotalHours
                };
            });
    }
}
