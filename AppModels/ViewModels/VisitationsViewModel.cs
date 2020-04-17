using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class VisitationsViewModel
    {
        public List<Visitation> Visitations { get; set; }

        [DisplayName("Reporting Date")]
        [DataType(DataType.Date)]
        public DateTime ReportingDate { get; set; }
        [DisplayName("Leaving Date")]
        [DataType(DataType.Date)]
        public DateTime LeavingDate { get; set; }
    }
}
