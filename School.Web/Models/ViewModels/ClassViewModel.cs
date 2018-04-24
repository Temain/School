using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models.ViewModels
{
    public class ClassViewModel
    {
        public int ClassId { get; set; }
        public int Number { get; set; }
        public string Prefix { get; set; }
        public string Direction { get; set; }
    }
}