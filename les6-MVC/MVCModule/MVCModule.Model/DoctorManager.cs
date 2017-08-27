using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models {
    internal class DoctorManager : DataManager, IDoctorManager {
        public IEnumerable<Doctor> Doctors {
            get {
                return Database.Doctors.ToList();
            }
        }
    }
}
