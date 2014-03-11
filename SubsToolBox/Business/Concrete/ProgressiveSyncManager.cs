using SubsToolBox.Business.Abstract;
using SubsToolBox.Model;
using System;
using System.IO;

namespace SubsToolBox.Business.Concrete
{
    public class ProgressiveSyncManager: SyncManager
    {
        #region Private Members

        private double progressiveRatio;

        #endregion

        #region Public Members

        public TimeSpan lastSubtitleTime { get; set; }

        #endregion

        #region Constructor

        public ProgressiveSyncManager(SubtitleFile inputFile, TimeSpan firstSubtitleTime, double videoFrameRate, double subtitleFrameRate)
        {
            this.inputFile = inputFile;
            this.firstSubtitleTime = firstSubtitleTime;
            this.progressiveRatio = subtitleFrameRate / videoFrameRate;        
        }

        #endregion

        #region Private Methods

        private double CalculateProgressiveGap(TimeSpan currentTimeCode, TimeSpan firstTimeCode)
        {
            return (currentTimeCode - firstTimeCode).TotalSeconds * this.progressiveRatio;
        }

        //TODO : Manage direction for substract
        private Subtitle SyncSubtitle(Subtitle inputSubtitle, Subtitle firstInputSubtitle, Subtitle firstOutputSubtitle, bool isFirst)
        {
            Subtitle outputSubtitle = new Subtitle();
            outputSubtitle.Id = inputSubtitle.Id;
            outputSubtitle.Text = inputSubtitle.Text;

            if (isFirst)
            {
                outputSubtitle.Start = firstSubtitleTime;
                outputSubtitle.End = firstSubtitleTime + (inputSubtitle.End - inputSubtitle.Start);
            }
            else
            {
                outputSubtitle.Start = TimeSpan.FromSeconds(firstOutputSubtitle.Start.TotalSeconds + CalculateProgressiveGap(inputSubtitle.Start, firstInputSubtitle.Start));
                outputSubtitle.End = TimeSpan.FromSeconds(firstOutputSubtitle.End.TotalSeconds + CalculateProgressiveGap(inputSubtitle.End, firstInputSubtitle.End));
            }

            return outputSubtitle;
        }

        #endregion

        #region Public Methods

        public override SubtitleFile SyncFile(string outputFilePath)
        {
            SubtitleFile outputFile = new SubtitleFile();
            outputFile.FileDirectory = Path.GetDirectoryName(outputFilePath);
            outputFile.FileName = Path.GetFileName(outputFilePath);

            // Only remove interval for 1st
            outputFile.Subtitles.Add(SyncSubtitle(this.inputFile.Subtitles[0], null, null, true));

            for (int i = 1; i < this.inputFile.Subtitles.Count; i++ )
            {
                outputFile.Subtitles.Add(SyncSubtitle(this.inputFile.Subtitles[i], this.inputFile.Subtitles[0], outputFile.Subtitles[0], false));
            }

            return outputFile;
        }

        #endregion
    }
}
