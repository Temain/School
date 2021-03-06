﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models
{
    public class Teacher
    {
        public Teacher()
        {
            CreatedAt = DateTime.Now;
        }

        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Speciality { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } 

        public virtual List<Discipline> Disciplines { get; set; }
    }
}