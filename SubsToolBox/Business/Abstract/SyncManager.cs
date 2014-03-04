using SubsToolBox.Model.Entities;
using System;

namespace SubsToolBox.Business.Abstract
{
    public abstract class SyncManager
    {
        #region Public Members

        public SubtitleFile inputFile { get; set; }

        #endregion

        #region Virtual Methods

        public virtual SubtitleFile SyncFile()
        {
        }

        #endregion
    }
}
