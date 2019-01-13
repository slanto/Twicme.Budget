using System;

namespace Twicme.Budget
{
    public interface IClock
    {
        DateTimeOffset NowUtc { get; }
    }
}