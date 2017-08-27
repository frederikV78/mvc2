using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCModule.Models
{
    public class AppointmentsModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IDictionary<int, Doctor> Doctors { get; set; }

    }
}