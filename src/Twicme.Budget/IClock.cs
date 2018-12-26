using System;

namespace Twicme.Budget
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
    }
}