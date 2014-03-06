using SubsToolBox.Business.Abstract;
using SubsToolBox.Business.Concrete;
using SubsToolBox.Model;
using SubsToolBox.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsToolBox.Service
{
    public class SyncService
    {
        #region Private Members

        private SyncManager sm;
        private SubtitleFile inputFile;
        private TimeSpan inputFirstTime;

        #endregion

        #region Constructor

        public SyncService(string inputFilePath, string inputFirstTime)
        {
            FileHelper fh = new FileHelper();
            this.inputFile = fh.GenerateSubtitleFile(inputFilePath);

            this.inputFirstTime = TimeSpan.Parse(inputFirstTime);
        }

        #endregion

        #region Public Methods

        public void LinearSynchronization(bool overlapFix)
        {
            sm = new LinearSyncManager(this.inputFile, this.inputFirstTime);
            SubtitleFile outputFile = sm.SyncFile();

            if (overlapFix)
            {
                sm.RemoveOverlap(ref outputFile);
            }
        }

        public void ProgressiveSynchronization(string inputLastTime, bool overlapFix)
        {
            
        }

        #endregion
    }
}
