using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Class
    {
        public Class()
        {
            CreatedAt = DateTime.Now;
        }

        public int ClassId { get; set; }
        public int Number { get; set; }
        public string Prefix { get; set; }
        public string Direction { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Discipline> Disciplines { get; set; }
    }
}