using System;

namespace mongoApi.Models
{
    public class student
    {
        public Object _id { get; set; }
        public string IdStudent { get; set; }
        public string NameStudent { get; set; }
        public string SubjectStudent { get; set; }
        public string ScoreStudent { get; set; }
        public DateTime? DateStudent { get; set; }

    }
}