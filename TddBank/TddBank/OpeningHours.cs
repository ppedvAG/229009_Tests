namespace TddBank
{
    public class OpeningHours
    {
        private Dictionary<DayOfWeek, List<(TimeSpan start, TimeSpan end)>> schedule;

        public OpeningHours()
        {
            schedule = new Dictionary<DayOfWeek, List<(TimeSpan, TimeSpan)>>
            {
                [DayOfWeek.Monday] = new List<(TimeSpan, TimeSpan)>
            {
                (new TimeSpan(10, 30, 0), new TimeSpan(18, 59, 0))
            },
                [DayOfWeek.Saturday] = new List<(TimeSpan, TimeSpan)>
            {
                (new TimeSpan(13, 0, 0), new TimeSpan(15, 59, 0))
            },
                // Define the opening hours for other days as well
            };
        }

        public bool IsOpen(DateTime dateTime)
        {
            //if (dateTime.DayOfWeek == DayOfWeek.Wednesday) { return false; }

            if (schedule.TryGetValue(dateTime.DayOfWeek, out List<(TimeSpan, TimeSpan)> openingHours))
            {
                foreach (var (start, end) in openingHours)
                {
                    var timeOfDay = dateTime.TimeOfDay;
                    if (timeOfDay >= start && timeOfDay <= end)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
     
        public bool IsNowWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Saturday;
        }
    }
}
