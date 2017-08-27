using MVCModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModule.Controllers
{
    [Authorize(Roles = "Doctors")]
    public class DoctorController : Controller
    {

        //public ActionResult Appointments()
        //{
        //    DoctorAppointmentsModel model = new DoctorAppointmentsModel();
        //    using (var appointmentsManager = ServiceLocator.Resolve<IAppointmentManager>())
        //    {
        //        using (var doctorsManager = ServiceLocator.Resolve<IDoctorManager>())
        //        {
        //            var doc = doctorsManager.Doctors.First();
        //            var apps = appointmentsManager.Appointments.Where(c => c.DoctorId == doc.Id).OrderBy(c => c.Date);
        //            return View(new DoctorAppointmentsModel
        //            {
        //                Doctors = doctorsManager.Doctors.ToList(),
        //                DoctorId = doc.Id,
        //                AppointmentList = new AppointmentListViewModel { Appointments = apps.ToList() }
        //            });
        //        }

        //    }
        //}

        public ActionResult Appointments()
        {
            using (var docMgr = ServiceLocator.Resolve<IDoctorManager>())
            {
                using (var appMgr = ServiceLocator.Resolve<IAppointmentManager>())
                {
                    var docs = docMgr.Doctors;
                    return View(new DoctorAppointmentsModel
                    {
                        Doctors = docs,
                        DoctorId = docs.First().Id,
                        AppointmentList = new AppointmentListViewModel { Appointments = appMgr.GetAppointmentsForDoctor(docs.First().Id) }
                    });
                }
            }
        }

        [HttpPost]
        public ActionResult GetAppointmentList(DoctorAppointmentsModel model)
        {
            using (var appMgr = ServiceLocator.Resolve<IAppointmentManager>())
            {
                return PartialView("~/Views/Doctor/AppointmentList.cshtml",
                    new AppointmentListViewModel
                    {
                        Appointments = appMgr.GetAppointmentsForDoctor(model.DoctorId)
                    });
            }
        }

        public ActionResult DeleteAppointment(int id)
        {
            using (var appMgr = ServiceLocator.Resolve<IAppointmentManager>())
            {
                appMgr.Delete(id);
            }
            return RedirectToAction("Appointments", "Doctor");
        }




    }
}