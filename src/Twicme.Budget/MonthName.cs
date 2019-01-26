using System;
using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class MonthName : ValueObject<MonthName>
    {
        public static readonly MonthName January = new MonthName(1, "January");
        public static readonly MonthName February = new MonthName(2, "February");
        public static readonly MonthName March = new MonthName(3, "March");
        public static readonly MonthName April = new MonthName(4, "April");
        public static readonly MonthName May = new MonthName(5, "May");
        public static readonly MonthName June = new MonthName(6, "June");
        public static readonly MonthName July = new MonthName(7, "July");
        public static readonly MonthName August = new MonthName(8, "August");
        public static readonly MonthName September = new MonthName(9, "September");
        public static readonly MonthName October = new MonthName(10, "October");
        public static readonly MonthName November = new MonthName(11, "November");
        public static readonly MonthName December = new MonthName(12, "December");

        public int Index { get; }
        public string Name { get; }

        private MonthName(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public static MonthName Create(string name)
        {
            var found = All.Single(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            Contracts.Require(found != null, $"Month name: {name} not found.");
            
            return new MonthName(found.Index, found.Name);
        }

        private static IEnumerable<MonthName> All => new[]
            {January, February, March, April, May, June, July, August, September, October, November, December};

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Index;
            yield return Name;
        }
    }
}