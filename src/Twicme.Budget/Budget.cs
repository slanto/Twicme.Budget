using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget
    {   
        public ImmutableList<IMoney> Moneys { get; }
        
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }
        
        public Budget(Month month, uint year, ImmutableList<IMoney> moneys)
        {
            Month = month;
            Year = year;
            Created = DateTimeOffset.UtcNow;
            Moneys = moneys;
        }
    }
}