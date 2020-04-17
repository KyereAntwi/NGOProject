using AppModels.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModels.ViewModels
{
    public class EventsViewModel
    {
        public List<Event> Events { get; set; }

        [Required]
        public string Theme { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartingTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndingDate { get; set; }
        public IFormFile Banner { get; set; }
    }
}
