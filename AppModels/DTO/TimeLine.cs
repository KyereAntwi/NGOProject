using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class TimeLine
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Activity { get; set; }
        public string Status { get; set; }
    }
}
