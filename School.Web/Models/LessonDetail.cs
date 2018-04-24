using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class LessonDetail
    {
        public LessonDetail()
        {
            CreatedAt = DateTime.Now;
        }

        public int LessonDetailId { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int Grade { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}