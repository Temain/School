using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Lesson
    {
        public Lesson()
        {
            CreatedAt = DateTime.Now;
        }

        public int LessonId { get; set; }

        public DateTime LessonDate { get; set; }

        public int DisciplineId { get; set; }
        public virtual Discipline Discipline { get; set; }

        public string HomeWork { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<LessonDetail> LessonDetails { get; set; }
    }
}