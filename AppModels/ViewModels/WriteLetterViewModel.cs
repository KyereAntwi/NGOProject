using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class WriteLetterViewModel
    {
        public List<Child> Children { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
        public Guid ChildId { get; set; }
    }
}
