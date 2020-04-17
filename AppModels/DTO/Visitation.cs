using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class Visitation
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime ReportingDate { get; set; }
        public DateTime LeavingDate { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Done { get; set; }
        public bool Cancelled { get; set; }
    }
}
