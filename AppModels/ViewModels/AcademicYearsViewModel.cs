using AppModels.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class AcademicYearsViewModel
    {
        public List<AcademicYear> Years { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "The year field should not be less than and more than 9")]
        public string YearName { get; set; }
    }
}
