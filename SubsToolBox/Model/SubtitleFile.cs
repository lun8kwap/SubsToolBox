using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Model
{
    public class SubtitleFile
    {
        #region Members

        public List<Subtitle> Subtitles { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }

        #endregion
    }
}
