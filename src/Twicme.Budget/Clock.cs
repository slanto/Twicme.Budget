using System;

namespace Twicme.Budget
{
    public class Clock : IClock
    {
        public DateTimeOffset NowUtc => DateTimeOffset.UtcNow;
    }
}