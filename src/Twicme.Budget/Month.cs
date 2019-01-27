using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Month : ValueObject<Month>
    {
        public DateTime Value { get; } 
        public Month(int year, MonthName monthName)
        {
            Value = new DateTime(year, monthName.Index, 1);
        }   
        
        public static implicit operator DateTime(Month month) => month.Value;
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}