namespace Twicme.Budget
{
    public class Month
    {
        public static readonly Month January = new Month(1, "January");
        public static readonly Month February = new Month(2, "February");
        public static readonly Month March = new Month(3, "March");
        public static readonly Month April = new Month(4, "April");
        public static readonly Month May = new Month(5, "May");
        public static readonly Month June = new Month(6, "June");
        public static readonly Month July = new Month(7, "July");
        public static readonly Month August = new Month(8, "August");
        public static readonly Month September = new Month(9, "September");
        public static readonly Month October = new Month(10, "October");
        public static readonly Month November = new Month(11, "November");
        public static readonly Month December = new Month(12, "December");

        public int Index { get; }
        public string Name { get; }

        private Month(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}