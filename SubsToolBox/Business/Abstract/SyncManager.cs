using SubsToolBox.Model;
using System;

namespace SubsToolBox.Business.Abstract
{
    public abstract class SyncManager
    {
        #region Public Members

        public SubtitleFile inputFile { get; set; }

        #endregion

        #region Abstract Methods

        public abstract SubtitleFile SyncFile();

        #endregion

        #region Public Methods

        public void RemoveOverlap(ref SubtitleFile inputFile)
        {
            for (int i = 0; i < inputFile.Subtitles.Count-1; i++)
            {
                if (inputFile.Subtitles[i].End > inputFile.Subtitles[i+1].Start)
                {
                    inputFile.Subtitles[i].End = inputFile.Subtitles[i + 1].Start - TimeSpan.FromMilliseconds(1);
                }
            }
        }

        #endregion
    }
}
