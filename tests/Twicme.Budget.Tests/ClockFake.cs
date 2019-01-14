using System;

namespace Twicme.Budget.Tests
{
    public class ClockFake : IClock
    {
        public ClockFake(DateTimeOffset datetime)
        {
            NowUtc = datetime;
        }

        public DateTimeOffset NowUtc { get; }
    }
}