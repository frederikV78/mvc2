using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModule.Models
{
    public class CreateAppointmentModel
    {
        [Required(ErrorMessage = "Please enter a date")]
        [DataType(DataType.Date)]
        [Display(Name = "The date for the appointment")]
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Please enter a reason for the appointment")]
        [DataType(DataType.Text)]
        [Display(Name = "Your reason")]
        public string Reason { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }

    }
}