namespace SqlLiteSampleTime.Models;

    public class TimeTable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartTime { get; set; }
        public TimeOnly StartTimeOnly 
            => TimeOnly.Parse(StartTime);

    }
