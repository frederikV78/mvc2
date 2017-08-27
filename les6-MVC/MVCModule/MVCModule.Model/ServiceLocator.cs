using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace MVCModule.Models {
    public static class ServiceLocator {
        private static UnityContainer _container;

        static ServiceLocator() {
            _container = new UnityContainer();
            _container.RegisterInstance<IUnityContainer>(_container);

            // register the views
            _container.RegisterType<IAppointmentManager, AppointmentManager>();
            _container.RegisterType<ITagLineManager, TagLineManager>();
            _container.RegisterType<IDoctorManager, DoctorManager>();
            _container.RegisterType<IUserManager, MVCModuleUserManager>();
        }

        public static TProxyType Resolve<TProxyType>() {
            return _container.Resolve<TProxyType>();
        }
        
    }
}
