﻿using SubsToolBox.Business.Abstract;
using SubsToolBox.Business.Concrete;
using SubsToolBox.Model;
using SubsToolBox.Service.Helpers;
using System;

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

        public SyncService(string inputFilePath, TimeSpan inputFirstTime, int firstId)
        {
            FileHelper fh = new FileHelper();
            this.inputFile = fh.GenerateSubtitleFile(inputFilePath);
            this.inputFile.StartSubtitleListFromId(firstId);
            this.inputFirstTime = inputFirstTime;
        }

        #endregion

        #region Private Methods

        private void PhysicallyWriteSubtitleFile(SubtitleFile subtitleFile)
        {
            FileHelper fh = new FileHelper();
            fh.WriteSubtitleFilePhysically(subtitleFile);
        }

        #endregion

        #region Public Methods

        public void LinearSynchronization(string outputFilePath, bool overlapFix)
        {
            sm = new LinearSyncManager(this.inputFile, this.inputFirstTime);
            SubtitleFile outputFile = sm.SyncFile(outputFilePath);

            if (overlapFix)
            {
                sm.RemoveOverlap(ref outputFile);
            }

            try
            {
                PhysicallyWriteSubtitleFile(outputFile);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void ProgressiveSynchronization(string outputFilePath, double videoFrameRate, double subtitleFrameRate, TimeSpan inputLastTime, bool overlapFix)
        {
            sm = new ProgressiveSyncManager(this.inputFile, this.inputFirstTime, videoFrameRate, subtitleFrameRate);
            SubtitleFile outputFile = sm.SyncFile(outputFilePath);

            if (overlapFix)
            {
                sm.RemoveOverlap(ref outputFile);
            }

            try
            {
                PhysicallyWriteSubtitleFile(outputFile);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        #endregion
    }
}
