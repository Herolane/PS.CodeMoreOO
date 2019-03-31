using System.Collections.Generic;
using System.Linq;

namespace SequencesAndIterators
{
    public class Painters
    {
        private IEnumerable<IPainter> InternalPainters { get; }

        public Painters(IEnumerable<IPainter> internalPainters)
        {
            InternalPainters = internalPainters.ToList();
        }

        public Painters GetAvailable() =>
            new Painters(InternalPainters.Where(painter => painter.IsAvailable));

        public IPainter GetCheapestOne(double sqMeters) =>
            InternalPainters.WithMinimumV2(p => p.EstimateCompensation(sqMeters));

        public IPainter GetFastestOne(double sqMeters)
            => InternalPainters.WithMinimumV2(p => p.EstimateTimeToPaint(sqMeters));
    }
}
