using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Theme { get; set; }
        public string Details { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingDate { get; set; }
        public byte[] Banner { get; set; }
    }
}
