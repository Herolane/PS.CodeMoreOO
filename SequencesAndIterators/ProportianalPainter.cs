using System;

namespace SequencesAndIterators
{
    public class ProportianalPainter : IPainter
    {
        public TimeSpan TimePerSqMeter { get; set; }
        public double CostPerSqMeter { get; set; }
        public bool IsAvailable { get; }

        public TimeSpan EstimateTimeToPaint(double squareMeters) =>
            TimeSpan.FromHours(TimePerSqMeter.TotalHours * squareMeters);


        public double EstimateCompensation(double squareMeters) => CostPerSqMeter * squareMeters;
    }
}