using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Companies.Models
{
    public class Company
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Enter company name")]
        [Display(Name="Company name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter earning")]
        [Display(Name="Company earning")]
        public int Earning { get; set; }
        [NotMapped]
        public int? TotalEarning { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? ParentId { get; set; }
    }
}