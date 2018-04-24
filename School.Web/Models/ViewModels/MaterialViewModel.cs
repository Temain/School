using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Models.ViewModels
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string Authors { get; set; }
        public int Pages { get; set; }
    }
}