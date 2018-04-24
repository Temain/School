using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Student
    {
        public Student()
        {
            CreatedAt = DateTime.Now;
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<LessonDetail> LessonDetails { get; set; }
    }
}