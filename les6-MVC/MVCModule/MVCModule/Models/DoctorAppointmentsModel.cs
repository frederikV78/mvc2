using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCModule.Models
{
    public class DoctorAppointmentsModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public int DoctorId { get; set; }
        public AppointmentListViewModel AppointmentList { get; set; }
    }
    public class AppointmentListViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
    }

}