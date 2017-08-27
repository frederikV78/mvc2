using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models {
    internal class DataManager : IDisposable {
        public DataManager() {
            this.Database = new MVCModuleDbContext();
        }
        public void Dispose() {
            this.Database?.Dispose();
            this.Database = null;
        }
        ~DataManager() {
            Dispose();
        }
        protected MVCModuleDbContext Database {
            get; private set;
        }
    }
}
