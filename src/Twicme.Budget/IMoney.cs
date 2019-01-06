using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public interface IMoney
    {
        Amount Amount { get; }
    }
}