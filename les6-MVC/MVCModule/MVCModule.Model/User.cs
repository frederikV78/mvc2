using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

namespace MVCModule.Models
{
    public class UserObject
    {
        public UserObject(HttpRequestBase request) {
            var owin = request.GetOwinContext();
            var authManager = owin.Authentication;
        }



        public static void CreateUser(string username, string password) {
            var user = new IdentityUser(username);
            var v = new OwinContext();
            using (var usrmgr = ServiceLocator.Resolve<IUserManager>()) {
                usrmgr.Create(user, password);
            }
        }
    }
}
