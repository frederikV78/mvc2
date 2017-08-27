using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models {
    internal class AppointmentManager : DataManager, IAppointmentManager {
        public IEnumerable<Appointment> Appointments {
            get {
                return Database.Appointments.OrderByDescending(c => c.Date).ToList();
            }
        }
        public void Create(Appointment appointment) {
            if (appointment == null)
                throw new ArgumentNullException("appointment");
            Database.Appointments.Add(appointment);
            Database.SaveChanges();
        }
        public IEnumerable<Appointment> GetAppointments(Doctor doctor) {
            if (doctor == null)
                throw new ArgumentNullException("doctor");
            return GetAppointmentsForDoctor(doctor.Id);
        }
        public IEnumerable<Appointment> GetAppointments(IIdentity patient) {
            if (patient == null)
                throw new ArgumentNullException("patient");
            return GetAppointmentsForPatient(patient.Name);
        }
        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId) {
            return Database.Appointments.Where(c => c.DoctorId == doctorId).OrderByDescending(c => c.Date).ToList();
        }
        public IEnumerable<Appointment> GetAppointmentsForPatient(string patient) {
            return Database.Appointments.Where(c => c.Patient == patient).OrderByDescending(c => c.Date).ToList();
        }
        public void Delete(Appointment appointment) {
            if (appointment == null)
                throw new ArgumentNullException("doctor");
            Delete(appointment.Id);
        }
        public void Delete(int appointmentId) {
            Database.Appointments.RemoveRange(Database.Appointments.Where(c => c.Id == appointmentId));
            Database.SaveChanges();
        }
    }
}
