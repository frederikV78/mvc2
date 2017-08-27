using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCModule.Models
{

        public class OpeningHoursModel
        {
            public List<OpenDayViewModel> Days { get; set; }
        }
        public class OpenDayViewModel
        {
            public string Name { get; set; }
            public List<ConsultationViewModel> Consultations { get; set; }
        }
        public class ConsultationViewModel
        {
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Description { get; set; }
        }

}