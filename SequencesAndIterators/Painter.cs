using System;

namespace SequencesAndIterators
{
    public class Painter : IPainter
    {
        public bool IsAvailable { get; } = true;
        
        private readonly double _factor;

        public Painter()
        {
            var rand = new Random();
            IsAvailable = rand.NextDouble() < 0.5;
            _factor = rand.NextDouble() * 5;
        }

        public TimeSpan EstimateTimeToPaint(double squareMeters)
        {
            var approximation = squareMeters * _factor;
            var result = (int) Math.Ceiling(approximation);
            return TimeSpan.FromHours(result);
        }

        public double EstimateCompensation(double squareMeters)
        {
            return squareMeters * _factor;
        }
    }
}
