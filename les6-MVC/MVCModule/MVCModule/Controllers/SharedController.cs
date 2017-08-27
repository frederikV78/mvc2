using MVCModule.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModule.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult TagLine()
        {
            var tagManager = ServiceLocator.Resolve<ITagLineManager>();
            return PartialView(tagManager.GetRandom());

        }

        public ActionResult Menu()
        {
            XmlSiteMapProvider testXmlProvider = new XmlSiteMapProvider();
            NameValueCollection providerAttributes = new NameValueCollection(1);
            providerAttributes.Add("siteMapFile", "Web.sitemap");
            testXmlProvider.Initialize("menuProvider", providerAttributes);
            var collection = testXmlProvider.GetChildNodes(testXmlProvider.RootNode);
            return PartialView(collection);
        }




    }
}