using SubsToolBox.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Model.Entities.Concrete
{
    public class SubtitleFile : ISubtitleFile
    {
        #region Members

        public List<ISubtitle> Subs { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }

        #endregion
    }
}
