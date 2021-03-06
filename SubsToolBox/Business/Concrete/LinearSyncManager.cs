﻿using SubsToolBox.Business.Abstract;
using SubsToolBox.Model;
using System;
using System.IO;
using System.Linq;

namespace SubsToolBox.Business.Concrete
{
    public class LinearSyncManager : SyncManager
    {
        #region Private Members

        private enum SyncMode
        {
            FileFirst
            , UserInputFirst
        };

        private TimeSpan timeGap;
        private SyncMode syncMode;

        #endregion

        #region Constructor

        public LinearSyncManager(SubtitleFile inputFile, TimeSpan firstSubtitleTime)
        {
            this.inputFile = inputFile;
            this.firstSubtitleTime = firstSubtitleTime;
            InitializeSyncDirection();
            CalculateGap();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Define if file is used as first operand in sync process
        /// </summary>
        private void InitializeSyncDirection()
        {
            TimeSpan inputfirstSubtitleTime = this.inputFile.Subtitles.OrderBy(s => s.Id).First().Start;
            if (inputfirstSubtitleTime > this.firstSubtitleTime)
            {
                this.syncMode = SyncMode.FileFirst;
            }
            else
            {
                this.syncMode = SyncMode.UserInputFirst;
            }
        }

        /// <summary>
        /// Calculate the fixed time gap between 1st subtitle and user inputed 1st time 
        /// </summary>
        private void CalculateGap()
        {
            if (this.syncMode == SyncMode.FileFirst)
            {
                this.timeGap = this.inputFile.Subtitles.OrderBy(s => s.Id).First().Start - this.firstSubtitleTime;
            }
            else
            {
                this.timeGap = this.firstSubtitleTime - this.inputFile.Subtitles.OrderBy(s => s.Id).First().Start;
            }
        }

        /// <summary>
        /// Resync specific subtitle to new time using pre-calculated timeGap
        /// </summary>
        /// <param name="inputSubtitle">Subtitle to resync</param>
        /// <returns>Resync Subtitle</returns>
        private Subtitle SyncSubtitle(Subtitle inputSubtitle)
        {
            Subtitle outputSubtitle = new Subtitle();
            outputSubtitle.Id = inputSubtitle.Id;
            outputSubtitle.Text = inputSubtitle.Text;

            if (this.syncMode == SyncMode.FileFirst)
            {
                outputSubtitle.Start = inputSubtitle.Start - timeGap;
                outputSubtitle.End = inputSubtitle.End - timeGap;
            }
            else
            {
                outputSubtitle.Start = inputSubtitle.Start + timeGap;
                outputSubtitle.End = inputSubtitle.End + timeGap;
            }

            return outputSubtitle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create new file of resynced subtitles prefixed with "OUT_"
        /// </summary>
        /// <returns>New resynced subtitle file</returns>
        public override SubtitleFile SyncFile(string outputFilePath)
        {
            SubtitleFile outputFile = new SubtitleFile();
            outputFile.FileDirectory = Path.GetDirectoryName(outputFilePath);
            outputFile.FileName = Path.GetFileName(outputFilePath);

            foreach (Subtitle sub in this.inputFile.Subtitles)
            {
                outputFile.Subtitles.Add(SyncSubtitle(sub));
            }

            return outputFile;
        }

        #endregion
    }
}
