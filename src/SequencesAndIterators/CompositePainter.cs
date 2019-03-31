using System;
using System.Collections.Generic;
using System.Linq;

namespace SequencesAndIterators
{
    public class CompositePainter<TPainter> : IPainter
        where TPainter : IPainter
    {
        private IEnumerable<TPainter> InternalPainters { get; }
        private Func<double, IEnumerable<TPainter>, IPainter> Reduce { get; }

        public CompositePainter(IEnumerable<TPainter> internalPainters,
                                Func<double, IEnumerable<TPainter>, IPainter> reduce)
        {
            InternalPainters = internalPainters.ToList();
            Reduce = reduce;
        }

        public bool IsAvailable => InternalPainters.Any(p => p.IsAvailable);

        public TimeSpan EstimateTimeToPaint(double squareMeters) => Reduce(squareMeters, InternalPainters)
            .EstimateTimeToPaint(squareMeters);

        public double EstimateCompensation(double squareMeters) => Reduce(squareMeters, InternalPainters)
            .EstimateCompensation(squareMeters);
    }
}
