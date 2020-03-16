using System;
using System.Collections.Generic;

namespace AppModels.DTO
{
    public class Feature
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Disability { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<ChildFeature> Children { get; set; }
    }
}
