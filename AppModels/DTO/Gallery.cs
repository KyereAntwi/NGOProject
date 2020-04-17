using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Gallery
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
