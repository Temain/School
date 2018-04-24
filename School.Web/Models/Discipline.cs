using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Discipline
    {
        public Discipline()
        {
            CreatedAt = DateTime.Now;
        }

        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public int? NumberOfLessons { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Material> Materials { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
    }
}