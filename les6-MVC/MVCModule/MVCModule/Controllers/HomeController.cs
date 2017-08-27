using MVCModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MVCModule.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OpeningHours()
        {
            List<OpenDayViewModel> ret = new List<OpenDayViewModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/OpeningHours.xml"));
            foreach (XmlNode dag in doc.SelectNodes("/OpeningHours/Day"))
            {
                OpenDayViewModel od = new OpenDayViewModel() { Name = dag.Attributes["name"].Value, Consultations = new List<ConsultationViewModel>() };
                foreach (XmlNode cons in dag.SelectNodes("./Hour"))
                {
                    od.Consultations.Add(new ConsultationViewModel() { StartTime = cons.Attributes["begin"].Value, EndTime = cons.Attributes["end"].Value, Description = cons.Attributes["type"].Value });
                }
                ret.Add(od);
            }
            return View(new OpeningHoursModel() { Days = ret });
        }
    }
}