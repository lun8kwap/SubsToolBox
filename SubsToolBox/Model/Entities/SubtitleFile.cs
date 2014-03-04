using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Model.Entities
{
    public class SubtitleFile
    {
        #region Members

        public List<Subtitle> Subs { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }

        #endregion
    }
}
