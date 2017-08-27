using MVCModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModule.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Appointments()
        {
            AppointmentsModel model = new AppointmentsModel();

            using (var appointmentsManager = ServiceLocator.Resolve<IAppointmentManager>())
            {
                model.Appointments = appointmentsManager.GetAppointmentsForPatient(User.Identity.Name);
            }
            using (var doctorsManager = ServiceLocator.Resolve<IDoctorManager>())
            {
                model.Doctors = doctorsManager.Doctors.ToDictionary(c => c.Id);
            }
            return View(model);
        }

        public ActionResult CreateAppointment()
        {
            using (var docMgr = ServiceLocator.Resolve<IDoctorManager>())
            {
                return View(new CreateAppointmentModel { Doctors = docMgr.Doctors, Date = DateTime.Now });
            }
        }
        [HttpPost]
        public ActionResult CreateAppointment(CreateAppointmentModel model)
        {
            // make sure you set the culture info in the web.config!
            using (var appMgr = ServiceLocator.Resolve<IAppointmentManager>())
            {
                using (var docMgr = ServiceLocator.Resolve<IDoctorManager>())
                {
                    model.Doctors = docMgr.Doctors;
                    if (ModelState.IsValid)
                    {
                        Appointment a = new Appointment();
                        a.Date = model.Date;
                        a.DoctorId = model.DoctorId;
                        a.Reason = model.Reason;
                        a.Patient = User.Identity.Name;
                        appMgr.Create(a);
                        return View("AppointmentCreated", model);
                    }
                    // If we got this far, something failed, redisplay form
                    ModelState.AddModelError("", "There are some problems with your input.");
                    return View(model);
                }
            }
        }







    }
}