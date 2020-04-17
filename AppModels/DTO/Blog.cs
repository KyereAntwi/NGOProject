using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public byte[] Banner { get; set; }
        public DateTime DateAdded { get; set; }
        public string Introduction { get; set; }

        public ICollection<BlogSub> Subs { get; set; }
    }
}
