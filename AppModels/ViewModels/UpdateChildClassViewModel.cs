using AppModels.DTO;
using System;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class UpdateChildClassViewModel
    {
        public List<Class> Classes { get; set; }

        public Guid ChildId { get; set; }
        public Guid CalssId { get; set; }
    }
}
