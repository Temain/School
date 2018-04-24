﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models.ViewModels
{
    public class LessonViewModel
    {
        public int LessonId { get; set; }

        public DateTime LessonDate { get; set; }

        public int DisciplineId { get; set; }
        public DisciplineViewModel Discipline { get; set; }

        public string HomeWork { get; set; }

        public virtual List<LessonDetailViewModel> LessonDetails { get; set; }
    }
}