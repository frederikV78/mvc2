using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace MVCModule.Models {
    internal class MVCModuleUserManager : UserManager<IdentityUser>, IUserManager {
        [InjectionConstructor]
        public MVCModuleUserManager() : this(new MVCModuleDbContext()) { }
        public MVCModuleUserManager(DbContext context) : base(new UserStore<IdentityUser>(context)) {
            this.UserValidator = new UserValidator<IdentityUser>(this) {
                AllowOnlyAlphanumericUserNames = false
            };
            this.PasswordValidator = new PasswordValidator() {
                RequiredLength = 0,
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };
        }

        public IUser Find(string userName, string password) {
            return this.FindAsync(userName, password).Result;
        }
        public ClaimsIdentity CreateIdentity(IUser user) {
            return this.CreateIdentityAsync(user as IdentityUser, DefaultAuthenticationTypes.ApplicationCookie).Result;
        }
        public bool Create(IUser user, string password) {
            return this.CreateAsync(user as IdentityUser, password).Result.Succeeded;
        }

    }
}
