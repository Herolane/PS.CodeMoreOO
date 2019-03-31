using System;

namespace SequencesAndIterators
{
    public interface IPainter
    {
        bool IsAvailable { get; }
        TimeSpan EstimateTimeToPaint(double squareMeters);
        double EstimateCompensation(double squareMeters);
    }
}
