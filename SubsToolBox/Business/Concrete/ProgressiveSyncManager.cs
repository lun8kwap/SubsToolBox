using SubsToolBox.Business.Abstract;
using SubsToolBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Business.Concrete
{
    public class ProgressiveSyncManager: SyncManager
    {
        #region Public Members

        public TimeSpan lastSubtitleTime { get; set; }

        #endregion

        #region Public Methods

        public override SubtitleFile SyncFile(string outputFilePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
