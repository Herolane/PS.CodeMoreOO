using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SequencesAndIterators.Tests
{
    public class PainterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IEnumerable<IPainter> _painters;
        private const double sqMeters = 230.32;
        private readonly Stopwatch _stopwatch;

        public PainterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _painters = Enumerable.Range(0, 100)
                                  .Select(_ => new Painter());
            _painters.ToList();
            _stopwatch = new Stopwatch();
        }

        [Fact]
        public void Compare_Invations()
        {
            var actions = new List<(Action, string)>();
            actions.Add((() => _painters.FindCheapestV1(sqMeters), "V1"));
            actions.Add((() => _painters.FindCheapestV2(sqMeters), "V2"));
            actions.Add((() => _painters
                             .WithMinimum(painter => painter.EstimateCompensation(sqMeters)),
                         "WithMinimum"));
            actions.Add((() => _painters
                             .WithMinimumV2(painter => painter.EstimateCompensation(sqMeters)),
                         "WithMinimumV2"));
            actions.Add((() => BadFindCheapestPainter(sqMeters, _painters), "foreach"));
            
            
            var ranked = Rank(actions);
            var counter = 0;
            
            foreach (var (name, ticks) in ranked)
            {
                _testOutputHelper.WriteLine(name + ":\t\t" + ticks.ToString());
                counter++;
            }
        }

        private IEnumerable<(string name, long ticks)> Rank(IEnumerable<(Action, string)> actions)
        {
            var actionResults = actions.Select((a) => (a.Item2, SpeedTest(a.Item1, a.Item2)));
            return actionResults.OrderBy((a) => a.Item2);
        }

        private long SpeedTest(Action action, string name)
        {
            _stopwatch.Start();
            action();
            _stopwatch.Stop();
            return _stopwatch.ElapsedTicks;
        }

        private IPainter FindCheapestPainter(double squareMeters, IEnumerable<IPainter> painters)
        {
            return painters.ThoseAvailable()
                           .WithMinimumV2(painter => painter.EstimateCompensation(squareMeters));
        }

        private IPainter BadFindCheapestPainter(double squareMeters, IEnumerable<IPainter> painters)
        {
            double bestPrice = 0;
            IPainter cheapest = null;

            foreach (var painter in painters)
            {
                if (painter.IsAvailable)
                {
                    var price = painter.EstimateCompensation(squareMeters);

                    if (cheapest is null || price < bestPrice)
                    {
                        bestPrice = price;
                        cheapest = painter;
                    }
                }
            }

            return cheapest;
        }
    }
}
