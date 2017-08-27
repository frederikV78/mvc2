using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCModule.Models {
    public interface IAppointmentManager : IDisposable {
        IEnumerable<Appointment> Appointments { get; }
        void Create(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(Doctor doctor);
        IEnumerable<Appointment> GetAppointments(IIdentity patient);
        IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId);
        IEnumerable<Appointment> GetAppointmentsForPatient(string patient);
        void Delete(Appointment appointment);
        void Delete(int appointmentId);
    }
    public interface IDoctorManager : IDisposable {
        IEnumerable<Doctor> Doctors { get; }
    }
    public interface ITagLineManager : IDisposable {
        IEnumerable<TagLine> TagLines { get; }
        TagLine GetRandom();
    }
    public interface IUserManager : IDisposable {
        IUser Find(string userName, string password);
        ClaimsIdentity CreateIdentity(IUser user);
        bool Create(IUser user, string password);
    }
}
