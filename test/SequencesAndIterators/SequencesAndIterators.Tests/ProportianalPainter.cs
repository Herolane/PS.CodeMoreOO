using System.Collections.Generic;
using Xunit;

namespace SequencesAndIterators.Tests
{
    public class ProportianalPainterTests
    {
        private IEnumerable<ProportianalPainter> _proportianalPainters;
        private const double SqMeters = 490.30;

        public ProportianalPainterTests()
        {
            _proportianalPainters = new ProportianalPainter[10];
        }

        [Fact]
        public void Should_Select_Fastest_Painter()
        {
            var painter = CompositePainterFactories.CreateGroup(_proportianalPainters);
        }
    }
}
