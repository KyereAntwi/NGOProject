using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class BlogSub
    {
        [Key]
        public Guid Id { get; set; }
        public string Subtitle { get; set; }
        public string Details { get; set; }


        public Guid BlogId { get; set; }
        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; }
    }
}
