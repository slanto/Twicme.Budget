using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Month : ValueObject<Month>
    {
        public int Year { get; }
        public MonthName MonthName { get; }
        public DateTime Value { get; } 
        
        private Month(int year, MonthName monthName)
        {
            Year = year;
            MonthName = monthName;
            Value = new DateTime(Year, MonthName.Index, 1);
        }   
        
        public static implicit operator DateTime(Month month) => month.Value;
        
        public static Month Create(int year, MonthName monthName) => new Month(year, monthName);
        
        public static Month Create(int year, int month) => new Month(year, MonthName.Create(month));
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}