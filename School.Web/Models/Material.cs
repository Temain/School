using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Material
    {
        public Material()
        {
            CreatedAt = DateTime.Now;
        }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string Authors { get; set; }
        public int Pages { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Discipline> Disciplines { get; set; }
    }
}