using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models {
    internal class TagLineManager : DataManager, ITagLineManager {
        public IEnumerable<TagLine> TagLines {
            get {
                return Database?.TagLines.ToList();
            }
        }
        public TagLine GetRandom() {
            return Database?.TagLines.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
