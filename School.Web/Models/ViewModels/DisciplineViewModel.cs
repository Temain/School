using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models.ViewModels
{
    public class DisciplineViewModel
    {
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public int ClassId { get; set; }
        public virtual string ClassName { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public int? NumberOfLessons { get; set; }

        public List<int> SelectedMaterials { get; set; }
        public List<MaterialViewModel> Materials { get; set; }
    }
}