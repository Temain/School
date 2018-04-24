using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models.ViewModels
{
    public class LessonDetailViewModel
    {
        public int LessonDetailId { get; set; }

        public int LessonId { get; set; }

        public int StudentId { get; set; }
        public StudentViewModel Student { get; set; }

        public int Grade { get; set; }

        public string Comment { get; set; }
    }
}